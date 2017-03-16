using System.Web.Http;
using System.Threading.Tasks;
using Messaging.Models.Interfaces;

namespace Consumer.Controllers
{
    [RoutePrefix("consumer")] // This route is not being set as expected, app is is the prefix to Hello
    public class HelloController : ApiController
    {
        public HelloController()
        {
        }

        [Route("Hello")] // route is app/Hello, 
        [HttpPost]
        public async Task<IHttpActionResult> GetUserInfo([FromBody] IHelloWorld MessageLog)
        {
            var haveALook = MessageLog;
            return Ok();
        }

    }
}
