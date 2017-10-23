using Microsoft.AspNetCore.Mvc;
using pwg.core;
using System.Collections.Generic;

namespace pwg.web.Controllers
{
    [Produces("application/json")]
    [Route("api/gost_instance")]
    public class GostInstanceController : Controller
    {
        [HttpGet]
        public List<string> Get()
        {
            var g = new GostRepository();
            return g.GetProjects();
        }

        [HttpPost]
        public async void Post([FromBody]PostObject postObject)
        {
            var g = new GostRepository();
            await g.StartAsync(postObject.Name, postObject.Tld);
        }

        [HttpDelete]
        public void Delete(string name, string Tld)
        {
            var g = new GostRepository();
            g.Stop(name, Tld);
        }
    }

    public class PostObject
    {
        public string Name { get; set; }
        public string Tld { get; set; }
    }
}