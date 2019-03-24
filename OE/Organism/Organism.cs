using System;


class Organism:IComparable<Organism>
{
    protected uint genotype;
    protected double? functionVal;
    public virtual double Fenotyp
    {
        get
        {
            return -2 + (genotype * 4.0 / (double)uint.MaxValue);
        }
    }
    public virtual double Function
    {
        get
        {
            if(!functionVal.HasValue)
                functionVal= this.Fenotyp * Math.Sin(this.Fenotyp) * Math.Sin(10 * this.Fenotyp);
            return functionVal.Value;
        }
    }
    public Organism()
    {
    }

    public virtual void SetRandomGenotype(Random r)
    {
        //this.genotype = (uint)r.Next() * 2;
        // losuje najpierw 30 bitow, potem jeszcze dwa pozostałe
        genotype = ((uint)r.Next(1 << 30) << 2) | (uint)r.Next(1 << 2);
        functionVal = null;
    }

    static public Organism Recombination(Organism a, Organism b, Random r)
    {
        // losujemy punkt podziału
        int cutPoint = r.Next(1, 31); // losuje taka, aby nie wziąć wszystkiego z jednego rodzica

        uint mask = ~0u >> cutPoint;

        Organism o = (Organism)a.MemberwiseClone();
        o.genotype = (a.genotype & mask) | (b.genotype & ~mask);

        o.functionVal = null; // aby od nowa policzyła sie funkcja, bo zmienił się genotyp
        return o; //return new Organism((a.genotype & mask) | (b.genotype & ~mask));
    }
    public Organism Mutation(Random r, double prawdopodobienstwo = 0.1)
    {
        // mutujemy jeden bit
        if (r.NextDouble() <= prawdopodobienstwo) // 10% wyników
        {
            genotype ^= (1u << r.Next(0, 32));
        }
        this.functionVal = null; // aby od nowa policzyła sie funkcja, bo zmienił się genotyp
        return this;
    }
    public virtual Organism RecombinationWithMutation(Organism b, Random r, double prawdopodobienstwo = 0.1)
    {
        return Recombination(this, b, r).Mutation(r,prawdopodobienstwo);
    }
    public static Organism Better(Organism o1, Organism o2)
    {
        if (o1.Function > o2.Function)
            return o1;
        else
            return o2;
    }
    public Organism Better(Organism o2)
    {
        if (this.Function > o2.Function)
            return this;
        else
            return o2;
    }
    public override string ToString()
    {
        return String.Format("f({0})={1}", Fenotyp, Function);
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
    public virtual Organism Repair()
    {
        return this;
    }

    public int CompareTo(Organism other)
    {
        return this.Function.CompareTo(other.Function);
    }
}