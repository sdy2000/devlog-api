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
        [Route("register")]
        public async Task<ActionResult<IsRegisterViewModel>> PostRegisterUser(RegisterViewModel register)
        {
            IsRegisterViewModel isRegisterUser;
            isRegisterUser = await _userService.RegisterUserAsync(register);

            return Ok(isRegisterUser);
        }

        [HttpGet]
        [Route("active-account/{id}")]
        public async Task<ActionResult<ActiveAccountViewModel>> PostRegisterUser(string id)
        {
            ActiveAccountViewModel activeAccount;
            activeAccount = await _userService.ActiveAccountAsync(id);

            return Ok(activeAccount);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<UserContextViewModel>> PostUserContext(LoginViewModel login)
        {
            UserContextViewModel userContext;
            userContext = await _userService.LoginUserAsync(login);

            return Ok(userContext);
        }

        [HttpPost]
        [Route("forgot-pass")]
        public async Task<ActionResult<ForgotPasswordResponsViewModel>> PostForgotPasswod(ForgotPasswordViewModel forgot)
        {
            ForgotPasswordResponsViewModel forgotRes;
            forgotRes = await _userService.ForgotPasswordAsync(forgot);

            return Ok(forgotRes);
        }

        [HttpPost]
        [Route("retrieve-pass")]
        public async Task<ActionResult<RetierePasswordResponsViewModel>> PostretrievePasswod(RetierePasswordViewModel retieve)
        {
            RetierePasswordResponsViewModel retieveRes;
            retieveRes = await _userService.RetrievePasswordAsync(retieve);

            return Ok(retieveRes);
        }
    }
}
