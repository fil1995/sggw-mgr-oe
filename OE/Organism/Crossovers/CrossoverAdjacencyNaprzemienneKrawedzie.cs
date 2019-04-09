using System;
using System.Collections.Generic;
using System.Text;

// naprzemienne wybieranie krawędzi
class CrossoverAdjacencyNaprzemienneKrawedzie : Crossover
{
    public override GenotypeRepresentation genotypeRepresentation => GenotypeRepresentation.ADJACENCYLIST;
    public override Organism Cross(Organism a, Organism b, Random r)
    {
        if (a.genotypeRepresentation != genotypeRepresentation || b.genotypeRepresentation != genotypeRepresentation)
            throw new NotImplementedException("Reprezentacja genotypu nie zgadza sie z metodą krzyzowania");

        Organism o = new Organism(a.TSPcities, a.genotypeRepresentation);
        int i = a.TSPcities.Length;

        bool[] used = new bool[a.TSPcities.Length];
        uint index = 0;
        uint value = a.genotype[index];
        bool readFromA = false;
        o.genotype[index] = value; // wpisuje pierwsza wartość
        used[index]= true;
        i--;

        while (i > 0)
        {
            bool earlyLoop = true;
            uint earlyLoopIndex = 1; // bo 0 jest zawsze uzyte
            index = value; // ustawiam nowy index w kolejnym rodzicu
            while (earlyLoop)
            {
                // skąd czytać następną - a czy b?
                if (readFromA)
                    value = a.genotype[index];
                else
                    value = b.genotype[index];
                // muszę sprawdzić, czy nie trafię zbyt wczesną pętlę. Jeśli tak, to muszę wylosować nowy index
                if (i==1) // to znaczy, że zostało tylko jedno miasto, więc musimy zrobić pętlę
                {
                    earlyLoop = false;
                    break;
                }
                // jeśli wracam zbyt wcześnie do tego który został już użyty
                // lub co wazniejsze, czy nie ide sam do siebie
                if (used[value])
                {
                    // losuje nowy index
                    // z tych co jeszcze zostały wolne
                    do
                    {
                        index = earlyLoopIndex;
                    } while (used[index]); // przesuwaj index do takiego, co nie został użyty jeszcze


                }


            }

            //wpisz
            o.genotype[index] = value;
            //
            i--;
            readFromA = !readFromA; // następnym razem czytamy z drugiego rodzica
        }


        return o;
    }
}
