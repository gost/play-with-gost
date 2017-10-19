using Microsoft.AspNetCore.Mvc;
using pwg.core;

namespace pwg.web.Controllers
{
    [Produces("application/json")]
    [Route("api/Test")]
    public class TestController : Controller
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            var g = new Gost();
            g.Stop("gost_prod");

            return "hallo";

        }
    }
}