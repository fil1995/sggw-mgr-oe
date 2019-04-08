using System;
using System.Collections.Generic;
using System.Text;


class MutationOridinalOnePoint:Mutation
{
    public override GenotypeRepresentation genotypeRepresentation => GenotypeRepresentation.ORDINAL;
    public MutationOridinalOnePoint(double mutationPercentage) : base(mutationPercentage) { }
    public override void Mutate(Organism o, Random r)
    {
        // mutujemy jeden bit
        if (r.NextDouble() <= mutationPercentage) // 10% wyników
        {
            // na ktorej pozycji
            int point = r.Next(o.genotype.Length);
            // jaka wartosc
            o.genotype[point] = (uint)r.Next(o.genotype.Length - point);
        }
    }
}
