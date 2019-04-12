using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


class Test01
{
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
            history[i] = RunAlgorithm();
            Console.WriteLine($"{i}\' run ");
        }

        ComputeStats();
        SaveStats(fileName);

    }

    Stats RunAlgorithm()
    {
        Algorithm a = new Algorithm(r,
                                    stopCondition,
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
        
        /// zawsze stała liczba epok
        epochs = history[0].historyEpochs.Count;
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

    static double Mediana(List<double> wyniki)
    {
        wyniki.Sort();
        if (wyniki.Count % 2 == 0)
            return wyniki[wyniki.Count / 2];
        else
            return (wyniki[wyniki.Count / 2] + wyniki[1 + (wyniki.Count / 2)]) / 2;
    }
    static double OdchylenieStandardowe(List<double> wyniki)
    {
        double srednia = wyniki.Average();
        double suma = 0;
        for (int i = 0; i < wyniki.Count; i++)
        {
            suma += Math.Pow(wyniki[i] - srednia, 2);
        }
        return Math.Sqrt(suma / wyniki.Count);
    }
    static Tuple<double, double> PrzedzialUfnosci(List<double> wyniki)
    {
        /// dla 95%
        /// 
        return new Tuple<double, double>(wyniki.Average() - OdchylenieStandardowe(wyniki) * 1.96, wyniki.Average() + OdchylenieStandardowe(wyniki) * 1.96);
    }
}