using Microsoft.AspNetCore.Mvc;
using PersistentUnreal.ViewModels;

namespace PersistentUnreal.Controllers
{
    [Produces("application/json")]
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        [Route("api/v1")]
        [HttpGet]
        public IActionResult GetAccount(int accountId)
        {
            return Ok(new ApiOkResponse());
        }
    }
}
