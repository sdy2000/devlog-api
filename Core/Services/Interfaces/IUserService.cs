using Core.DTOs;
using Data.Models;

namespace Core.Services.Interfaces
{
    public interface IUserService
    {
        #region ACCOUNT

        Task<UserContextViewModel> LoginUser(LoginViewModel login);

        #endregion
    }
}
