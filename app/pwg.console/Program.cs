using Ductus.FluentDocker.Services;
using System;
using System.Linq;
using Ductus.FluentDocker.Commands;

namespace pwg.console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Environment.SetEnvironmentVariable("PORT", "8080");
            Environment.SetEnvironmentVariable("VIRTUAL_HOST", "gost_prod.lvh.me");

            var file = "docker-compose.yml";
            var hosts = new Hosts().Discover();
            var _docker = hosts.FirstOrDefault(x => x.IsNative) ?? hosts.FirstOrDefault(x => x.Name == "default");
            var containers = _docker.Host.ComposeUp(composeFile: file, altProjectName: "gost_prod");

            Console.WriteLine("Docker-compose fired: " + containers.Success);
            Console.ReadKey();
        }
    }
}
