using Microsoft.AspNetCore.Http;

namespace Core.DTOs
{
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

    public class EditUserFromUserPanelViewModel
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string email { get; set; }
        public IFormFile? user_avatar { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? phone { get; set; }
        public string? gender { get; set; }
    }

    public class IsEditUserViewModel
    {
        public bool is_exist_email { get; set; }
        public bool is_send_active_code { get; set; }
        public bool is_success { get; set; }
    }
}