﻿using Core.DTOs;
using Data.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
//using Data.Models;

namespace Core.Services.Interfaces
{
    public interface IUserService
    {

        Task<bool> AddUserAsync(User user);
        bool UpdateUser(User user);
        Task<bool> IsExistUserNameAsync(string userName);
        Task<bool> IsExistEmailAsync(string email);
        Task<bool> SaveChangeAsync();
        string UserImagePath(string folderName, string imgName);
        Task<string> SaveOrUpDateImg(IFormFile img, string imgName = "No-Photo.jpg");


        #region ACCOUNT

        Task<IsRegisterViewModel> RegisterUserAsync(RegisterViewModel register);
        Task<UserContextViewModel> LoginUserAsync(LoginViewModel login);

        #endregion

        #region USER PANEL

        Task<UserPanelInfoViewModel> GetUserForUserPanelAsync(UserContextViewModel user);
        Task<EditedUserViewModel> EditUserFromUserPanelAsync(EditUserFromUserPanelViewModel edit_user);

        #endregion
    }
}
