using System;
using System.Collections.Generic;
using System.Text;

abstract class Mutation
{
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
