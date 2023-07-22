using Core.Convertors;
using Core.DTOs;
using Core.Generators;
using Core.Security;
using Core.Senders;
using Core.Services.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Http;
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

        public bool UpdateUser(User user)
        {
            try
            {
                _context.Users.Update(user);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<User> GetUserByActiveCodeAsync(string activeCode)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.ActiveCode == activeCode);
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

        public string UserImagePath(string folderName, string imgName)
        {
            string imagePath = Path.Combine(
                 Directory.GetCurrentDirectory(),
                 "wwwroot", "UserAvatar", folderName,
                 imgName);

            string path = Path.Combine(
                 Directory.GetCurrentDirectory(),
                 "wwwroot", "UserAvatar", folderName);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return imagePath;
        }

        public async Task<string> SaveOrUpDateImg(IFormFile img, string imgName = "No-Photo.jpg")
        {


            if (img != null && img.IsImage() && !FileValidator.CheckIfExiclFile(img))
            {

                string normalPath = UserImagePath("NormalSize", imgName);
                string thumbPath = UserImagePath("ThumbSize", imgName);
                string iconPath = UserImagePath("IconSize", imgName);

                if (imgName != "No-Photo.jpg")
                {
                    if (File.Exists(normalPath))
                        File.Delete(normalPath);

                    if (File.Exists(thumbPath))
                        File.Delete(thumbPath);

                    if (File.Exists(iconPath))
                        File.Delete(iconPath);
                }

                imgName = new string
                (Path.GetFileNameWithoutExtension(img.FileName).Take(10).ToArray()).Replace(' ', '-') + "-" +
                NameGenerator.GeneratorUniqCode() + "-" +
                DateTime.Now.ToString("yyyyMMddHH") + Path.GetExtension(img.FileName);

                normalPath = UserImagePath("NormalSize", imgName);
                thumbPath = UserImagePath("ThumbSize", imgName);
                iconPath = UserImagePath("IconSize", imgName);

                using (var stream = new FileStream(normalPath, FileMode.Create))
                {
                    await img.CopyToAsync(stream);
                }


                #region RESIZE IMAGE TO THUMB

                ImageConvertor imgResizeThumb = new ImageConvertor();

                imgResizeThumb.Image_resize(normalPath, thumbPath, 184);

                #endregion

                #region RESIZE IMAGE TO ICON

                ImageConvertor imgResize = new ImageConvertor();

                imgResize.Image_resize(normalPath, iconPath, 64);

                #endregion


                return imgName;
            }
            else if (imgName != "No-Photo.jpg")
            {
                return imgName;
            }
            else
            {
                return "No-Photo.jpg";
            }
        }





        // TODO : Update remmember me value from LoginUser method 
        #region ACCOUN

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
                IsActive = false,
                Password = PasswordHelper.EncodePasswordMd5(register.password),
                UserAvatar = "No-Photo.jpg"

            };

            bool addUser = await AddUserAsync(user);

            #region SEND ACTIVATION EMAIL

            if (addUser)
            {
                try
                {
                    string body = EmailBodyGenerator.SendActiveEmail(user.UserName, user.ActiveCode);
                    bool isSendEmail = SendEmail.Send(user.Email, "Activation", body);

                    result.is_send_active_code = true;
                }
                catch
                {
                    result.is_send_active_code = false;
                    result.is_success = false;

                    return result;
                }
            }

            #endregion


            result.is_success = await SaveChangeAsync();

            return result;
        }

        public async Task<ActiveAccountViewModel> ActiveAccountAsync(string activeCode)
        {
            ActiveAccountViewModel result = new ActiveAccountViewModel();

            User user = await GetUserByActiveCodeAsync(activeCode);

            if (user == null || user.IsActive)
            {
                result.is_exits_user = false;

                return result;
            }

            user.IsActive = true;
            user.ActiveCode = NameGenerator.GeneratorUniqCode();

            UpdateUser(user);
            bool isUpdate = await SaveChangeAsync();

            result.user_name = user.UserName;
            result.email = user.Email;
            result.is_exits_user = true;
            result.is_active = true;

            return result;
        }

        //TODO: Update remmember me value from LoginUser method 
        public async Task<UserContextViewModel> LoginUserAsync(LoginViewModel login)
        {
            UserContextViewModel user;

            string hashPassword = PasswordHelper.EncodePasswordMd5(login.password);
            string email = FixedText.FixedEmail(login.email);

            try
            {
                user = await _context.Users.
                Where(user => user.Email == email && user.Password == hashPassword)
                .Select(user => new UserContextViewModel()
                {
                    user_id = user.Id,
                    user_name = user.UserName,
                    email = user.Email,
                    first_name = user.FirstName,
                    last_name = user.LastName,
                    user_avatar = user.UserAvatar,
                    phone = user.Phone,
                    gender = user.Gender,
                    register_date = user.RegisterDate.ToString("yyyy-MM-dd")

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


        #region USER PANEL

        public async Task<UserPanelInfoViewModel> GetUserForUserPanelAsync(UserContextViewModel user)
        {
            UserPanelInfoViewModel userInfo;


            string email = FixedText.FixedEmail(user.email);

            userInfo = await _context.Users.
                Where(u => u.UserName == user.user_name && u.Email == email && u.Id == user.user_id && u.IsActive)
               .Select(u => new UserPanelInfoViewModel()
               {
                   email = u.Email,
                   user_name = u.UserName,
                   first_name = u.FirstName,
                   last_name = u.LastName,
                   user_avatar = u.UserAvatar,
                   phone = u.Phone,
                   gender = u.Gender,
                   register_date = u.RegisterDate.ToString("yyyy-MM-dd")
               }).SingleAsync();

            return userInfo;
        }

        public async Task<EditedUserViewModel> EditUserFromUserPanelAsync(EditUserFromUserPanelViewModel edit_user)
        {
            EditedUserViewModel result = new EditedUserViewModel();

            User user = await _context.Users.
                SingleOrDefaultAsync(user => user.UserName == edit_user.user_name && user.Id == int.Parse(edit_user.user_id));
            string email = FixedText.FixedEmail(edit_user?.email);

            #region VALIDATION

            if (edit_user == null)
            {
                result.is_success = false;

                return result;
            }
            if (user.Email != email && email != null)
            {
                if (await IsExistEmailAsync(email))
                {
                    result.is_exist_email = true;
                    result.is_success = false;

                    return result;
                }
            }

            #endregion

            user.FirstName = edit_user.first_name;
            user.LastName = edit_user.last_name;
            user.Phone = edit_user.phone;
            user.Gender = edit_user.gender;
            user.UserAvatar = await SaveOrUpDateImg(edit_user.user_avatar, user.UserAvatar);


            #region SEND ACTIVATION EMAIL

            if (email != null)
            {
                try
                {
                    string body = EmailBodyGenerator.SendActiveEmail(user.UserName, user.ActiveCode);
                    bool isSendEmail = SendEmail.Send(user.Email, "Activation New Email", body);

                    user.IsActive = false;
                    result.is_send_active_code = true;
                }
                catch
                {
                    result.is_send_active_code = false;
                    result.is_success = false;

                    return result;
                }
            }

            #endregion


            UpdateUser(user);
            result.user = new UserContextViewModel()
            {
                user_id = user.Id,
                user_name = user.UserName,
                email = user.Email,
                first_name = user.FirstName,
                last_name = user.LastName,
                user_avatar = user.UserAvatar,
                phone = user.Phone,
                gender = user.Gender,
                register_date = user.RegisterDate.ToString("yyyy-MM-dd")
            };
            result.is_success = await SaveChangeAsync();

            return result;
        }

        public async Task<UserPassResponsViewModel> ChengPasswordAsync(EditUserPassViewModel user_pass)
        {
            UserPassResponsViewModel result = new UserPassResponsViewModel();

            string oldPass = PasswordHelper.EncodePasswordMd5(user_pass.old_password);
            string newPass = PasswordHelper.EncodePasswordMd5(user_pass.new_password);

            User user = await _context.Users
                .SingleOrDefaultAsync(user => user.UserName == user_pass.user_name && user.Id == user_pass.user_id);

            #region VALIDATION

            if (user == null)
            {
                result.is_success = false;

                return result;
            }

            if (user.Password != oldPass)
            {
                result.is_old_pass_true = false;
                result.is_success = false;

                return result;
            }
            result.is_old_pass_true = true;

            #endregion

            user.Password = newPass;

            UpdateUser(user);
            result.is_success = await SaveChangeAsync();

            return result;
        }

        #endregion
    }
}
