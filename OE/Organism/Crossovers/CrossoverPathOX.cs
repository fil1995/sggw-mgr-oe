using System;


// naprzemienne wybieranie krawędzi
class CrossoverPathOX : Crossover
{
    public override GenotypeRepresentation genotypeRepresentation => GenotypeRepresentation.PATH;
    public override Organism Cross(Organism a, Organism b, Random r)
    {
        if (a.genotypeRepresentation != genotypeRepresentation || b.genotypeRepresentation != genotypeRepresentation)
            throw new NotImplementedException("Reprezentacja genotypu nie zgadza sie z metodą krzyzowania");

        uint?[] genotype = new uint?[a.TSPcities.Length];
        bool[] used = new bool[a.TSPcities.Length];

        // losujemy punkty podziału
        int cutPoint1 = r.Next(a.genotype.Length);
        int cutPoint2 = r.Next(a.genotype.Length);
        // nie mogą być takie same
        while (cutPoint1==cutPoint2)
        {
            cutPoint2 = r.Next(a.genotype.Length);
        }
        if (cutPoint1 > cutPoint2)
        {
            int tmp = cutPoint1;
            cutPoint1 = cutPoint2;
            cutPoint2 = tmp;
        }
        //Console.WriteLine($"Cut point: {cutPoint1} do {cutPoint2}");

        // przepisuje wartości z tego przedziału do dziecka z a
        for (int i = cutPoint1; i < cutPoint2; i++)
        {
            genotype[i] = a.genotype[i];
            used[a.genotype[i]] = true; // ustawiam, że miasto jest już użyte
        }
        int indexGetFromB = cutPoint2;

        // teraz wypełniam po kolei z rodzica b, ale pomijam te co już były użyte
        for (int i = cutPoint2; i < a.genotype.Length; i++)
        {
            //while (HasCity(genotype,b.genotype[indexGetFromB]))
            while (used[b.genotype[indexGetFromB]])
            {
                indexGetFromB++;
                if (indexGetFromB == b.genotype.Length) indexGetFromB = 0;
            }
            genotype[i] = b.genotype[indexGetFromB];
            used[b.genotype[indexGetFromB]] = true;
        }

        for (int i = 0; i < cutPoint1; i++)
        {
            while (used[b.genotype[indexGetFromB]])
            {
                indexGetFromB++;
                if (indexGetFromB == b.genotype.Length) indexGetFromB = 0;
            }
            genotype[i] = b.genotype[indexGetFromB];
            used[b.genotype[indexGetFromB]] = true;
        }


        Organism o = new Organism(a.TSPcities, a.genotypeRepresentation);
        for (int i = 0; i < genotype.Length; i++)
        {
            o.genotype[i] = genotype[i].Value;
        }
        return o;
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
