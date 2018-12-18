using Shop.Service;
using Shop.Web.Infrastructure.Core;
using System.Web.Http;

namespace Shop.Web.Api
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiControllerBase
    {
        public HomeController(IErrorService errorService) : base(errorService)
        {
        }

        [Route("TestMethod")]
        [HttpGet]
        public string TestMethod()
        {
            return "test method";
        }
    }
}