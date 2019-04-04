using System;
using System.Collections.Generic;

class Organism:IComparable<Organism>
{
    // w genotypie mam reprezentacje porządkową, liczę od 0
    protected uint[] genotype;
    protected Cities TSPcities;

    protected double? cacheFitness;
    protected double? cacheDistance;

    // fenotyp to reprezentacja normalna - sieżkowa licze mista od 0
    public virtual uint[] Phenotype
    {
        get
        {
            uint[] phenotype = new uint[genotype.Length];

            List<uint> list = new List<uint>();
            for (uint i = 0; i < TSPcities.Length; i++)
            {
                list.Add(i);
            }

            for (int i = 0; i < phenotype.Length; i++)
            {
                phenotype[i] = list[(int)genotype[i]];
                list.RemoveAt((int)genotype[i]);
            }

            return phenotype;
        }
    }
    public virtual double Fitness
    {
        get
        {
            if (!cacheFitness.HasValue)
                cacheFitness = 1/Distance;
            return cacheFitness.Value;
        }
    }
    public virtual double Distance
    {
        get
        {
            if (!cacheDistance.HasValue)
                cacheDistance= TSPcities.Distance(Phenotype);
            return cacheDistance.Value;
        }
    }
    public Organism(Cities c)
    {
        TSPcities = c;
        genotype = new uint[TSPcities.Length];
    }
    public Organism(Cities c, Random r)
    {
        TSPcities = c;
        genotype = new uint[TSPcities.Length];
        SetRandomGenotype(r);
    }

    public virtual void SetRandomGenotype(Random r)
    {
        // losowe ustawienie
        for (uint i = 0; i < genotype.Length; i++)
        {
            genotype[i] = (uint)r.Next(genotype.Length - (int)i);
        }

        cacheFitness = null;
        cacheDistance = null;
    }

    static public Organism Recombination(Organism a, Organism b, Random r)
    {
        // losujemy punkt podziału
        int cutPoint1 = r.Next(b.genotype.Length);
        int cutPoint2 = r.Next(b.genotype.Length);
        if (cutPoint1>cutPoint2)
        {
            int tmp = cutPoint1;
            cutPoint1 = cutPoint2;
            cutPoint2 = tmp;
        }

        // Organism o = (Organism)a.MemberwiseClone();
        Organism o = new Organism(a.TSPcities);

        // dwa pkt podziału
        for (int i = 0; i < a.genotype.Length; i++)
        {
            if (i>cutPoint1 && i< cutPoint2)
                o.genotype[i] = a.genotype[i];
            else
                o.genotype[i] = b.genotype[i];
        }

        o.cacheFitness = null; // aby od nowa policzyła sie funkcja, bo zmienił się genotyp
        o.cacheDistance = null;
        return o; //return new Organism((a.genotype & mask) | (b.genotype & ~mask));
    }
    public Organism Mutation(Random r, double prawdopodobienstwo = 0.1)
    {
        // mutujemy jeden bit
        if (r.NextDouble() <= prawdopodobienstwo) // 10% wyników
        {
            // na ktorej pozycji
            int point = r.Next(genotype.Length);
            // jaka wartosc
            genotype[point] = (uint)r.Next(genotype.Length - point);
        }

        // mutacja procentowa ilości bitów
        //for (int i = 0; i < genotype.Length * prawdopodobienstwo; i++)
        //{
        //    // na ktorej pozycji
        //    int point = r.Next(genotype.Length);
        //    // jaka wartosc
        //    genotype[point] = (uint)r.Next(genotype.Length - point);
        //    // moze okazać się, że kilka razy zmutujemy na tej samej pozycji - do poprawy pozniej
        //}

        this.cacheFitness = null; // aby od nowa policzyła sie funkcja, bo zmienił się genotyp
        this.cacheDistance = null; // aby od nowa policzyła sie funkcja, bo zmienił się genotyp
        return this;
    }
    //public virtual Organism RecombinationWithMutation(Organism b, Random r, double prawdopodobienstwo = 0.1)
    //{
    //    return Recombination(this, b, r).Mutation(r,prawdopodobienstwo);
    //}

    public Organism Better(Organism o2)
    {
        if (this.Fitness > o2.Fitness)
            return this;
        else
            return o2;
    }
    public override string ToString()
    {
        string ret = "{";
        foreach (uint item in Phenotype)
        {
            ret += $"{item + 1} ";
        }
        ret += "}";
        return ret;
    }
    public virtual string GetTypeOfOrganism()
    {
        return "Organism";
    }
    public virtual bool IsVaild
    {
        get
        {
            return true;
        }
    }

    public int CompareTo(Organism other)
    {
        return this.Fitness.CompareTo(other.Fitness);
    }
}