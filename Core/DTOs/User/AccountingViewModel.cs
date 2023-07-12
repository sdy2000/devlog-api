using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class UserContextViewModel
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string email { get; set; }
    }

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

    public class LoginViewModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public bool remember_me { get; set; }
    }
}
