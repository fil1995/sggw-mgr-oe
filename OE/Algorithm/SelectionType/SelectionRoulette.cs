using System;

class SelectionRoulette:SelectionType
{
    public override bool NeedSortedPopulation => false;

    public override Organism Select()
    {
        double[] populationFunctions = new double[a.population.Length];
        int i_min = 0;
        
        // obliczam funkcje dla całej populacji i wartości min
        for (int i = 0; i < populationFunctions.Length; i++)
        {
            populationFunctions[i] = a.population[i].Function;
            if (populationFunctions[i] < populationFunctions[i_min]) i_min = i;
        }

        if (populationFunctions[i_min]<0)
        {
            double min = populationFunctions[i_min];
            // pozbywam sie wartości ujemnych
            for (int i = 0; i < populationFunctions.Length; i++)
            {
                populationFunctions[i] -= min;
            }
        }
        double sum = 0;
        /// liczymy sume populacji
        for (int i = 0; i < populationFunctions.Length; i++)
        {
            sum += populationFunctions[i];
        }

        // mam wartość max z całego zbioru i min>=0
        // losuje liczbe z tego przedziału;
        double rouletteVal = r.NextDouble() * sum;


        /// losowanie
        double currentSum = 0;
        for (int i = 0; i < populationFunctions.Length; i++)
        {
            currentSum += populationFunctions[i];
            if (currentSum >= rouletteVal)
                return a.population[i];
        }

        throw new Exception("Roulette selection error");
    }
}
