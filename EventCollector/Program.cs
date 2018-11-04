using System;

namespace EventCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Live Bets");

            Collector collector = new Collector(new CommonLib.WebApiClient());
            collector.Start();

            Console.ReadKey();
        }
    }
}
