using System;
using System.Collections.Generic;
using System.Text;


class MutationPathTwoOpt : Mutation
{
    public override GenotypeRepresentation genotypeRepresentation => GenotypeRepresentation.PATH;
    public MutationPathTwoOpt(double mutationPercentage) : base(mutationPercentage) { }
    public override void Mutate(Organism o, Random r)
    {
        if (o.genotypeRepresentation != genotypeRepresentation)
            throw new NotImplementedException("Reprezentacja genotypu nie zgadza sie z metodą mutacji");

        // mutujemy jeden bit
        if (r.NextDouble() <= mutationPercentage) // 10% wyników
        {
            int cutPoint1 = r.Next(o.genotype.Length);
            int cutPoint2 = r.Next(o.genotype.Length);
            if (cutPoint1 > cutPoint2)
            {
                int tmp = cutPoint1;
                cutPoint1 = cutPoint2;
                cutPoint2 = tmp;
            }

            // odwracamy kolejność miast pomiędzy tymi punktami

            for (int i = cutPoint1; i < cutPoint2-((cutPoint2-cutPoint1)/2); i++)
            {
                uint tmp = o.genotype[i];
                o.genotype[i]= o.genotype[cutPoint2 - i];
                o.genotype[cutPoint2 - i] = tmp;
            }



        }
    }
}
