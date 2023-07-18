namespace Core.Convertors
{
    public class FixedText
    {
        public static string FixedEmail(string email)
        {
            return email==null?null: email.Trim().ToLower();
        }
    }
}
