using System;

/// <summary>
/// Tu różni się tylko sposób radzenia sobie z osobnikiem z poza dziedziny  - inna funkcja
/// </summary>
class OrganismWithRepair : OrganismWithRemove
{
    public override double Fenotyp
    {
        get
        {
            return -2 + (genotype * 1.0 / 4000000000.0);
        }
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

            return o.Repair();
        }
    }
    public override void SetRandomGenotype(Random r)
    {
        base.SetRandomGenotype(r);
        this.genotype %= 4000000000u;
    }

    public override Organism Repair()
    {
        genotype -= 2 * (genotype % 4000000000);
        return this;
    }
    public override string GetTypeOfOrganism()
    {
        return "OrganismWithRepair";
    }
}