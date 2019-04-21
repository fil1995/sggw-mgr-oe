using System;
using System.Collections.Generic;
using System.Text;

abstract class Crossover
{
    public virtual string CrossoverTypeName => this.GetType().Name;
    public virtual GenotypeRepresentation genotypeRepresentation => throw new NotImplementedException("define genotypeRepresentation");
    // tu ustalam czy działam na dwóch rodzicach, czy na całej populacji
    public virtual bool PopulationOrParents => false; // jeśli true to na populacji
    public virtual Organism Cross(Organism a, Organism b, Random r)
    {
        throw new NotImplementedException("implement Cross");
    }
    public virtual void Cross(Organism[] population, Random r)
    {

        throw new NotImplementedException("implement cross for Population");
    }
}
