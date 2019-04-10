using System;
using System.Collections.Generic;
using System.Text;


class MutationAdjacencyUsingPathTwoOpt : Mutation
{
    public override GenotypeRepresentation genotypeRepresentation => GenotypeRepresentation.ADJACENCYLIST;
    public MutationAdjacencyUsingPathTwoOpt(double mutationPercentage) : base(mutationPercentage) { }
    public override void Mutate(Organism o, Random r)
    {
        if (o.genotypeRepresentation != genotypeRepresentation)
            throw new NotImplementedException("Reprezentacja genotypu nie zgadza sie z metodą mutacji");

        o.ConvertToPath();
        Mutation mut = new MutationPathTwoOpt(mutationPercentage);
        mut.Mutate(o, r);
        o.ConvertToAdjecency();
        

    }
}
