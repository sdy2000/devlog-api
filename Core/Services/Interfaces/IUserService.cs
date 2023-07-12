using Core.DTOs;
using Data.Models;
using System.Threading.Tasks;
//using Data.Models;

namespace Core.Services.Interfaces
{
    public interface IUserService
    {

        Task<bool> AddUserAsync(User user);
        Task<bool> IsExistUserNameAsync(string userName);
        Task<bool> IsExistEmailAsync(string email);
        Task<bool> SaveChangeAsync();


        #region ACCOUNT

        Task<IsRegisterViewModel> RegisterUserAsync(RegisterViewModel register);
        Task<UserContextViewModel> LoginUserAsync(LoginViewModel login);

        #endregion

        #region USER PANEL

        Task<UserPanelInfoViewModel> GetUserForUserPanelAsync(UserContextViewModel user);

        #endregion
    }
}
