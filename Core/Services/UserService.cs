using Core.Convertors;
using Core.DTOs;
using Core.Generators;
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


        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
               await _context.Users.AddAsync(user);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsExistUserNameAsync(string userName)
        {
            return await _context.Users.AnyAsync(u => u.UserName == userName);
        }

        public async Task<bool> IsExistEmailAsync(string email)
        {
            string Email = FixedText.FixedEmail(email);
            return await _context.Users.AnyAsync(u => u.Email == Email);
        }

        public async Task<bool> SaveChangeAsync()
        {
            try
            {
               await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }




        #region ACCOUNTING

        public async Task<IsRegisterViewModel> RegisterUserAsync(RegisterViewModel register)
        {
            IsRegisterViewModel result = new IsRegisterViewModel();

            #region VALIDATION

            if (register == null)
            {
                result.is_success = false;

                return result;
            }
            if (await IsExistUserNameAsync(register.user_name))
            {
                result.is_exist_user_name = true;
                result.is_success = false;

                return result;
            }
            if (await IsExistEmailAsync(register.email))
            {
                result.is_exist_email = true;
                result.is_success = false;

                return result;

            }

            #endregion

            User user = new User()
            {
                UserName = register.user_name,
                Email = FixedText.FixedEmail(register.email),
                ActiveCode = NameGenerator.GeneratorUniqCode(),
                // After Add Email Sender Update IsActive
                IsActive = true,
                Password = PasswordHelper.EncodePasswordMd5(register.password),
                UserAvatar = "No-Photo.jpg"
                
            };

            bool addUser = await AddUserAsync(user);

            // TODO Email Activator
            #region SEND ACTIVATION EMAIL



            #endregion


            result.is_success = await SaveChangeAsync();

            return result;
        }


        //TODO: Update remmember me value from LoginUser method 
        public async Task<UserContextViewModel> LoginUserAsync(LoginViewModel login)
        {
            UserContextViewModel user;

            // string hashPassword =  PasswordHelper.EncodePasswordMd5(login.Password);
            string email = FixedText.FixedEmail(login.email);

            try
            {
                user = await _context.Users.
                Where(user => user.Email == email && user.Password == login.password)
                .Select(user => new UserContextViewModel()
                {
                    user_id = user.Id,
                    user_name = user.UserName,
                    email = user.Email,
                }).SingleOrDefaultAsync();
            }
            catch
            {
                user = null;
            }
            if (user == null)
            {
                user = new UserContextViewModel()
                {
                    user_id = 0,
                    user_name = "",
                    email = ""
                };
            }

            return user;
        }

        #endregion
    }
}
