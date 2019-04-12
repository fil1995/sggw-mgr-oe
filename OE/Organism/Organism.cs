using System;
using System.Collections.Generic;

class Organism : IComparable<Organism>
{
    public uint[] genotype;
    public GenotypeRepresentation genotypeRepresentation { get; set; }

    public Cities TSPcities;

    protected double? cacheDistance;

    public Organism(Cities c, GenotypeRepresentation gr)
    {
        TSPcities = c;
        genotype = new uint[TSPcities.Length];
        genotypeRepresentation = gr;
        cacheDistance = null;
    }
    public Organism(Cities c, GenotypeRepresentation gr, Random r)
    {
        TSPcities = c;
        genotype = new uint[TSPcities.Length];
        genotypeRepresentation = gr;
        SetRandomGenotype(r);
        cacheDistance = null;
    }


    public uint[] Phenotype
    {
        get
        {
            switch (genotypeRepresentation)
            {
                case GenotypeRepresentation.PATH:
                    return genotype;
                case GenotypeRepresentation.ORDINAL:
                    return OrdinalToPath(genotype);
                case GenotypeRepresentation.ADJACENCYLIST:
                    return AdjecencyListToPath(genotype);
                default:
                    throw new NotImplementedException("Genotype representation error");
            }

        }
    }

    public virtual double Fitness
    {
        get
        {
            return 1 / Distance;
        }
    }
    public virtual double Distance
    {
        get
        {
            if (!cacheDistance.HasValue)
                cacheDistance = TSPcities.Distance(Phenotype);
            return cacheDistance.Value;
        }
    }


    private uint[] OrdinalToPath(uint[] ordinal)
    {
        uint[] ret = new uint[ordinal.Length];

        List<uint> list = new List<uint>();
        for (uint i = 0; i < ordinal.Length; i++)
        {
            list.Add(i);
        }

        for (int i = 0; i < ret.Length; i++)
        {
            ret[i] = list[(int)ordinal[i]];
            list.RemoveAt((int)ordinal[i]);
        }
        return ret;
    }
    private uint[] PathToOrdinal(uint[] path)
    {
        uint[] ret = new uint[path.Length];

        List<uint> list = new List<uint>();
        for (uint i = 0; i < path.Length; i++)
        {
            list.Add(i);
        }

        for (int i = 0; i < path.Length; i++)
        {
            ret[i] = (uint)list.IndexOf(path[i]);
            list.RemoveAt((int)ret[i]);
        }

        return ret;
    }
    private uint[] AdjecencyListToPath(uint[] adjecencyList)
    {
        uint[] ret = new uint[adjecencyList.Length];

        ret[0] = 0;
        for (int i = 1; i < adjecencyList.Length; i++)
        {
            ret[i] = adjecencyList[ret[i - 1]];
        }

        return ret;
    }
    // wszędzie liczę miasta od 0
    private uint[] PathToAdjecencyList(uint[] path)
    {
        uint[] ret = new uint[path.Length];

        for (int i = 0; i < path.Length - 1; i++)
        {
            uint from = path[i];
            uint to = path[i + 1];
            ret[from] = to;
        }
        // na końcu dodaję pętle
        ret[path[path.Length - 1]] = path[0];

        return ret;
    }

    public void SetRandomGenotype(Random r)
    {
        // losowe ustawienie
        for (int i = 0; i < genotype.Length; i++)
        {
            genotype[i] = (uint)r.Next(genotype.Length - (int)i);
        }

        switch (genotypeRepresentation)
        {
            case GenotypeRepresentation.PATH:
                genotype = OrdinalToPath(genotype);
                break;
            case GenotypeRepresentation.ORDINAL:
                // to jest ok
                break;
            case GenotypeRepresentation.ADJACENCYLIST:
                genotype = PathToAdjecencyList(OrdinalToPath(genotype));
                break;
            default:
                break;
        }

        cacheDistance = null;
    }
    public void ConvertToOrdinal()
    {
        switch (genotypeRepresentation)
        {
            case GenotypeRepresentation.PATH:
                genotype = PathToOrdinal(genotype);
                genotypeRepresentation = GenotypeRepresentation.ORDINAL;
                break;
            case GenotypeRepresentation.ORDINAL:
                // to jest ok
                break;
            case GenotypeRepresentation.ADJACENCYLIST:
                genotype = PathToOrdinal(AdjecencyListToPath(genotype));
                genotypeRepresentation = GenotypeRepresentation.ORDINAL;
                break;
            default:
                break;
        }
    }
    public void ConvertToPath()
    {
        switch (genotypeRepresentation)
        {
            case GenotypeRepresentation.PATH:
                // to jest ok
                break;
            case GenotypeRepresentation.ORDINAL:
                genotype = OrdinalToPath(genotype);
                genotypeRepresentation = GenotypeRepresentation.PATH;
                // to jest ok
                break;
            case GenotypeRepresentation.ADJACENCYLIST:
                genotype = AdjecencyListToPath(genotype);
                genotypeRepresentation = GenotypeRepresentation.PATH;
                break;
            default:
                break;
        }
    }
    public void ConvertToAdjecency()
    {
        switch (genotypeRepresentation)
        {
            case GenotypeRepresentation.PATH:
                genotype = PathToAdjecencyList(genotype);
                genotypeRepresentation = GenotypeRepresentation.ADJACENCYLIST;
                break;
            case GenotypeRepresentation.ORDINAL:
                genotype = PathToAdjecencyList(OrdinalToPath(genotype));
                genotypeRepresentation = GenotypeRepresentation.ADJACENCYLIST;
                // to jest ok
                break;
            case GenotypeRepresentation.ADJACENCYLIST:
                // to jest ok
                break;
            default:
                break;
        }
    }
    public Organism Better(Organism o2)
    {
        if (this.Fitness > o2.Fitness)
            return this;
        else
            return o2;
    }
    public override string ToString()
    {
        string ret = "{";
        foreach (uint item in Phenotype)
        {
            ret += String.Format("{0,2} ", item);
        }
        ret += "}-Phenotype";
        return ret;
    }
    public string Genotype
    {
        get {
            string ret = "{";
            foreach (uint item in genotype)
            {
                ret += String.Format("{0,2} ", item);
            }
            ret += "}-Genotype";
            return ret;
        }
    }
    public virtual string GetTypeOfOrganism()
    {
        return "Organism";
    }
    public virtual bool IsVaild
    {
        get
        {
            
            // sprawdzam czy fenotyp ma wszystkie miasta
            List<uint> list = new List<uint>();
            for (uint i = 0; i < genotype.Length; i++)
            {
                list.Add(i);
            }
            foreach (uint city in Phenotype)
            {
                list.Remove(city);
            }
            if (list.Count == 0) return true;
            else
            {

                throw new Exception("Niepoprawny osobnik");
                return false;
            }

        }
    }

    public int CompareTo(Organism other)
    {
        return this.Fitness.CompareTo(other.Fitness);
    }
}