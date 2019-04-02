using System;
using System.Collections.Generic;
using System.Linq;


class TestAlgorithm<TOrganism> where TOrganism : Organism,new()
{
    Algorithm a;
    public TestAlgorithm(Algorithm a)
    {
        this.a = a;
        RunTest();
    }
    void RunTest()
    {
        int minIloscUruchomien = 10;
        int maxIloscUruchomien = 50;
        double dokladnosc = 0.00001;
        double aktDokladnosc = 1;
        int i = 0;
        List<double> wyniki = new List<double>();
        while (minIloscUruchomien > 0 || maxIloscUruchomien > 0 || aktDokladnosc > dokladnosc)
        {
            i++;
            a.Run<TOrganism>();
            double aktualny = a.Result().Fitness;

            wyniki.Add(aktualny);

            if (i > 3)
            {
                aktDokladnosc = Math.Abs(wyniki[wyniki.Count - 1] - wyniki[wyniki.Count - 2]);
            }

            minIloscUruchomien--;
            maxIloscUruchomien--;
            //Console.Write(".");
        }
        Console.WriteLine();
        Console.WriteLine("Średnia to: " + wyniki.Average());
        wyniki.Sort();
        Console.WriteLine("Mediana to: " + Mediana(wyniki));
        Console.WriteLine("Odchylenie standardowe to: " + OdchylenieStandardowe(wyniki));
        Console.WriteLine("Przedział ufności dla 95% wyników ze średniej to: " + PrzedzialUfnosci(wyniki).ToString());
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