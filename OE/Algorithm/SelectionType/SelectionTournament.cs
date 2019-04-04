using System;

class SelectionTournament:SelectionType
{
    public override bool NeedSortedPopulation => false;
    public override Organism Select(Random r, Organism[] population)
    {
        int losA = r.Next(0, population.Length);
        int losB = r.Next(0, population.Length);
        int i = 0;
        while (losA == losB)
        {
            losB = r.Next(0, population.Length);
            if (i == 5)
            {
                return population[losA];
            }
            i++;
        }

        return population[losA].Better(population[losB]);
    }
}
