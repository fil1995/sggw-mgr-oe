using System;
/// <summary>
/// Tu każdemu osobnikowi przypisujemy ustaloną wartość - jeśli jakiś będzie bardzo odstawał od pozostałych, to nie wyprze ich aż tak mocno
/// </summary>
class SelectionRouletteRank:SelectionType
{
    public override Organism Select()
    {
        int maxVal= a.population.Length * (1+ a.population.Length) / 2; // suma ciągu arytmetycznego
        int rouletteVal = r.Next(maxVal);

        // sortowanie populacji
        QuickSort(a.population, 0, a.population.Length-1);

        /// losowanie
        double currentSum = 0;
        for (int i = 0; i < a.population.Length; i++)
        {
            currentSum += i + 1;
            if (currentSum >= rouletteVal)
                return a.population[i];
        }

        throw new Exception("RouletteRank selection error");
    }

    static void QuickSort(Organism[] population, int left, int right)
    {
        int i = left;
        int j = right;
        double pivot = population[(left + right) / 2].Function;
        while (i < j)
        {
            while (population[i].Function < pivot) i++;
            while (population[j].Function > pivot) j--;
            if (i <= j)
            {
                // zamiana
                Organism tmp = population[i];
                population[i++] = population[j];
                population[j--] = tmp;
            }
        }
        if (left < j) QuickSort(population, left, j);
        if (i < right) QuickSort(population, i, right);
    }

}
