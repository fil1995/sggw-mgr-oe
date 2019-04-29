using System;

struct StatsEpoch
{
    public Organism best;
    public double populationDeviation;
    public double time;
    public StatsEpoch(Algorithm a)
    {
        best = a.Best;
        populationDeviation = a.PopulationDeviation();
        time = a.stats.ElapsedSeconds;
    }
}
