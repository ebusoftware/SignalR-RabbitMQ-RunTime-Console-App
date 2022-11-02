using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EmailSenderExample
{
    static class EmailSender
    {
        public static void Send(string to,string message)
        {

            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("ebuali038@hotmail.com", "Ebu.Ali123");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Host = "smtp-mail.outlook.com";

            NetworkCredential credential = new NetworkCredential("ebuali038@hotmail.com","sifre");
            smtp.Credentials = credential;

            MailAddress gonderen = new MailAddress("ebuali038@hotmail.com","Deneme Mesaji");
            MailAddress alici = new MailAddress(to);

            MailMessage mail = new MailMessage(gonderen,alici);
            mail.Subject = "Example";
            mail.Body = message;

            smtp.Send(mail);

        }
    }
}
