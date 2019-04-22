using System;

abstract class Mutation
{
    public string MutationTypeName => this.GetType().Name;
    public virtual GenotypeRepresentation genotypeRepresentation => throw new NotImplementedException("define genotypeRepresentation");
    public double mutationPercentage;

    public Mutation(double mutationPercentage)
    {
        this.mutationPercentage = mutationPercentage;
    }

    public virtual void Mutate(Organism o, Random r)
    {
        throw new NotImplementedException("implement Mutate");
    }
}
