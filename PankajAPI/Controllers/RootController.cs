using Microsoft.AspNetCore.Mvc;

namespace PankajAPI.Controllers
{
    [Route("/")]
    [ApiController]
    public class RootController : ControllerBase
    {
        [HttpGet(Name =nameof(GetRoot))]
        [ProducesResponseType(200)]
        [ApiVersion("1.0")]
        public IActionResult GetRoot()
        {
            var response = new
            {
                href = Url.Link(nameof(GetRoot), null),
                rooms = new
                {
                    href = Url.Link(nameof(RoomsController.GetRooms), null)
                }
            };

            return Ok(response);
        }
    }
}
