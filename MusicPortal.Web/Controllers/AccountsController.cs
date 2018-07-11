using Microsoft.AspNetCore.Mvc;
using MusicPortal.Auth.Helpers;
using MusicPortal.BLL.Interfaces;
using MusicPortal.ViewModels.ViewModels;
using System.Threading.Tasks;

namespace MusicPortal.Web.Controllers {
    [Route("api/accounts")]
    public class AccountsController : Controller {
        private readonly IAccountService accountService;

        public AccountsController(IAccountService accountService) {
            this.accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var result = await accountService.Create(model);

            if (!result.Succeeded) {
                return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
            }

            return new OkObjectResult("Account created!");
        }
    }
}
