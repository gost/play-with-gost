using System;
using pwg.core;

namespace pwg.console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting GOST");
            var g = new Gost();
            g.Start("gost_prod", "gost_prod.lvh.me", 8080);
            // Console.WriteLine(res);
            // to stop do the following:
            // g.Stop("gost_prod");
            Console.WriteLine("Stopped.. press a key");
            Console.ReadKey();

        }
    }
}
