using System;
using System.Linq;


namespace WeatherLab
{
    class Program
    {
        static string dbfile = @".\data\climate.db";

        static void Main(string[] args)
        {
            var measurements = new WeatherSqliteContext(dbfile).Weather;

            var total_2020_precipitation = measurements.Where(w => w.year == 2020).Sum(w => w.precipitation);
            Console.WriteLine($"Total precipitation in 2020: {total_2020_precipitation} mm\n");

            //
            // Heating Degree days have a mean temp of < 18C
            //   see: https://en.wikipedia.org/wiki/Heating_degree_day
            //

            // ?? TODO ??
            var HeatDegreeDays = measurements.Where(w => w.meantemp < 18);

            //
            // Cooling degree days have a mean temp of >=18C
            //

            // ?? TODO ??
            var CoolDegreeDays = measurements.Where(w => w.meantemp >= 18);
            //
            // Most Variable days are the days with the biggest temperature
            // range. That is, the largest difference between the maximum and
            // minimum temperature
            //
            // Oh: and number formatting to zero pad.
            // 
            // For example, if you want:
            //      var x = 2;
            // To display as "0002" then:
            //      $"{x:d4}"
            //
            Console.WriteLine("Year\tHDD\tCDD");

            // ?? TODO ??
            for (var year = 2016; year <= 2020; year++)
            {
                var hdd = HeatDegreeDays.Where(n => n.year == year);
                var cdd = CoolDegreeDays.Where(n => n.year == year);
                Console.WriteLine($"{year}\t{hdd.Count():d3}\t{cdd.Count():d3}");
            }

            Console.WriteLine("\nTop 5 Most Variable Days");
            Console.WriteLine("YYYY-MM-DD\tDelta");

            // ?? TODO ??
         
            var varDay = measurements.OrderByDescending(w => (w.maxtemp - w.mintemp));
            var only = 5;
            var Delta = varDay.Take(only).Select(w => (w.maxtemp - w.mintemp)).ToArray(); ;
            var yr = varDay.Take(only).Select(w => w.year).ToArray();
            var mo = varDay.Take(only).Select(w => w.month).ToArray(); ;
            var dy = varDay.Take(only).Select(w => w.day).ToArray(); ;
            for (var i = 0; i < 5; i++)
            {
                Console.WriteLine($"{yr[i]:d4}-{mo[i]:d2}-{dy[i]:d2}\t {Delta[i]:f5} ");
            }
        }
    }
}
