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
            var result = new List<String>();

            foreach(var cnt in runningContainers)
            {
                if (cnt.Name.Contains("_dashboard_")){
                    int start = 1;
                    int end = cnt.Name.IndexOf("_dashboard_");
                    var project = cnt.Name.Substring(start, end - start);
                    result.Add(project);
                }
            }
            return result;
        }

        public void Stop(string ProjectName)
        {
            _docker.Host.ComposeDown(altProjectName: ProjectName);
        }
    }
}
