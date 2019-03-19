using System;

class Algorithm
{
    protected Organism[] population;
    protected Random r;
    protected Organism best;
    public Organism Best { get { return best; } }
    protected int currentEpoch;
    public int CurrentEpoch { get { return currentEpoch; } }
    public Stats stats;

    protected StopCondition stopCondition;
    public string StopConditionType => stopCondition.StopType;

    public Algorithm(Random r, StopCondition stopCondition, int populationSize = 20)
    {
        this.r = r;
        population = new Organism[populationSize];
        currentEpoch = 0;
        stats = new Stats(this);

        this.stopCondition = stopCondition;
        stopCondition.Initialize(this);


    }
    public void Run<TOrganism>() where TOrganism : Organism, new()
    {
        GeneratePopulation<TOrganism>();
        Run();
    }
    void Run()
    {
        stats.StartLogging();
        //        GeneratePopulation();
        //Console.WriteLine(GetPopulationValues());
        //Console.WriteLine("Run() --- START --- best=" + best.Fenotyp + " Organism type:" + population[0].GetTypeOfOrganism());
        while (!stopCondition.Stop())
        {
            //Console.WriteLine(CurrentEpoch + ": " + GetPopulationValues());
            RunEpoch();

        }
        // Console.WriteLine("Run() --- END --- best="+best.Fenotyp+" Organism type:"+population[0].GetTypeOfOrganism());
        stats.StopLogging();
        Console.WriteLine(stats);
    }
    public Organism Result()
    {
        return best;
    }

    void RunEpoch()
    {
        // musze wygenerowac nową populacje
        Organism[] newPopulation = new Organism[population.Length];

        for (int i = 0; i < population.Length; i++)
        {
            newPopulation[i] = CreateChild();
        }
        population = newPopulation;
        currentEpoch++;
        // aktualizacja najlepszego osobnika
        UpdateBest();
        stats.AfterEpoch();
    }
    Organism CreateChild()
    {
        Organism selA = SelectOrganism();
        Organism selB = SelectOrganism();

        
        Organism newOrganism = selA.RecombinationWithMutation(selB, r);

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
            population[i] = f.CreateOrganism(r);
        }
        // aktualizacja najlepszego osobnika
        best = BestFromPopulation();
        stats.AfterEpoch();
    }

    public virtual Organism CreateOrganism()
    {
        return new Organism();
    }
    Organism SelectOrganism()
    {
        int losA = r.Next(0, population.Length);
        int losB = r.Next(0, population.Length);
        int i = 0;
        while (losA == losB)
        {
            losB = r.Next(0, population.Length);
            if (i == 5)
            {
                return population[losA];
            }
            i++;
        }

        return population[losA].Better(population[losB]);//  return Organism.Better(population[losA], population[losB]);
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
            average += population[i].Fenotyp;
        }
        average /= population.Length;

        double sum = 0;
        for (int i = 0; i < population.Length; i++)
        {
            sum += Math.Pow(population[i].Fenotyp - average, 2);
        }
        return Math.Sqrt(sum / population.Length);
    }
    public virtual string GetTypeOfAlgorithm()
    {
        return "Algorithm";
    }
    public string GetPopulationValues()
    {
        string res = "";
        foreach (Organism o in population)
        {
            res += o.Fenotyp + "\t";
        }
        res += "Najlepszy:" + best.Fenotyp + "\n";
        return res;
    }
}
