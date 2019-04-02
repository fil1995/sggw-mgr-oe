using System;
using System.Collections.Generic;

class Organism:IComparable<Organism>
{
    // w genotypie mam reprezentacje porządkową, liczę od 0
    protected uint[] genotype;
    protected Cities TSPcities;

    protected double? cacheFitness;

    // fenotyp to reprezentacja normalna - sieżkowa 
    public virtual uint[] Phenotype
    {
        get
        {
            uint[] phenotype = new uint[genotype.Length];

            List<uint> list = new List<uint>();
            for (uint i = 0; i < TSPcities.Length; i++)
            {
                list.Add(i + 1);
            }

            for (int i = 0; i < phenotype.Length; i++)
            {
                phenotype[i] = list[(int)genotype[i]];
                list.RemoveAt((int)genotype[i]);
            }

            return phenotype;
            // return -2 + (genotype * 4.0 / (double)uint.MaxValue);
        }
    }
    public virtual double Fitness
    {
        get
        {
            if (!cacheFitness.HasValue)
                cacheFitness = TSPcities.Distance(Phenotype);
            return cacheFitness.Value;
        }
    }
    public Organism()
    {
    }

    public virtual void SetRandomGenotype(Random r, Cities c)
    {
        this.TSPcities = c;
        genotype = new uint[c.Length];

        // losowe ustawienie
        for (uint i = 0; i < genotype.Length; i++)
        {
            genotype[i] = (uint)r.Next(genotype.Length - (int)i);
        }

        cacheFitness = null;
    }

    static public Organism Recombination(Organism a, Organism b, Random r)
    {
        // losujemy punkt podziału
        int cutPoint = r.Next(b.genotype.Length); // losuje taka, aby nie wziąć wszystkiego z jednego rodzica

        Organism o = (Organism)a.MemberwiseClone();

        // musze wpisac część z b do genotypu
        for (int i = cutPoint; i < b.genotype.Length; i++)
        {
            o.genotype[i] = b.genotype[i];
        }

        o.cacheFitness = null; // aby od nowa policzyła sie funkcja, bo zmienił się genotyp
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
            genotype[point] = (uint)r.Next(genotype.Length-point);
        }
        this.cacheFitness = null; // aby od nowa policzyła sie funkcja, bo zmienił się genotyp
        return this;
    }
    public virtual Organism RecombinationWithMutation(Organism b, Random r, double prawdopodobienstwo = 0.1)
    {
        return Recombination(this, b, r).Mutation(r,prawdopodobienstwo);
    }
    public static Organism Better(Organism o1, Organism o2)
    {
        if (o1.Fitness > o2.Fitness)
            return o1;
        else
            return o2;
    }
    public Organism Better(Organism o2)
    {
        if (this.Fitness > o2.Fitness)
            return this;
        else
            return o2;
    }
    public override string ToString()
    {
        return String.Format("f({0})={1}", Phenotype, Fitness);
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