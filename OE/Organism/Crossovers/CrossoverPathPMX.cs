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
        // jeśli bym wylosował te same, to losuje aż będą różne
        while (cutPoint1 == cutPoint2)
        {
            cutPoint2 = r.Next(a.genotype.Length);
        }

        if (cutPoint1 > cutPoint2)
        {
            int tmp = cutPoint1;
            cutPoint1 = cutPoint2;
            cutPoint2 = tmp;
        }
        Console.WriteLine($"a: {a}");
        Console.WriteLine($"b: {b}");
        Console.WriteLine($"Cross: {cutPoint1} do {cutPoint2}");

        // przepisuje wartości z tego przedziału do dziecka z rodzica a
        for (int i = cutPoint1; i < cutPoint2; i++)
        {
            genotype[i] = a.genotype[i];
        }
        Console.WriteLine(GenotypeToString(genotype));

        for (int i = 0; i < cutPoint1; i++)
        {
            // wpisuje wartości, te które mogę.
            if (!HasCity(genotype, b.genotype[i]))
            {
                genotype[i] = b.genotype[i];
            }
        }
        Console.WriteLine("te ktore moge 0->cut1");
        Console.WriteLine(GenotypeToString(genotype));

        for (int i = cutPoint2; i < a.genotype.Length; i++)
        {
            // wpisuje wartości, te które mogę.
            if (!HasCity(genotype, b.genotype[i]))
            {
                genotype[i] = b.genotype[i];
            }
        }
        Console.WriteLine("te ktore moge cut2->end");
        Console.WriteLine(GenotypeToString(genotype));
        Console.WriteLine("wypełniam nulle");
        // teraz zostało wypełnić nulle
        for (int i = 0; i < a.genotype.Length; i++)
        {
            if (!genotype[i].HasValue)
            {
                // jeśli nie ma wartości to możemy wypełnić z macierzy przejścia
                // trzeba sprawdzic, czy przypadkiem już nie ma wpisanego takiego miasta - wtedy wstawiam inne
                uint insert = Mapping(a, b, b.genotype[i], cutPoint1, cutPoint2);
                Console.WriteLine($"{i}: zamiana {b.genotype[i]} na {insert}");
                if (HasCity(genotype, insert)) throw new Exception("PMX - blad macierzy przejscia");
                genotype[i] = insert;
            }
        }
        Console.WriteLine("wypełniam nule");
        Console.WriteLine(GenotypeToString(genotype));

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
    uint? HasCityInRange(uint[] genotype, uint number, int cut1, int cut2)
    {
        for (int i = cut1; i < cut2; i++)
        {
            if (genotype[i] == number)
            {
                return (uint)i;
            }
        }
        return null;
    }
    uint Mapping(Organism a, Organism b, uint number, int cut1, int cut2)
    {
        for (int i = cut1; i < cut2; i++)
        {
            if (a.genotype[i] == number)
            {
                uint mapTo = b.genotype[i];
                // jeśli w A mamy już takie miasto, to patrzymy na co mamy je zamienić...
                uint? conflictId = HasCityInRange(a.genotype, mapTo, cut1, cut2);
                if (!conflictId.HasValue)
                {
                    return mapTo;
                }
                else
                {
                    Console.WriteLine("w przedziale jest juz takie miasto - rekurencja");
                    return Mapping(a, b, mapTo, cut1, cut2);
                }
            }
        }
        throw new NotImplementedException("PMX - błąd w odwzorowaniach");
    }
    string GenotypeToString(uint?[] g)
    {
        string ret = "{";
        foreach (uint? item in g)
        {
            if (item.HasValue)
                ret +=String.Format("{0,2} ", item);
            else ret += "xx ";
        }
        ret += "}";
        return ret;
    }
}
