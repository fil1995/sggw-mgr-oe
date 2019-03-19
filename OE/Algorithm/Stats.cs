using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Stats
{
    Algorithm algorithm;
    List<Organism> historyEpochs;
    System.Diagnostics.Stopwatch time;
    public Stats(Algorithm algorithm)
    {
        this.algorithm = algorithm;
        historyEpochs = new List<Organism>();
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
        historyEpochs.Add(algorithm.Best);
    }
    public int NumEpochFromBest(double percentage)
    {
        double betterThan = algorithm.Best.Function * percentage;
        for (int i = 0; i < historyEpochs.Count; i++)
        {
            if (historyEpochs[i].Function >= betterThan)
            {
                return i;
            }
        }
        return historyEpochs.Count-1;

    }
    public override string ToString()
    {
        return "Stats  epochs:" + algorithm.CurrentEpoch + " Organism Type:" + algorithm.Best.GetTypeOfOrganism() + " Algotithm Type:" + algorithm.GetTypeOfAlgorithm() +
                "\nTime: " + time.ElapsedMilliseconds + "milis Stop:" + algorithm.StopConditionType + " \n" +
                "-----------------" + "\n" +
                "Last population deviation: " + algorithm.PopulationDeviation() + "\n" +
                "Best: f(" + algorithm.Best.Fenotyp + ")=" + algorithm.Best.Function + "\n" +
                " 95% on : " + NumEpochFromBest(0.95) + " f(" + historyEpochs[NumEpochFromBest(0.95)].Fenotyp + ")=" + historyEpochs[NumEpochFromBest(0.95)].Function + "\n" +
                " 90% on : " + NumEpochFromBest(0.9) + " f(" + historyEpochs[NumEpochFromBest(0.9)].Fenotyp + ")=" + historyEpochs[NumEpochFromBest(0.9)].Function + "\n" +
                " 80% on : " + NumEpochFromBest(0.8) + " f(" + historyEpochs[NumEpochFromBest(0.8)].Fenotyp + ")=" + historyEpochs[NumEpochFromBest(0.8)].Function + "\n";
    }

}
