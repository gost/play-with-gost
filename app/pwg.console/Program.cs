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

            var tld = "lvh.me";
            var projectname = "bert12";
            StartGostInstance(g, projectname, tld);
            // StopGostInstance(g, projectname, tld);
            var projects = g.GetProjects();
            Console.WriteLine("Projects: " + string.Join(",", projects));
            Console.WriteLine("Stopped.. press a key");
            Console.ReadKey();

        }

        static void StopGostInstance(GostRepository repos, string ProjectName, string Tld)
        {
            repos.Stop(ProjectName, Tld);
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
