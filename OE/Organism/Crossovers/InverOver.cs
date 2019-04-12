using System;
using System.Collections.Generic;
using System.Text;

class InverOver : Crossover
{
    double p;
    public override GenotypeRepresentation genotypeRepresentation => GenotypeRepresentation.PATH;
    public override bool PopulationOrParents => true;
    public InverOver(double p)
    {
        this.p = p;
    }
    public override void Cross(Organism[] population, Random r)
    {
        // dla każdego osobnika w populacji
        for (int i = 0; i < population.Length; i++)
        {
            uint[] s_ = population[i].genotype;
            // wybierz losowo miasto c z s_
            uint c = s_[r.Next(s_.Length)];

            uint c_;
            while (true)
            {
                if (r.NextDouble() < p)
                {
                    // losuje drugie miasto w s_
                    c_ = s_[r.Next(s_.Length)];

                }
                else
                {
                    // wybierz losowo osobnika z populacji
                    int tmpIndex = r.Next(population.Length);
                    while (i == tmpIndex) // nie mogę wybrać tego samego
                    {
                        tmpIndex = r.Next(population.Length);
                    }
                    uint[] tmp = population[tmpIndex].genotype;

                    // przypisz  do c_ miasto "za" mistem  c w wybranym osobniku
                    c_ = CityAfterCity(tmp, c);
                }

                if (HasNeighbour(s_, c, c_))
                {
                    break;
                }

                //// odwróć w s_ ciąg od miasta za c do miasta c_
                s_ = InvertSubTour(s_, FindIndex(s_, c), FindIndex(s_, c_));

                c = c_;
            } 

            // 
            Organism created = new Organism(population[0].TSPcities, population[0].genotypeRepresentation);
            created.genotype = s_;
            if (!created.IsVaild) throw new Exception("Błąd w InverOver przy tworzeniu osobnika");

            if (created.Distance < population[i].Distance)
            {
                population[i] = created;
            }

        }

    }
    uint CityAfterCity(uint[] o, uint city)
    {
        for (uint i = 0; i < o.Length; i++)
        {
            if (o[i] == city)
            {
                // jeśli to miasto jest na końcu, to zwracam pierwsze
                if (i == o.Length - 1)
                {
                    return o[0];
                }
                else
                {
                    return o[i + 1];
                }
            }
        }
        throw new Exception("Nie ma takiego miasta - błąd w genotypie");
    }
    bool HasNeighbour(uint[] genotype, uint mainCity, uint city)
    {
        uint index = FindIndex(genotype, mainCity);

        // jeśli index jest na początku
        if (index == 0)
        {
            if (genotype[1] == city || genotype[genotype.Length - 1] == city)
            {
                return true;
            }
        }
        else
        // jeśli jesteśmy na końcu
        if (index == genotype.Length - 1)
        {
            if (genotype[0] == city || genotype[genotype.Length - 2] == city)
            {
                return true;
            }
        }
        else
        // w środku już jest normalnie
        if (genotype[index - 1] == city || genotype[index + 1] == city)
        {
            return true;
        }


        return false;
    }
    uint FindIndex(uint[] genotype, uint city)
    {
        // znalezienie indeksu 
        for (uint i = 0; i < genotype.Length; i++)
        {
            if (genotype[i] == city)
            {
                return i;
            }
        }
        throw new Exception("Nie ma takiego miasta");
    }
    uint[] InvertSubTour(uint[] Genotype, uint From, uint To)
    {
        uint[] genotype = Genotype;
        if (From == To) return genotype;

        uint from = From, to = To;
        
        // jeśli odwracamy sciezke, ale zaczynamy pozniej niż konczymy
        from++;
        uint length = 0;
        if (from > to)
        {
            length += (uint)(genotype.Length - (int)from);
            length += to;
        }
        else
        {
            length = to - from;
        }
        for (uint j = 0; j <= length / 2; j++)
        {
            uint X = (from + j) % (uint)genotype.Length;
            uint Y = (from + length - j) % (uint)genotype.Length;
            // Console.WriteLine($"zamieniam {X} z {Y}");
            uint tmp = genotype[X];
            genotype[X] = genotype[Y];
            genotype[Y] = tmp;
        }
        return genotype;
    }

}
