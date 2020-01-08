using System;
using System.Net;
using System.Net.Mail;

namespace MailCore.Helpers
{
    public class MailConfig
    {
        public string Host { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public bool SSL { get; set; }
        public int Port { get; set; }
    }
    public class MailHelper
    {
        public static void SendMail(MailConfig config, string toList, string from, string fromName, string subject, string body, bool isBodyHtml = false)
        {
            try
            {
                using (MailMessage message = new MailMessage())
                {
                    message.Body = body;
                    message.Subject = subject;
                    message.IsBodyHtml = isBodyHtml;
                    message.From = new MailAddress(from, fromName);
                    var tos = toList.Split(";");
                    foreach (var to in tos)
                    {
                        isMailCorrect(to);
                        message.To.Add(to);
                    }
                    Send(message, config);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Mail gönderim işlemi sırasında bir hata meydana geldi.\n" + ex.Message);
            }
        }
        static void Send(MailMessage message, MailConfig config)
        {
            try
            {
                using (SmtpClient client = new SmtpClient())
                {
                    client.Host = config.Host;
                    client.EnableSsl = config.SSL;
                    client.Credentials = new NetworkCredential(config.Mail, config.Password);
                    client.UseDefaultCredentials = false;
                    client.Port = config.Port;
                    client.Timeout = 60;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Mail gönderim işlemi sırasında bir hata meydana geldi.SMTP Bilgileri hatalı olabilir...\n" + ex.Message);
            }
        }
        static void isMailCorrect(string mail)
        {
            if (!mail.Contains("@") || !mail.Contains(".com"))
                throw new Exception(mail + "\nGeçersiz mail !");
        }
    }
}
