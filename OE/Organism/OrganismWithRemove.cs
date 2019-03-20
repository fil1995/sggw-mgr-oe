using System;

class OrganismWithRemove : Organism
{
    public override double Fenotyp
    {
        get
        {
            return -2 + (genotype * 1.0 / 4000000000.0);
        }
    }

    public override void SetRandomGenotype(Random r)
    {
        base.SetRandomGenotype(r);
        this.genotype %= 4000000000u;
    }

    public override Organism RecombinationWithMutation(Organism b, Random r, double prawdopodobienstwo = 0.1)
    {
        Organism o = Recombination(this, b, r).Mutation(r, prawdopodobienstwo);
        if (o.IsVaild)
        {
            return o;
        }
        else
        {
            // jeśli coś jest nie tak, to dopuszczam tylko raz do stworzenia kolejnego osobnika
            return Recombination(this, b, r).Mutation(r, prawdopodobienstwo);
        }
    }

    public override bool IsVaild
    {
        get
        {
            if (genotype > 4000000000)
            {
                Console.WriteLine("Niepoprawny!!!");
                return false;
            }
            return true;
        }
    }

    public override string GetTypeOfOrganism()
    {
        return "OrganismWithRemove";
    }
}