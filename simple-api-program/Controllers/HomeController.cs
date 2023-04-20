using Microsoft.AspNetCore.Mvc;

namespace simple_api_program.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    // Don't derive from Controller, as it supports Views, while this app is for API only.
    // <https://stackoverflow.com/a/55239483/8834000>
    public class HomeController : ControllerBase
    {
        [HttpGet]
        // ActionResult is a one-for-all wrapper of mostly what an API can returns from a request
        // Common are: EmptyResult, FileResult, HttpStatusCodeResult, JsonResult
        // IActionResult compatible with IResult
        public IActionResult Hello()
        {
            return Ok();
        }
    }
}
