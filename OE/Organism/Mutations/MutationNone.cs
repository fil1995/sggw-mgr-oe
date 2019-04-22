using System;

class MutationNone : Mutation
{
    public override GenotypeRepresentation genotypeRepresentation => GenotypeRepresentation.PATH;
    public MutationNone(double mutationPercentage) : base(mutationPercentage) { }
    public override void Mutate(Organism o, Random r)
    {

    }
}
