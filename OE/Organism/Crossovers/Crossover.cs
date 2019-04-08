using System;
using System.Collections.Generic;
using System.Text;

abstract class Crossover
{
    public virtual GenotypeRepresentation genotypeRepresentation => throw new NotImplementedException("define genotypeRepresentation");

    public virtual Organism Cross(Organism a, Organism b, Random r)
    {
        throw new NotImplementedException("implement Cross");
    }
}
