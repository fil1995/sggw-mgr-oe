using System;
using System.Collections.Generic;
using System.Text;

// naprzemienne wybieranie krawędzi
class CrossoverPathPMX : Crossover
{
    public override GenotypeRepresentation genotypeRepresentation => GenotypeRepresentation.PATH;
    public override Organism Cross(Organism a, Organism b, Random r)
    {
        if (a.genotypeRepresentation != genotypeRepresentation || b.genotypeRepresentation != genotypeRepresentation)
            throw new NotImplementedException("Reprezentacja genotypu nie zgadza sie z metodą krzyzowania");

        uint?[] genotype = new uint?[a.TSPcities.Length];

        // losujemy punkty podziału
        int cutPoint1 = r.Next(a.genotype.Length);
        int cutPoint2 = r.Next(a.genotype.Length);
        if (cutPoint1 > cutPoint2)
        {
            int tmp = cutPoint1;
            cutPoint1 = cutPoint2;
            cutPoint2 = tmp;
        }
        //Console.WriteLine($"Cross: {cutPoint1} do {cutPoint2}");

        // przepisuje wartości z tego przedziału do dziecka z a
        for (int i = cutPoint1; i < cutPoint2; i++)
        {
            genotype[i] = a.genotype[i];
        }
        for (int i = 0; i < cutPoint1; i++)
        {
            // wpisuje wartości, te które mogę.
            if (!HasCity(genotype, b.genotype[i]))
            {
                genotype[i] = b.genotype[i];
            }
        }
        for (int i = cutPoint2; i < a.genotype.Length; i++)
        {
            // wpisuje wartości, te które mogę.
            if (!HasCity(genotype, b.genotype[i]))
            {
                genotype[i] = b.genotype[i];
            }
        }

        // teraz zostało wypełnić nulle
        for (int i = 0; i < a.genotype.Length; i++)
        {
            if (!genotype[i].HasValue)
            {
                // jeśli nie ma wartości to możemy wypełnić z macierzy przejścia
                genotype[i] = Convert(a, b, b.genotype[i], cutPoint1, cutPoint2);
            }
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
    uint Convert(Organism a, Organism b, uint number, int cut1, int cut2)
    {
        for (int i = cut1; i < cut2; i++)
        {
            if (a.genotype[i] == number)
            {
                return b.genotype[i];
            }
        }
        throw new NotImplementedException("PMX - błąd w odwzorowaniach");
    }
}
