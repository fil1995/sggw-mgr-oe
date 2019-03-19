using System;
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// LAB 03
/// Stworzyć trzy wersje algorytmu implementując wybrane strategie radzenia sobie z ograniczeniami.
/// 
/// tu robie 3 różne możliwości, czyli jedno takie, że 
/// Ograniczenie dziedziny, tak, aby nie możliwe były osobniki spoza
/// usuwam te spoza dziedziny i losuję nowe zamiast nich
/// naprawiam te, które są spoza dziedziny - np odbijanie, lub odejmowanie jakiejś wartości - nie obcinamy, bo wtedy możemy dążyć do jakiejś wartości niepotrzebnie
/// 
/// 
//Rozbudować program tak, aby móc stwierdzić, jak szybko program osiąga osobnika o wartości 80%, 90% i 95% najlepszego.
/// czyli odpalamy algorytm mamy po 1000 epokach watość 10, czyli w którym pokoleniu był taki co miał wartość 8? 9 ? itp...

//Rozbudować program tak, aby przebadać działanie programu przy różnym kryterium stopu zależnego od:
//- liczby pokoleń,
//- czasu obliczeń,
//- stopienia poprawy w k ostatnich pokoleń,
//- różnorodności populacji.
/// 
/// </summary>


class Program
{
    private static int licznik = 1000;
    private static int osobnikow = 20;
    private static uint[] populacja = new uint[osobnikow];
    private static Random random = new Random();

    static void Main(string[] args)
    {
        // Algorithm a1 = new Algorithm(random, new StopNumEpochs(10));
        Algorithm a1 = new Algorithm(random, new StopNumEpochs(100));
        a1.Run<Organism>();
        double aktualny1 = a1.Result().Fenotyp;


        Console.ReadKey();
        return;


        int minIloscUruchomien = 10;
        int maxIloscUruchomien = 50;
        double dokladnosc = 0.00001;
        double aktDokladnosc = 1;
        int i = 0;
        List<double> wyniki = new List<double>();
        while (minIloscUruchomien > 0 || maxIloscUruchomien > 0 ||  aktDokladnosc > dokladnosc)
        {
            i++;

            //Algorithm a = new Algorithm(random, EStop.PopulationDeviation);
            //Algorithm a = new Algorithm(random, EStop.BetterLastEpochs);
            //Algorithm a = new Algorithm(random, EStop.Time);
            Algorithm a = new Algorithm(random, new StopNumEpochs(10));

            //Algorithm a = new AlgorithmWithOrganismWithRemove(random, EStop.PopulationDeviation);
            //Algorithm a = new AlgorithmWithOrganismWithRemove(random, EStop.BetterLastEpochs);
            //Algorithm a = new AlgorithmWithOrganismWithRemove(random, EStop.Time);
            //Algorithm a = new AlgorithmWithOrganismWithRemove(random, EStop.NumEpochs);

            //Algorithm a = new AlgorithmWithOrganismWithRepair(random, EStop.PopulationDeviation);
            //Algorithm a = new AlgorithmWithOrganismWithRepair(random, EStop.BetterLastEpochs);
            //Algorithm a = new AlgorithmWithOrganismWithRepair(random, EStop.Time);
            //Algorithm a = new AlgorithmWithOrganismWithRepair(random, EStop.NumEpochs);

            a.Run<Organism>();
            double aktualny = a.Result().Fenotyp;

            wyniki.Add(aktualny);

            if (i > 3)
            {
                aktDokladnosc = Math.Abs(wyniki[wyniki.Count - 1] - wyniki[wyniki.Count - 2]);
            }

            minIloscUruchomien--;
            maxIloscUruchomien--;
        }
        Console.WriteLine("Średnia to: " + wyniki.Average());
        wyniki.Sort();
        Console.WriteLine("Mediana to: " + Mediana(wyniki));
        Console.WriteLine("Odchylenie standardowe to: " + OdchylenieStandardowe(wyniki));
        Console.WriteLine("Przedział ufności dla 95% wyników ze średniej to: " + PrzedzialUfnosci(wyniki).ToString());

        Console.ReadKey();
    }
    
    static double Mediana(List<double> wyniki)
    {
        wyniki.Sort();
        if (wyniki.Count % 2 == 0)
            return wyniki[wyniki.Count / 2];
        else
            return (wyniki[wyniki.Count / 2] + wyniki[1 + (wyniki.Count / 2)]) / 2;
    }
    static double OdchylenieStandardowe(List<double> wyniki)
    {
        double srednia = wyniki.Average();
        double suma = 0;
        for (int i = 0; i < wyniki.Count; i++)
        {
            suma += Math.Pow(wyniki[i] - srednia, 2);
        }
        return Math.Sqrt(suma / wyniki.Count);
    }
    static Tuple<double, double> PrzedzialUfnosci(List<double> wyniki)
    {
        /// dla 95%
        /// 
        return new Tuple<double, double>(wyniki.Average() - OdchylenieStandardowe(wyniki) * 1.96, wyniki.Average() + OdchylenieStandardowe(wyniki) * 1.96);
    }
}