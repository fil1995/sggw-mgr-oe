using System;
using System.Collections.Generic;
using System.Text;


class CrossoverOrdinalSinglePoint:Crossover
{
    public override GenotypeRepresentation genotypeRepresentation => GenotypeRepresentation.ORDINAL;
    public override Organism Cross(Organism a, Organism b, Random r)
    {
        if (a.genotypeRepresentation!= genotypeRepresentation || b.genotypeRepresentation != genotypeRepresentation)
            throw new NotImplementedException("Reprezentacja genotypu nie zgadza sie z metodą krzyzowania");

        // losujemy punkt podziału
        int cutPoint1 = r.Next(b.genotype.Length);
        int cutPoint2 = r.Next(b.genotype.Length);
        if (cutPoint1 > cutPoint2)
        {
            int tmp = cutPoint1;
            cutPoint1 = cutPoint2;
            cutPoint2 = tmp;
        }

        // Organism o = (Organism)a.MemberwiseClone();
        Organism o = new Organism(a.TSPcities, a.genotypeRepresentation);

        // dwa pkt podziału
        for (int i = 0; i < a.genotype.Length; i++)
        {
            if (i > cutPoint1 && i < cutPoint2)
                o.genotype[i] = a.genotype[i];
            else
                o.genotype[i] = b.genotype[i];
        }
        return o; //return new Organism((a.genotype & mask) | (b.genotype & ~mask));
    }
}
