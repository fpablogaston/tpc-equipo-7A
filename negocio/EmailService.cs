using System;
using System.Net.Mail;
using System.Web.Configuration;

public class EmailService
{
    public void EnviarMail(string destinatario, string asunto, string cuerpo)
    {
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(WebConfigurationManager.AppSettings["smtpUser"]);
        mail.To.Add(destinatario);
        mail.Subject = asunto;
        mail.Body = cuerpo;
        mail.IsBodyHtml = true;

        SmtpClient smtp = new SmtpClient();
        smtp.Send(mail);
    }
}

