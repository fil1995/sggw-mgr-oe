using System;
using System.Collections.Generic;
using System.Text;

// naprzemienne wybieranie krawędzi
class CrossoverAdjacencyAlternatingEdges : Crossover
{
    public override GenotypeRepresentation genotypeRepresentation => GenotypeRepresentation.ADJACENCYLIST;
    public override Organism Cross(Organism a, Organism b, Random r)
    {
        if (a.genotypeRepresentation != genotypeRepresentation || b.genotypeRepresentation != genotypeRepresentation)
            throw new NotImplementedException("Reprezentacja genotypu nie zgadza sie z metodą krzyzowania");

        uint?[] genotype = new uint?[a.TSPcities.Length];


        int i = a.TSPcities.Length;
        bool[] used = new bool[a.TSPcities.Length];
        uint indexFROM = 0;
        uint valueTO = a.genotype[indexFROM];
        bool readFromA = false;
        genotype[indexFROM] = valueTO; // wpisuje pierwsza wartość
        used[indexFROM] = true;
        i--;
        //Console.WriteLine("------");
        //Console.WriteLine(GenotypeToString(a.genotype));
        //Console.WriteLine(GenotypeToString(b.genotype));
        //Console.WriteLine(GenotypeToString(genotype));

        while (i > 0)
        {
            indexFROM = genotype[indexFROM].Value; // ustawiam następny indeks
            // skąd czytać następną - a czy b?
            if (readFromA)
                valueTO = a.genotype[indexFROM];
            else
                valueTO = b.genotype[indexFROM];
            //Console.WriteLine("Nastepny index FROM: " + indexFROM + " czytam z " + (readFromA ? "A" : "B") + " valueTO: " + valueTO);

            // jeśli została nam ostatnia krawędz, to musi ona prowadzić do 0
            if (i == 1)
            {
                valueTO = 0;
                //Console.WriteLine("Nastepny index FROM: " + indexFROM + " czytam z " + (readFromA ? "A" : "B") + " valueTO: " + valueTO +" FIX ostatnia krawedz");
            }
            else
            {   // jeśli okazuje się, że robimy cykl, to biore inna nieuzyta krawedz
                if (used[valueTO])
                {
                    //Console.WriteLine($"valueTo = {valueTO} jest w genotypie - szukam nowej");
                    valueTO = FindNotUsedEdge(genotype, used, indexFROM, a, b);
                    //Console.WriteLine("Nastepny index FROM: " + indexFROM + " czytam z " + (readFromA ? "A" : "B") + " valueTO: " + valueTO + " FIX");
                }
            }
            if (i > 1 && used[valueTO]) throw new Exception("nadpisuje");
            genotype[indexFROM] = valueTO;
            used[indexFROM] = true;
            //Console.WriteLine(GenotypeToString(genotype));
            i--;
            readFromA = !readFromA;
        }
        //Console.WriteLine("-----------");
        Organism o = new Organism(a.TSPcities, a.genotypeRepresentation);
        for (int j = 0; j < genotype.Length; j++)
        {
            o.genotype[j] = genotype[j].Value;
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

    uint FindNotUsedEdge(uint?[] genotype, bool[] used, uint actualIndexFrom, Organism a, Organism b)
    {
        //Console.WriteLine("--- Szukanie nieużytej krawędzi");
        for (uint i = 0; i < genotype.Length; i++)
        {
            // nie może być z samego do siebie
            if (i == actualIndexFrom)
            {
                //Console.WriteLine($"{i} - z samego do siebie nie");
                continue;
            }
            // jeśli jest to miasto gdzie już jest coś wpisane - też nie mogę
            if (used[i])
            {
                //Console.WriteLine($"{i} - used");
                continue;
            }
            // jeśli jest to miasto, które nie jest wpisane, to sprawdzam jeszcze czy nie ma go już w genotypie i wtedy mogę tam iść
            if (!HasCity(genotype, i))
            {
                //Console.WriteLine($"{i} - ok");
                return i;
            }
        }
        throw new Exception("Nie ma wolnej krawędzi");
    }

    string GenotypeToString(uint[] g)
    {
        string ret = "{";
        foreach (uint? item in g)
        {
            if (item.HasValue)
                ret += String.Format("{0,2} ", item);
            else ret += "xx ";
        }
        ret += "}";
        return ret;
    }
    string GenotypeToString(uint?[] g)
    {
        string ret = "{";
        foreach (uint? item in g)
        {
            if (item.HasValue)
                ret += String.Format("{0,2} ", item.Value);
            else ret += "xx ";
        }
        ret += "}";
        return ret;
    }
}
