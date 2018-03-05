using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ERM.Utilities
{
    public class ReUsable
    {
        public static Task<int> SendEmail(String Subject, string ToEMail, String HtmlBodyWithSignature)
        {
            var task = new Task<int>(() => 0);
            try
            {
                MailMessage mm = new MailMessage();
                mm.To.Add(new MailAddress(ToEMail));
                mm.Subject = Subject;
                mm.Body = HtmlBodyWithSignature;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;
                smtp.Send(mm);
                mm.Dispose();
                return task = new Task<int>(() => 1); //Mail Sent
            }
            catch (System.Net.Mail.SmtpException)
            {
                return task;
            }

        }
        public static string WelcomeMail(string EmailPath, string UserName, string EmailId, string PassWord)
        {
            using (StreamReader reader = new StreamReader(EmailPath))
            {
                string readFile = reader.ReadToEnd();
                string myString = "";
                myString = readFile;
                myString = myString.Replace("$$UserName$$", EmailId);
                myString = myString.Replace("$$PassWord$$", PassWord);
                string bodyWithSign = myString.ToString();
                return bodyWithSign;
            }
        }
    }
}
