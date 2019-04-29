using System;

class Algorithm
{
    private readonly object globalLock = new object();

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

    public Cities TSPcities;

    public Crossover crossover;
    public Mutation mutation;

    public Algorithm(Random r,
        StopCondition stopCondition, SelectionType selectionType, Crossover crossover, Mutation mutation, Cities TSPcities,
        int populationSize = 20,
        bool verbose = true, bool verbose2 = false)
    {
        this.r = r;
        population = new Organism[populationSize];
        this.verbose = verbose;
        this.verbose2 = verbose2;
        currentEpoch = 0;
        stats = new Stats(this);

        this.stopCondition = stopCondition;
        stopCondition.Initialize(this);

        this.selectionType = selectionType;
        this.crossover = crossover;
        this.mutation = mutation;

        this.TSPcities = TSPcities;

    }
    public void Run(string filename)
    {
        Run();
        stats.Save(filename);
    }
    public void Run()
    {
        GeneratePopulation();
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
        if (crossover.PopulationOrParents)
        {
            // działam na całej populacji
            crossover.Cross(population, r);
        }
        else
        {
            // działam na wybranych rodzicach

            // musze wygenerowac nową populacje
            Organism[] newPopulation = new Organism[population.Length];

            for (int i = 0; i < population.Length; i++)
            {
                newPopulation[i] = CreateChild(r);
            }
            population = newPopulation;
        }
        // sprawdzamy czy selekcja wymaga posortowanej populacji
        if (selectionType.NeedSortedPopulation)
            Array.Sort(population);

        currentEpoch++;
        // aktualizacja najlepszego osobnika
        UpdateBest();
        stats.AfterEpoch();
    }
    Organism CreateChild(Random r)
    {
        Organism selA = selectionType.Select(r, population); // SelectOrganism();
        Organism selB = selectionType.Select(r, population); // SelectOrganism();

        Organism newOrganism = crossover.Cross(selA, selB, r);

        mutation.Mutate(newOrganism, r);

        // i tak aby znaleźć najelpszego musimy znać fitnessy każego
        // double calculateOnly = newOrganism.Fitness;

        //if (!newOrganism.IsVaild)
        //{
        //    throw new Exception("Nieprawidłowy osobnik!!!");
        //}

        return newOrganism;
    }

    public void GeneratePopulation()
    {
        //OrganismFactory<TOrganism> f = new OrganismFactory<TOrganism>();
        for (int i = 0; i < population.Length; i++)
        {
            population[i] = new Organism(TSPcities, crossover.genotypeRepresentation, r);
            //population[i] = f.CreateOrganism(r, TSPcities);
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
            average += population[i].Distance;
        }
        average /= population.Length;

        double sum = 0;
        for (int i = 0; i < population.Length; i++)
        {
            sum += Math.Pow(population[i].Distance - average, 2);
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
            res += String.Format("{0:0.000} ", population[i].Distance);
            if (i == 9)
            {
                res += "\n";
            }

        }
        res += "\nBest:" + best.Distance + "\n";
        return res;
    }
}
