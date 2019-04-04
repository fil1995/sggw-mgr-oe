using System;
using System.Collections.Generic;
using System.Text;


abstract class SelectionType
{
    public string SelectionTypeName => this.GetType().Name;
    public virtual bool NeedSortedPopulation => throw new NotImplementedException("define NeedSortedPopulation");

    public virtual Organism Select(Random r, Organism[] population)
    {
        throw new NotImplementedException("define Select");
    }
}
