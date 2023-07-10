using Core.Convertors;
using Core.DTOs;
using Core.Security;
using Core.Services.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private DevLogDbContext _context;
        public UserService(DevLogDbContext context)
        {
            _context = context;
        }


        public async Task<UserContextViewModel> LoginUser(LoginViewModel login)
        {
            UserContextViewModel user;

            // string hashPassword =  PasswordHelper.EncodePasswordMd5(login.Password);
            string email = FixedText.FixedEmail(login.Email);

            try
            {
                user = await _context.Users.
                Where(user => user.Email == email && user.Password == login.Password)
                .Select(user => new UserContextViewModel()
                {
                    user_id = user.Id,
                    user_name = user.Name,
                    email = user.Email,
                }).SingleOrDefaultAsync();
            }
            catch
            {
                user= null;
            }

            return user;
        }
    }
}
