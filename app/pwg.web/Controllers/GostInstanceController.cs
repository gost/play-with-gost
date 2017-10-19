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
            return null;
            // todo: get a list of gost instances
        }

        [HttpPost]
        public void Post(string Name, string Tld)
        {
            var g = new GostRepository();
            g.Start(Name, Tld);
        }


        [HttpDelete]
        public void Delete(string name)
        {
            var g = new GostRepository();
            g.Stop(name);
        }
    }
}