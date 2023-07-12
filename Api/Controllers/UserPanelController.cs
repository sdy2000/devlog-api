using Core.DTOs;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPanelController : ControllerBase
    {
        private IUserService _userService;

        public UserPanelController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("user-info")]
        public async Task<ActionResult<UserPanelInfoViewModel>> GetUesrInfo(UserContextViewModel user)
        {
            UserPanelInfoViewModel userInfo;
            userInfo = await _userService.GetUserForUserPanelAsync(user);

            return Ok(userInfo);
        }
    }
}
