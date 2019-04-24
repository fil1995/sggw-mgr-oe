using System;

struct StatsEpoch
{
    public Organism best;
    public double populationDeviation;
    public long time;
    //public double populationMin;
    //public double populationMax;
    //public double populationAvg;
    public StatsEpoch(Algorithm a)
    {
        best = a.Best;
        populationDeviation = a.PopulationDeviation();
        time = a.stats.ElapsedMilliseconds;
    }
}
