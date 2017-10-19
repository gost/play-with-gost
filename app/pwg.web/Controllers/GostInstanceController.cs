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
        public void Post([FromBody]PostObject postObject)
        {
            var g = new GostRepository();
            g.Start(postObject.Name, postObject.Tld);
        }

        [HttpDelete]
        public void Delete(string name)
        {
            var g = new GostRepository();
            g.Stop(name);
        }
    }

    public class PostObject
    {
        public string Name { get; set; }
        public string Tld { get; set; }
    }
}