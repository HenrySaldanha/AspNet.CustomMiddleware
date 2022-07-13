using CustomMiddleware.Models.Request;
using CustomMiddleware.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace CustomMiddleware.Controller
{
    [Route("v{version:apiVersion}/user")]
    [ApiController]
    [ApiVersion("1")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(UserResponse),StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create(UserRequest request)
        {
            if (request.Name is null)
                return BadRequest();

            return Created(nameof(Create), request);
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserResponse),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(Guid? id)
        {
            if (id == null)
                return NotFound();

            return Ok(new UserResponse
            {
                Id = id.Value,
                Name = "Test"
            });
        }
    }
}
