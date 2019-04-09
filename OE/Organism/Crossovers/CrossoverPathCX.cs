using System;
using System.Collections.Generic;
using System.Text;

// naprzemienne wybieranie krawędzi
class CrossoverPathCX : Crossover
{
    public override GenotypeRepresentation genotypeRepresentation => GenotypeRepresentation.PATH;
    public override Organism Cross(Organism a, Organism b, Random r)
    {
        if (a.genotypeRepresentation != genotypeRepresentation || b.genotypeRepresentation != genotypeRepresentation)
            throw new NotImplementedException("Reprezentacja genotypu nie zgadza sie z metodą krzyzowania");

        uint?[] genotype = new uint?[a.TSPcities.Length];

        bool cycleReached = false;
        uint index = 0;
        uint value = a.genotype[index];
        uint nextIndex = CityIndex(a.genotype, b.genotype[index]);

        while (!HasCity(genotype,b.genotype[index]))
        {
            genotype[index] = a.genotype[index];
            uint position = CityIndex(a.genotype, b.genotype[index]);
            genotype[position] = b.genotype[index];
            index = position;

        }



        while (!cycleReached)
        {
            Console.WriteLine($"index:{index} value:{value} nextIndex:{nextIndex}");

            genotype[index] = value;
            index = nextIndex;
            value = a.genotype[index];
            nextIndex = CityIndex(a.genotype, b.genotype[index]);

            if (nextIndex==0)
            {
                cycleReached = true;
            }
        }

        // teraz tam gdzie nulle, to przepisuje z rodzica b
        for (int i = 0; i < b.genotype.Length; i++)
        {
            if (!genotype[i].HasValue)
            {
                genotype[i] = b.genotype[i];
            }
        }


        Organism o = new Organism(a.TSPcities, a.genotypeRepresentation);
        for (int i = 0; i < genotype.Length; i++)
        {
            o.genotype[i] = genotype[i].Value;
        }
        return o;
    }
    uint CityIndex(uint[] genotype, uint number)
    {
        for (uint i = 0; i < genotype.Length; i++)
        {
            if (genotype[i] == number)
            {
                return i;
            }
        }
        throw new NotImplementedException("Nie ma miasta o takim indeksie");
    }
    bool HasCity(uint?[] genotype, uint number)
    {
        for (int i = 0; i < genotype.Length; i++)
        {
            if (genotype[i].HasValue && genotype[i].Value == number)
            {
                return true;
            }
        }
        return false;
    }

}
