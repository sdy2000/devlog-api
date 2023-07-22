using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class UserContextViewModel
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string email { get; set; }
        public string? user_avatar { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? phone { get; set; }
        public string? gender { get; set; }
        public string register_date { get; set; }
    }


    // Register User
    public class RegisterViewModel
    {
        public string user_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool rules { get; set; }
    }

    public class IsRegisterViewModel
    {
        public bool is_exist_user_name { get; set; }
        public bool is_exist_email { get; set; }
        public bool is_send_active_code { get; set; }
        public bool is_success { get; set; }
    }


    // Active Account
    public class ActiveAccountViewModel
    {
        public string user_name { get; set; }
        public string email { get; set; }
        public bool is_exits_user { get; set; }
        public bool is_active { get; set; }
    }


    // Login User
    public class LoginViewModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public bool remember_me { get; set; }
    }


    // Forgot Password
    public class ForgotPasswordViewModel
    {
        public string email { get; set; }
    }
    public class ForgotPasswordResponsViewModel
    {
        public bool is_exist_email { get; set; }
        public bool is_send_edit_pass { get; set; }
        public bool is_success { get; set; }
    }
}
