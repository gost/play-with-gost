using Microsoft.AspNetCore.Mvc;
using pwg.core;

namespace pwg.web.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpPost]
        public bool Get(string ProjectName)
        {
            var g = new Gost();
            g.Start(ProjectName, "gost_prod.lvh.me", 8080);
            return true;
        }
    }
}
