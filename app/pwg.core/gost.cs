using Ductus.FluentDocker.Commands;
using Ductus.FluentDocker.Services;
using System;
using System.Linq;

namespace pwg.core
{
    public class Gost
    {
        private IHostService _docker;

        public Gost()
        {
            var hosts = new Hosts().Discover();
            _docker = hosts.FirstOrDefault(x => x.IsNative) ?? hosts.FirstOrDefault(x => x.Name == "default");
        }

        public void Start(string ProjectName, string ExternalUri, int Port)
        {

            Environment.SetEnvironmentVariable("PORT", Port.ToString());
            Environment.SetEnvironmentVariable("VIRTUAL_HOST", ExternalUri);

            var file = "docker-compose.yml";
            var services = new string[1]{ "-d"};
            var containers = _docker.Host.ComposeUp(composeFile: file, altProjectName: ProjectName,services:services);
        }

        public void Stop(string ProjectName)
        {
            _docker.Host.ComposeDown(altProjectName: ProjectName);
        }
    }
}
