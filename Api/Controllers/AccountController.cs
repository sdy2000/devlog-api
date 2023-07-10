using Core.DTOs;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserContextViewModel>> PostUserContext(LoginViewModel login)
        {
            UserContextViewModel userContext;
            userContext = await _userService.LoginUserAsync(login);

            return Ok(userContext);
        }
    }
}
