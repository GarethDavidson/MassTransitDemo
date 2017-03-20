using System.Web.Http;
using Messaging.Models;
using System.Threading.Tasks;

namespace Consumer.Controllers
{
    [RoutePrefix("consumer")] // This route is not being set as expected, app is is the prefix to Hello
    public class HelloController : ApiController
    {
        public HelloController()
        {
        }

        [Route("Hello")]
        [HttpPost]
        public async Task<IHttpActionResult> GetUserInfo([FromBody] HelloWorld MessageLog)
        {
            var haveALook = MessageLog;
            return Ok();
        }

    }
}
