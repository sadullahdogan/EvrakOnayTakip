using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace EvrakOnayUI.Helper
{
    public class Email
    {
        //public static bool SendMail(string body, List<string> to, string subject, bool isHtml = true)
        //{
        //    bool result = false;

        //    try
        //    {
        //        var message = new MailMessage();
        //        message.From = new MailAddress("sado.doan@gmail.com");

        //        to.ForEach(x =>
        //        {
        //            message.To.Add(new MailAddress(x));
        //        });

        //        message.Subject = subject;
        //        message.Body = body;
        //        message.IsBodyHtml = isHtml;

        //        using (var smtp = new SmtpClient(
        //            "smtp.gmail.com",
        //           465))
        //        {
        //            smtp.EnableSsl = true;
        //            smtp.Credentials =
        //                new NetworkCredential(
        //                    "sado.doan@gmail.com",
        //                    "sadodoan");

        //            smtp.Send(message);
        //            result = true;
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }

        //    return result;
        //}
        public static void SendEmail(string subject,string body,string to)
        {
            try
            {
                var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
                var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
                var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
                var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
                var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

                

                //client.Send(message);
                MailMessage mm = new MailMessage(to,to);//mail kime gitcekse onun mail adresi
                mm.Subject = subject;
                mm.Body = body;

                mm.IsBodyHtml = true ;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(fromEmailAddress, fromEmailPassword);//senin email şifren
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);

            }
            catch (Exception ex)
            {

            }
        }
    }
}