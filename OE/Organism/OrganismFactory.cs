using System;

class OrganismFactory<TOrganism> where TOrganism : Organism, new()
{
    public TOrganism CreateOrganism(Random r,Cities c)
    {
        TOrganism o = new TOrganism();
        o.SetRandomGenotype(r,c);
        return o;
    }
}