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

        [HttpPost]
        [Route("user-info")]
        public async Task<ActionResult<UserPanelInfoViewModel>> GetUesrInfo(UserContextViewModel user)
        {
            UserPanelInfoViewModel userInfo;
            userInfo = await _userService.GetUserForUserPanelAsync(user);

            return Ok(userInfo);
        }

        [HttpPost]
        [Route("user-edit")]
        public async Task<ActionResult<IsEditUserViewModel>> GetUesrEdit([FromForm]EditUserFromUserPanelViewModel edit_user)
        {
            IsEditUserViewModel editUser;
            editUser = await _userService.EditUserFromUserPanelAsync(edit_user);

            return Ok(editUser);
        }
    }
}
