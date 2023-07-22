using Core.DTOs;
using Data.Models;
using Microsoft.AspNetCore.Http;

namespace Core.Services.Interfaces
{
    public interface IUserService
    {

        Task<bool> AddUserAsync(User user);
        bool UpdateUser(User user);
        Task<User> GetUserByActiveCodeAsync(string activeCode);
        Task<bool> IsExistUserNameAsync(string userName);
        Task<bool> IsExistEmailAsync(string email);
        Task<bool> SaveChangeAsync();
        string UserImagePath(string folderName, string imgName);
        Task<string> SaveOrUpDateImg(IFormFile img, string imgName = "No-Photo.jpg");


        #region ACCOUNT

        Task<IsRegisterViewModel> RegisterUserAsync(RegisterViewModel register);
        Task<ActiveAccountViewModel> ActiveAccountAsync(string activeCode);
        Task<UserContextViewModel> LoginUserAsync(LoginViewModel login);

        #endregion

        #region USER PANEL

        Task<UserPanelInfoViewModel> GetUserForUserPanelAsync(UserContextViewModel user);
        Task<EditedUserViewModel> EditUserFromUserPanelAsync(EditUserFromUserPanelViewModel edit_user);
        Task<UserPassResponsViewModel> ChengPasswordAsync(EditUserPassViewModel user_pass);

        #endregion
    }
}
