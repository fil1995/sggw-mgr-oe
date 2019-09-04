using System;
using System.Globalization;
using System.IO;


class Cities
{
    bool useDistanceCache = true;
    City[] cities;
    public int Length => cities.Length;
    public string name;

    double?[,] distanceCache;

    public Cities(string filename)
    {
        name = filename;
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
                if (!startReadCoords && line.StartsWith("NODE_COORD"))
                {
                    startReadCoords = true;
                    Console.WriteLine($"Start reading tsp file... {citiesToLoad} cities");
                    continue;
                }

                // teraz czytam miasta
                if (startReadCoords && citiesToLoad > 0)
                {
                    string[] data = line.Split(' ');
                    try
                    {
                        cities[i] = new City(int.Parse(data[0], CultureInfo.InvariantCulture),
                        double.Parse(data[1], CultureInfo.InvariantCulture),
                        double.Parse(data[2], CultureInfo.InvariantCulture));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"error: pozostało {citiesToLoad}   Błąd:" + line);
                    }
                    citiesToLoad--;
                    i++;
                }

            }
            reader.Close();
        }
        if (Length > 1000) { useDistanceCache = false; Console.WriteLine("Disable distance cache - too many cities."); }
        if (useDistanceCache) distanceCache = new double?[Length, Length];
    }

    // tu jako parametr dostaje fenotyp (licze od 0)
    public double Distance(uint[] tour)
    {
        double sum = 0.0;
        for (int i = 0; i < tour.Length - 1; i++)
        {
            sum += Distance(tour[i], tour[i + 1]);
            //sum += City.Distance(cities[tour[i]], cities[tour[i + 1]]);
            //Console.WriteLine($"i={i} od {tour[i]}  do {tour[i+1]}  dist: {cities[tour[i]]} -> {cities[tour[i + 1]]}");
        }
        //sum += City.Distance(cities[tour[tour.Length - 1]], cities[tour[0]]); // plus powrót 
        sum += Distance(tour[tour.Length - 1], tour[0]); // plus powrót 

        return sum;
    }
    // odległość po ID, z cache
    double Distance(uint a, uint b)
    {
        if(!useDistanceCache) return City.Distance(cities[a], cities[b]);

        // tu jeszcze skrócenie o połowe...  w najlepszym przypadku
        if (a > b)
        {
            uint tmp = a;
            a = b;
            b = tmp;
        }

        if (!distanceCache[a,b].HasValue)
        {
            distanceCache[a, b] =  City.Distance(cities[a], cities[b]);
        }

        return distanceCache[a, b].Value;
    }
    struct City
    {
        int id;
        double latitude;
        double longitude;
        public City(int id, double lat, double lon)
        {
            this.id = id;
            this.latitude = lat;
            this.longitude = lon;
        }
        public static double Distance(City a, City b)
        {
            //Console.WriteLine($"dist: {a} -> {b}");
            return Math.Sqrt(Math.Pow(a.latitude - b.latitude, 2) + Math.Pow(a.longitude - b.longitude, 2));
        }
        public override string ToString()
        {
            return $"({id}:[{latitude},{longitude}])";
        }
    }

}
