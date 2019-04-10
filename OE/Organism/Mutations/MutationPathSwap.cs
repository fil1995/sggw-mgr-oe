using System;
using System.Collections.Generic;
using System.Text;


class MutationPathSwap : Mutation
{
    public override GenotypeRepresentation genotypeRepresentation => GenotypeRepresentation.PATH;
    public MutationPathSwap(double mutationPercentage) : base(mutationPercentage) { }
    public override void Mutate(Organism o, Random r)
    {
        if (o.genotypeRepresentation != genotypeRepresentation)
            throw new NotImplementedException("Reprezentacja genotypu nie zgadza sie z metodą mutacji");

        // Zamieniamy dwa miasta miejscami
        if (r.NextDouble() <= mutationPercentage) // 10% wyników
        {
            int point1 = r.Next(o.genotype.Length);
            int point2 = r.Next(o.genotype.Length);

            uint tmp = o.genotype[point1];
            o.genotype[point1] = o.genotype[point2];
            o.genotype[point2] = tmp;

        }
    }
}
