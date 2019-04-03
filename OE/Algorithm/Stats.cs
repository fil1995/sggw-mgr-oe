using System;
using System.Collections.Generic;
using System.IO;

class Stats
{
    Algorithm algorithm;
    public List<StatsEpoch> historyEpochs;
    System.Diagnostics.Stopwatch time;
    public Stats(Algorithm algorithm)
    {
        this.algorithm = algorithm;
        historyEpochs = new List<StatsEpoch>();
        time = new System.Diagnostics.Stopwatch();
    }
    public void StartLogging()
    {
        time.Start();
    }
    public long ElapsedMilliseconds
    {
        get { return time.ElapsedMilliseconds; }
    }
    public void StopLogging()
    {
        time.Stop();
    }
    public void AfterEpoch()
    {
        historyEpochs.Add(new StatsEpoch(algorithm));
        //Console.WriteLine("Epoch:"+algorithm.CurrentEpoch);
    }
    public int NumEpochFromBest(double percentage)
    {
        double betterThan = algorithm.Best.Fitness * percentage;
        for (int i = 0; i < historyEpochs.Count; i++)
        {
            if (historyEpochs[i].best.Fitness >= betterThan)
            {
                return i;
            }
        }
        return historyEpochs.Count-1;

    }
    public override string ToString()
    {
        return  "::Stats:: epochs:" + algorithm.CurrentEpoch +" Population size: "+ algorithm.population.Length + " Time: " + time.ElapsedMilliseconds + "milis\n" +
                "Organism Type:" + algorithm.Best.GetTypeOfOrganism() + 
                " | Stop condition:" + algorithm.StopConditionType +
                " | Selection Type:" + algorithm.selectionTypeName +
                " | Mutation:" + algorithm.mutationPercentage +
                "\n-----------------\n" +
                "Last population deviation: " + algorithm.PopulationDeviation() + "\n" +
                "Best: f(" + algorithm.Best.Phenotype + ")=" + algorithm.Best.Fitness + "\n" +
                " 95% on: " + NumEpochFromBest(0.95) + "epoch f(" + historyEpochs[NumEpochFromBest(0.95)].best.Phenotype + ")=" + historyEpochs[NumEpochFromBest(0.95)].best.Fitness + "\n" +
                " 90% on: " + NumEpochFromBest(0.9) + "epoch f(" + historyEpochs[NumEpochFromBest(0.9)].best.Phenotype + ")=" + historyEpochs[NumEpochFromBest(0.9)].best.Fitness + "\n" +
                " 80% on: " + NumEpochFromBest(0.8) + "epoch f(" + historyEpochs[NumEpochFromBest(0.8)].best.Phenotype + ")=" + historyEpochs[NumEpochFromBest(0.8)].best.Fitness + "\n";
    }
    public void Save(string filename)
    {
        
        using (var stream = File.AppendText(filename))
        {
            stream.WriteLine(   $"{algorithm.CurrentEpoch};{time.ElapsedMilliseconds};{algorithm.population.Length};{algorithm.Best.GetTypeOfOrganism()};{algorithm.StopConditionType};"+
                            $"{algorithm.selectionTypeName};{algorithm.mutationPercentage};{algorithm.PopulationDeviation()};");
            stream.WriteLine("n_epoch;phenotype;");
            for (int i = 0; i < historyEpochs.Count; i++)
            {
                stream.WriteLine($"{i};{historyEpochs[i].best.Phenotype};");
            }
        }


    }

}
