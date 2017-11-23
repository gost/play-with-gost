using Ductus.FluentDocker.Commands;
using Ductus.FluentDocker.Model.Containers;
using Ductus.FluentDocker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pwg.core
{
    public class GostRepository
    {
        private IHostService _docker;
        private string nginxproxyid;

        public GostRepository()
        {
            var hosts = new Hosts().Discover();
            _docker = hosts.FirstOrDefault(x => x.IsNative) ?? hosts.FirstOrDefault(x => x.Name == "default");
        }

        public async Task StartAsync(string ProjectName, string Tld)
        {
            await Task.Run(() =>
            {
                return Start(ProjectName, Tld);
            }
            );
        }

        public CommandResponse<IList<String>> CreateNetwork(string Name)
        {
            var n = new NetworkCreateParams();
            var response = _docker.Host.NetworkCreate(Name, null);
            return response;
        }

        public CommandResponse<IList<String>> DeleteNetwork(string Id)
        {
            var networks = new string[] { Id };
            var response = _docker.Host.NetworkRm(network: networks);
            return response;
        }

        public string GetNetworkIdByName(string Name)
        {
            var networks = _docker.Host.NetworkLs();
            foreach (var n in networks.Data)
            {
                var sp = n.Split(new string[] { "   " }, StringSplitOptions.None);
                if (sp[1] == Name)
                {
                    return sp[0];
                }
            }

            return null;
        }


        public string GetContainerByName(string Name)
        {
            var containers = _docker.GetContainers(filter: $" -f name={Name}");
            if (containers.Count == 1)
            {
                return containers.FirstOrDefault().Id;
            }
            return null;
        }

        public CommandResponse<IList<String>> DisconnectNetwork(string ContainerId, string NetWorkId)
        {
            var response = _docker.Host.NetworkDisconnect(ContainerId, NetWorkId);
            return response;
        }

        public CommandResponse<IList<String>> ConnectNetwork(string ContainerId, string NetWorkId)
        {
            var response = _docker.Host.NetworkConnect(ContainerId, NetWorkId);
            return response;
        }

        public string GetVersion()
        {
            return _docker.Host.ComposeVersion().Data[0];
        }

        public CommandResponse<IList<String>> Start(string ProjectName, string Tld)
        {
            // get the nginx proxy
            nginxproxyid = GetContainerByName("nginx-proxy");

            if (nginxproxyid != null)
            {
                // create network for project an connect to nginx proxy
                var projectid = ProjectName + "." + Tld;
                var resp = CreateNetwork(projectid);
                var network_id = resp.Data[0];
                ConnectNetwork(nginxproxyid, network_id);

                // Start new gost instance in the new network
                Environment.SetEnvironmentVariable("VIRTUAL_HOST", projectid);

                var file = "docker-compose.yml";
                var services = new string[1] { "-d" };
                var response = _docker.Host.ComposeUp(composeFile: file, altProjectName: ProjectName, services: services);
                return response;
            }

            return null;
        }

        public List<String> GetProjects()
        {
            var runningContainers = _docker.GetRunningContainers();
            var result = new List<String>();

            foreach (var cnt in runningContainers)
            {
                if (cnt.Name.Contains("_dashboard_"))
                {
                    int start = 1;
                    int end = cnt.Name.IndexOf("_dashboard_");
                    var project = cnt.Name.Substring(start, end - start);
                    result.Add(project);
                }
            }
            return result;
        }

        public void Stop(string ProjectName, string Tld)
        {
            var projectid = ProjectName + "." + Tld;

            var containerid = GetContainerByName(ProjectName + "_dashboard_1");
            DisconnectNetwork(containerid, nginxproxyid);
            _docker.Host.ComposeDown(altProjectName: ProjectName, removeVolumes: true, removeOrphanContainers: true);
            var network = GetNetworkIdByName(projectid);
            DeleteNetwork(network);
        }
    }
}
