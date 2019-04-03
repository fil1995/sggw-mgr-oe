using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;


class Cities
{
    City[] cities;
    public int Length => cities.Length;


    public Cities(string filename)
    {
        using (StreamReader reader = new StreamReader(filename))
        {
            uint citiesToLoad = 0;
            int i = 0;
            bool startReadCoords = false;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine().Trim();
                if (line.Length == 0) continue;

                // szukam ile jest miast do wczytania
                if (!startReadCoords && line.StartsWith("DIMENSION"))
                {
                    int index = line.IndexOf(':');
                    if (index > 0)
                    {
                        citiesToLoad = Convert.ToUInt32(line.Substring(index + 1).Trim());
                        cities = new City[citiesToLoad];
                    }
                }
                if (!startReadCoords &&  line.StartsWith("NODE_COORD"))
                {
                    startReadCoords = true;
                    Console.WriteLine($"Start reading tsp file... {citiesToLoad} cities");
                    continue;
                }

                // teraz czytam miasta
                if (startReadCoords && citiesToLoad>0)
                {
                    string[] data = line.Split(' ');
                    try
                    {
                        cities[i] = new City(int.Parse(data[0], CultureInfo.InvariantCulture),
                        double.Parse(data[1], CultureInfo.InvariantCulture),
                        double.Parse(data[2], CultureInfo.InvariantCulture));
                    }catch(Exception e)
                    {
                        Console.WriteLine($"error: pozostało {citiesToLoad}   Błąd:"+line);
                    }
                    citiesToLoad--;
                    i++;
                }
  
            }
            reader.Close();
        }
    }

    // tu jako parametr dostaje fenotyp (licze od 0)
    public double Distance(uint[] tour)
    {
        double sum = 0.0;
        for (int i = 0; i < tour.Length-1; i++)
        {
            sum += City.Distance(cities[tour[i]], cities[tour[i+1]]);
            //Console.WriteLine($"i={i} od {tour[i]}  do {tour[i+1]}  dist: {cities[tour[i]]} -> {cities[tour[i + 1]]}");
        }
        sum += City.Distance(cities[tour[tour.Length - 1]], cities[tour[0]]); // plus powrót 

        return sum;
    }
}
