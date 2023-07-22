using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Core.Senders
{
    public class SendEmail
    {
        public static bool Send(string To, string Subject, string Body)
        {
            try
            {
                MimeMessage mail = new MimeMessage();
                mail.From.Add(MailboxAddress.Parse("percival.smith@ethereal.email"));
                mail.To.Add(MailboxAddress.Parse(To));
                mail.Subject = Subject;
                mail.Body = new TextPart(TextFormat.Html) { Text = Body };

                using SmtpClient SmtpServer = new SmtpClient();
                SmtpServer.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
                SmtpServer.Authenticate("percival.smith@ethereal.email", "5ZfpkPUySJJgsAw7eM");
                SmtpServer.Send(mail);
                SmtpServer.Disconnect(true);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
