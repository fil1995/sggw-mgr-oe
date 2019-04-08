using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


class Test01
{
    Random r;
    int numRun = 1000;
    int epochs;
    Stats[] history;

    double[] epochsAvg;
    double[] epochsAvgDeviation;
    double[] epochsMax;
    double[] epochsMin;

    public Test01(Random r)
    {
        this.r = r;
        history = new Stats[numRun];
        RunTest();
    }
    void RunTest()
    {
        for (int i = 0; i < history.Length; i++)
        {
            history[i] = RunAlgorithm();
        }

        ComputeStats();
        SaveStats("stats.txt");

    }

    Stats RunAlgorithm()
    {
        Algorithm a = new Algorithm(r,
                                                                new StopNumEpochs(50),
                                                                new SelectionRouletteRank(), 
                                                                new CrossoverOrdinalSinglePoint(),
                                                                new MutationOridinalOnePoint(0.2),
                                                                "dj38.tsp",
                                                                10,  false, false
                                                                );
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
            double tmpMin = history[0].historyEpochs[epoch].best.Fitness;
            double tmpMax = history[0].historyEpochs[epoch].best.Fitness;
            // dla każdego uruchomienia
            for (int run = 0; run < numRun; run++)
            {
                // licze sume wynikow
                tmpAvg += history[run].historyEpochs[epoch].best.Fitness;
                tmpAvgDeviation += history[run].historyEpochs[epoch].populationDeviation;
                if (tmpMin > history[run].historyEpochs[epoch].best.Fitness) tmpMin = history[run].historyEpochs[epoch].best.Fitness;
                if (tmpMax < history[run].historyEpochs[epoch].best.Fitness) tmpMax = history[run].historyEpochs[epoch].best.Fitness;
            }
            epochsAvg[epoch] = tmpAvg / (double)numRun;
            epochsAvgDeviation[epoch] = tmpAvgDeviation / (double)numRun;
            epochsMin[epoch] = tmpMin;
            epochsMax[epoch] = tmpMax;

        }

    }

    public void SaveStats(string filename)
    {

        using (var stream = File.AppendText(filename))
        {
            stream.WriteLine("n_epoch;avgDeviation;avgFunction;minFunction,maxFunction");
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