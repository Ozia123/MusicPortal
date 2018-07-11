using Microsoft.AspNetCore.Mvc;
using MusicPortal.Auth.Helpers;
using MusicPortal.Auth.Interfaces;
using MusicPortal.ViewModels.ViewModels;
using System.Threading.Tasks;

namespace MusicPortal.Web.Controllers {
    [Route("api/auth")]
    public class AuthController : Controller {
        private readonly IJwtService jwtService;

        public AuthController(IJwtService jwtService) {
            this.jwtService = jwtService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CredentialsViewModel credentials) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var jwt = await jwtService.GetJwtForCredentials(credentials);

            if (string.IsNullOrEmpty(jwt)) {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
            }

            return new OkObjectResult(jwt);
        }
    }
}
