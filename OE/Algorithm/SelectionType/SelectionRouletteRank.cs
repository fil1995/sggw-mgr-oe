using System;
/// <summary>
/// Tu każdemu osobnikowi przypisujemy ustaloną wartość - jeśli jakiś będzie bardzo odstawał od pozostałych, to nie wyprze ich aż tak mocno
/// </summary>
class SelectionRouletteRank:SelectionType
{
    public override bool NeedSortedPopulation => true;
    public override Organism Select(Random r, Organism[] population)
    {
        int maxVal= population.Length * (1+ population.Length) / 2; // suma ciągu arytmetycznego
        int rouletteVal = r.Next(maxVal);

        /// losowanie
        double currentSum = 0;
        for (int i = 0; i < population.Length; i++)
        {
            currentSum += i + 1;
            if (currentSum >= rouletteVal)
                return population[i];
        }

        throw new Exception("RouletteRank selection error");
    }
}
