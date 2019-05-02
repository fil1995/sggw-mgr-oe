using System;
using System.IO;
using System.Threading.Tasks;

class Test01
{
    private readonly object globalLock = new object();
    Random r;
    int numOfRuns;
    int epochs;
    Stats[] history;

    double[] epochsAvg;
    double[] epochsAvgDeviation;
    double[] epochsMax;
    double[] epochsMin;

    StopCondition stopCondition;
    SelectionType selectionType;
    Crossover crossover;
    Mutation mutation;
    Cities cities;
    int populationSize;
    string fileName;

    public Test01(Random r, StopCondition stopCondition,
    SelectionType selectionType,
    Crossover crossover,
    Mutation mutation,
    Cities cities,
    int populationSize,
        int numOfRuns,
        string fileName)
    {
        this.r = r;
        this.stopCondition = stopCondition;
        this.selectionType = selectionType;
        this.crossover = crossover;
        this.mutation = mutation;
        this.cities = cities;
        this.populationSize = populationSize;
        this.numOfRuns = numOfRuns;
        this.fileName = fileName;


        history = new Stats[numOfRuns];
        RunTest();
    }
    void RunTest()
    {
        for (int i = 0; i < history.Length; i++)
        {
            history[i] = RunAlgorithm(r);
            Console.WriteLine($"{i}\' run ");
        }


        //ParallelOptions po = new ParallelOptions();
        ////po.MaxDegreeOfParallelism = 4;

        //Parallel.For<Random>(0, history.Length, po,
        //    () => { lock (globalLock) { return new Random(r.Next()); } },
        //    (i, loop, local) =>
        //    {
        //        history[i] = RunAlgorithm(local);
        //        Console.WriteLine($"{i}\' run ");
        //        return local;
        //    },
        //        (x) => { }
        //);



        ComputeStats();
        SaveStats(fileName);

    }

    Stats RunAlgorithm(Random r)
    {
        StopCondition stop = stopCondition.Clone();
        Algorithm a = new Algorithm(r,
                                    stop,
                                    selectionType, 
                                    crossover,
                                    mutation,
                                    cities,
                                    populationSize,  false, false);
        a.Run();
        return a.stats;
    }

    void ComputeStats()
    {
        epochs = history[0].historyEpochs.Count;
        for (int i = 1; i < history.Length; i++)
        {
            if (epochs> history[i].historyEpochs.Count) // znajduje tam gdzie najmniej epok
            {
                epochs = history[i].historyEpochs.Count;
            }
        }

        epochsAvg = new double[epochs];
        epochsAvgDeviation = new double[epochs];
        epochsMin = new double[epochs];
        epochsMax = new double[epochs];

        // wszystkie wyniki w poszczególnych epokach - średnia, max, min
        for (int epoch = 0; epoch < epochs; epoch++)
        {
            double tmpAvg = 0;
            double tmpAvgDeviation = 0;
            double tmpMin = history[0].historyEpochs[epoch].best.Distance;
            double tmpMax = history[0].historyEpochs[epoch].best.Distance;
            // dla każdego uruchomienia
            for (int run = 0; run < numOfRuns; run++)
            {
                // licze sume wynikow
                tmpAvg += history[run].historyEpochs[epoch].best.Distance;
                tmpAvgDeviation += history[run].historyEpochs[epoch].populationDeviation;
                if (tmpMin > history[run].historyEpochs[epoch].best.Distance) tmpMin = history[run].historyEpochs[epoch].best.Distance;
                if (tmpMax < history[run].historyEpochs[epoch].best.Distance) tmpMax = history[run].historyEpochs[epoch].best.Distance;
            }
            epochsAvg[epoch] = tmpAvg / (double)numOfRuns;
            epochsAvgDeviation[epoch] = tmpAvgDeviation / (double)numOfRuns;
            epochsMin[epoch] = tmpMin;
            epochsMax[epoch] = tmpMax;

        }

    }

    public void SaveStats(string filename)
    {
        try
        {
            File.Delete(filename);
            Console.WriteLine("Usuwanie pliku:"+filename);
        }
        catch (Exception e)
        {
        }

        using (var stream = File.AppendText(filename))
        {
            stream.WriteLine("n_epoch;avgDistanceDeviation;avgDistance;minDistance;maxDistance");
            for (int i = 0; i < epochs; i++)
            {
                stream.WriteLine($"{i};{epochsAvgDeviation[i]};{epochsAvg[i]};{epochsMin[i]};{epochsMax[i]};");
            }
        }
    }
}