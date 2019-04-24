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
        if (algorithm.CurrentEpoch%100 ==0) Console.WriteLine($"Epoch:{algorithm.CurrentEpoch} deviation: {algorithm.PopulationDeviation()} best: {algorithm.Best.Distance}");
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
        return "::Stats:: epochs:" + algorithm.CurrentEpoch + " Population size: " + algorithm.population.Length + " Time: " + time.Elapsed +" file:"+algorithm.TSPcities.name +"\n" +
                "Crossover:" + algorithm.crossover.CrossoverTypeName +
                " | Stop:" + algorithm.StopConditionType +
                " | Selection:" + algorithm.selectionTypeName +
                " | Mutation:" +algorithm.mutation.MutationTypeName+": " + algorithm.mutation.mutationPercentage +"%"+
                "\n-----------------\n" +
                "Last population deviation: " + algorithm.PopulationDeviation() + "\n" +
                "Best distance:" + algorithm.Best.Distance + "\n" +
                " 95% on: " + NumEpochFromBest(0.95) + " Dst:" + historyEpochs[NumEpochFromBest(0.95)].best.Distance + "\n" +
                " 90% on: " + NumEpochFromBest(0.9) + " Dst:" + historyEpochs[NumEpochFromBest(0.9)].best.Distance + "\n" +
                " 80% on: " + NumEpochFromBest(0.8) + " Dst:" + historyEpochs[NumEpochFromBest(0.8)].best.Distance + "\n";
    }
    public void Save(string filename)
    {
        
        using (var stream = File.AppendText(filename))
        {
            stream.WriteLine(   $"{algorithm.CurrentEpoch};{time.ElapsedMilliseconds};{algorithm.population.Length};{algorithm.Best.GetTypeOfOrganism()};{algorithm.StopConditionType};"+
                            $"{algorithm.selectionTypeName};{algorithm.mutation.mutationPercentage};{algorithm.PopulationDeviation()};"+
                            $"{algorithm.crossover.CrossoverTypeName};{algorithm.mutation.MutationTypeName};{algorithm.crossover.genotypeRepresentation};");
            stream.WriteLine("n_epoch;distance;PopulationDeviation;");
            for (int i = 0; i < historyEpochs.Count; i++)
            {
                stream.WriteLine($"{i};{historyEpochs[i].best.Distance};{historyEpochs[i].populationDeviation};");
            }
        }

    }
}
