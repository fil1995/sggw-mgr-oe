
using System;

struct City
{
    int id;
    double latitude;
    double longitude;
    public City(int id,double lat,double lon)
    {
        this.id = id;
        this.latitude = lat;
        this.longitude = lon;
    }
    public static double Distance(City a, City b)
    {
        //Console.WriteLine($"dist: {a} -> {b}");
        return Math.Sqrt(Math.Pow(a.latitude-b.latitude,2) + Math.Pow(a.longitude - b.longitude, 2));
    }
    public override string ToString()
    {
        return $"({id}:[{latitude},{longitude}])";
    }
}
