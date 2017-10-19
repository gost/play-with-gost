using System;
using pwg.core;

namespace pwg.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var g = new GostRepository();
            Console.WriteLine("Starting GOST");
            var projectname = "haha";
            var tld = "lvh.me";
            // StartGostInstance(g, projectname, tld);
            StopGostInstance(g, projectname);
            Console.WriteLine("Stopped.. press a key");
            Console.ReadKey();

        }

        static void StopGostInstance(GostRepository repos, string ProjectName)
        {
            repos.Stop(ProjectName);
        }

        static void StartGostInstance(GostRepository repos, string ProjectName, string Tld)
        {
            var response = repos.Start(ProjectName, Tld);
            if (!String.IsNullOrEmpty(response.Error))
            {
                Console.WriteLine("Error: " + response.Error);
            }
        }
    }
}
