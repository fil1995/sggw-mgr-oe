using System;

class OrganismFactory<TOrganism> where TOrganism : Organism, new()
{
    public TOrganism CreateOrganism(Random r)
    {
        TOrganism o = new TOrganism();
        o.SetRandomGenotype(r);
        return o;
    }
}