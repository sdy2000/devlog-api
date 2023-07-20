using Data.Models;
using Microsoft.AspNetCore.Http;

namespace Core.DTOs
{
    // User Info View Model
    public class UserPanelInfoViewModel
    {
        public string user_name { get; set; }
        public string email { get; set; }
        public string? user_avatar { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? phone { get; set; }
        public string? gender { get; set; }
        public string register_date { get; set; }
    }


    // Edit User View Model 
    public class EditUserFromUserPanelViewModel
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string? email { get; set; }
        public IFormFile? user_avatar { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? phone { get; set; }
        public string? gender { get; set; }
    }

    public class EditedUserViewModel
    {
        public UserContextViewModel user { get; set; }
        public bool is_exist_email { get; set; }
        public bool is_send_active_code { get; set; }
        public bool is_success { get; set; }
    }


    // Edit User View Model 
    public class EditUserPassViewModel
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string old_password { get; set; }
        public string new_password { get; set; }
        public string re_new_password { get; set; }
    }

    public class UserPassResponsViewModel
    {
        public bool is_old_pass { get; set; }
        public bool is_success { get; set; }
    }


}