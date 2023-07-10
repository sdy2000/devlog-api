using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class UserContextViewModel
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string email { get; set; }
    }
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
