namespace Core.Generators
{
    public class EmailBodyGenerator
    {
        public static string SendActiveEmail(string userName, string activeCode)
        {
            string body = @"
                        <div style=""direction:rtl;padding20px"">
                            <h1>DevLog</h1>
                            <h2> Hi UserName !</h2>
                            <i>DateTime</i>
                            <p>Tanks for company in <a href=""http://localhost:3000"">DevLog</a> , It seems that you have forgotten your password. Click the link below to retrieve your password .</p>
                            <h3 style=""color:forestgreen;""><a href=""http://localhost:3000/login/retrieve-pass/ActiveCode"">retrieve your password !</a></h3>
                        </div>";

            body = body.Replace("UserName", userName);
            body = body.Replace("ActiveCode", activeCode);
            body = body.Replace("DateTime", DateTime.Now.ToString("yyyy-MM-dd/HH:mm"));

            return body;
        }
        public static string SendChengePasword(string userName, string activeCode)
        {
            string body = @"
                        <div style=""direction:rtl;padding20px"">
                            <h1>DevLog</h1>
                            <h2> Hi UserName !</h2>
                            <i>DateTime</i>
                            <p>Tanks for register in <a href=""http://localhost:3000"">DevLog</a> , You must activate your account to continue .</p>
                            <h3 style=""color:forestgreen;""><a href=""http://localhost:3000/register/ActiveCode"">Account activation !</a></h3>
                        </div>";

            body = body.Replace("UserName", userName);
            body = body.Replace("ActiveCode", activeCode);
            body = body.Replace("DateTime", DateTime.Now.ToString("yyyy-MM-dd/HH:mm"));

            return body;
        }
    }
}
