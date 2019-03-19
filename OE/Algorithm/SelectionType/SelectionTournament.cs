using System;

class SelectionTournament:SelectionType
{
    public override Organism Select()
    {
        int losA = r.Next(0, a.population.Length);
        int losB = r.Next(0, a.population.Length);
        int i = 0;
        while (losA == losB)
        {
            losB = r.Next(0, a.population.Length);
            if (i == 5)
            {
                return a.population[losA];
            }
            i++;
        }

        return a.population[losA].Better(a.population[losB]);
    }
}
