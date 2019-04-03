﻿using System;

class Algorithm
{
    bool verbose;
    bool verbose2;
    public Organism[] population;
    protected Random r;
    protected Organism best;
    public Organism Best { get { return best; } }
    protected int currentEpoch;
    public int CurrentEpoch { get { return currentEpoch; } }
    public Stats stats;

    protected StopCondition stopCondition;
    public string StopConditionType => stopCondition.StopType;
    protected SelectionType selectionType;
    public string selectionTypeName => selectionType.SelectionTypeName;
    public double mutationPercentage;

    Cities TSPcities;

    public Algorithm(Random r, 
        StopCondition stopCondition, SelectionType selectionType, string tspFileName,
        int populationSize = 20, double mutationPercentage = 0.1, 
        bool verbose = true, bool verbose2 = false)
    {
        this.r = r;
        population = new Organism[populationSize];
        this.mutationPercentage = mutationPercentage;
        this.verbose = verbose;
        this.verbose2 = verbose2;
        currentEpoch = 0;
        stats = new Stats(this);

        this.stopCondition = stopCondition;
        stopCondition.Initialize(this);

        this.selectionType = selectionType;
        selectionType.Initialize(this, r);

        TSPcities = new Cities(tspFileName);

    }
    public void Run<TOrganism>(bool saveResults=false,string filename="") where TOrganism : Organism, new()
    {
        GeneratePopulation<TOrganism>();
        Run();
        if (saveResults) stats.Save(filename);
    }
    void Run()
    {
        stats.StartLogging();
        if (verbose2) Console.WriteLine(GetPopulationValues());
        while (!stopCondition.Stop())
        {
            RunEpoch();
            if (verbose2) Console.WriteLine(GetPopulationValues());
        }
        stats.StopLogging();
        if (verbose) Console.WriteLine(stats);
    }
    public Organism Result()
    {
        return best;
    }

    void RunEpoch()
    {
        // musze wygenerowac nową populacje
        Organism[] newPopulation = new Organism[population.Length];

        //for (int i = 0; i < population.Length; i++)
        //{
        //    newPopulation[i] = CreateChild();
        //}

        System.Threading.Tasks.Parallel.For(0, population.Length, i =>
        {
            newPopulation[i] = CreateChild();
        });

        population = newPopulation;

        // sprawdzamy czy selekcja wymaga posortowanej populacji
        if (selectionType.NeedSortedPopulation)
            Array.Sort(population);

        currentEpoch++;
        // aktualizacja najlepszego osobnika
        UpdateBest();
        stats.AfterEpoch();
    }
    Organism CreateChild()
    {
        Organism selA = selectionType.Select(); // SelectOrganism();
        Organism selB = selectionType.Select(); // SelectOrganism();


        Organism newOrganism = selA.RecombinationWithMutation(selB, r, mutationPercentage);

        // jeśli osobnik jest niepoprawny, to robimy od nowa rekombinacje max 5 razy
        int j = 0;
        while (!newOrganism.IsVaild && j < 5)
        {
            newOrganism = selA.RecombinationWithMutation(selB, r);
            j++;
        }
        return newOrganism;
    }

    public void GeneratePopulation<TOrganism>() where TOrganism : Organism, new()
    {
        OrganismFactory<TOrganism> f = new OrganismFactory<TOrganism>();
        for (int i = 0; i < population.Length; i++)
        {
            //population[i] = CreateOrganism();
            population[i] = f.CreateOrganism(r, TSPcities);
        }
        // aktualizacja najlepszego osobnika
        best = BestFromPopulation();

        // sprawdzamy czy selekcja wymaga posortowanej populacji
        if (selectionType.NeedSortedPopulation)
            Array.Sort(population);

        stats.AfterEpoch();
    }

    Organism BestFromPopulation()
    {
        Organism best = population[0];

        for (int i = 1; i < population.Length; i++)
        {
            best = best.Better(population[i]);//             best = Organism.Better(best,population[i]);
        }

        return best;
    }
    void UpdateBest()
    {
        for (int i = 1; i < population.Length; i++)
        {
            best = best.Better(population[i]);
        }
    }
    public double PopulationDeviation()
    {
        double average = 0;
        for (int i = 0; i < population.Length; i++)
        {
            average += population[i].Fitness;
        }
        average /= population.Length;

        double sum = 0;
        for (int i = 0; i < population.Length; i++)
        {
            sum += Math.Pow(population[i].Fitness - average, 2);
        }
        return Math.Sqrt(sum / population.Length);
    }
    public virtual string GetTypeOfAlgorithm()
    {
        return "Algorithm";
    }
    public string GetPopulationValues()
    {
        string res = "Population values (" + currentEpoch + " epoch):\n";
        for (int i = 0; i < population.Length; i++)
        {
            res += String.Format("{0:0.000}\t", population[i].Phenotype);
            if (i == 9)
            {
                res += "\n";
            }

        }
        foreach (Organism o in population)
        {
        }
        res += "\nBest:" + best.Phenotype + "\n";
        return res;
    }
}
