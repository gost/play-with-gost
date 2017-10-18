using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using pwg.core;

namespace pwg.web.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var g = new Gost();
            g.Start("gost_prod", "gost_prod.lvh.me", 8080);
            // but no containers are started, why???
            return new string[] { "value1", "value2" };
        }
    }
}
