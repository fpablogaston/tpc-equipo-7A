using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

public class EmailService
{
    public void EnviarMail(string destinatario, string asunto, string cuerpo)
    {
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("tienda7equipo7@gmail.com");
        mail.To.Add(destinatario);
        mail.Subject = asunto;
        mail.Body = cuerpo;
        mail.IsBodyHtml = true;

        SmtpClient smtp = new SmtpClient();
        smtp.Send(mail);
    }
}
