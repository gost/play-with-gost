using Ductus.FluentDocker.Commands;
using Ductus.FluentDocker.Model.Containers;
using Ductus.FluentDocker.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace pwg.core
{
    public class GostRepository
    {
        private IHostService _docker;

        public GostRepository()
        {
            var hosts = new Hosts().Discover();
            _docker = hosts.FirstOrDefault(x => x.IsNative) ?? hosts.FirstOrDefault(x => x.Name == "default");
        }

        public CommandResponse<IList<String>> Start(string ProjectName, string Tld)
        {
            Environment.SetEnvironmentVariable("VIRTUAL_HOST", ProjectName + "." + Tld);

            var file = "docker-compose.yml";
            var services = new string[1]{ "-d"};
            var ver = _docker.Host.ComposeVersion().Data[0];

            var response = _docker.Host.ComposeUp(composeFile: file, altProjectName: ProjectName,services:services);
            return response;
        }

        public List<String> GetProjects()
        {
            var runningContainers = _docker.GetRunningContainers();
            var firstcontainer = runningContainers[0];
            var configfirst = runningContainers[0].GetConfiguration();

            //runningContainers[0].Name
            return null;
        }

        public void Stop(string ProjectName)
        {
            _docker.Host.ComposeDown(altProjectName: ProjectName);
        }
    }
}
