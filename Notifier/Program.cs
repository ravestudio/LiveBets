using System;

namespace Notifier
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Notifier notifier = new Notifier(new CommonLib.WebApiClient());
            notifier.Start();
        }
    }
}
