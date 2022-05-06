using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderExample
{
    public static class EmailSender
    {
        public static void Send(string to, string message) 
        {
            SmtpClient smtpClient = new();
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;

            NetworkCredential credentinal = new("you need to enter your email", "you need to enter your email password");
            smtpClient.Credentials = credentinal;

            MailAddress sender = new("you need to enter your email", "ExampleRabbitMQ");
            MailAddress receiver = new(to);

            MailMessage mail = new(sender, receiver);
            mail.Subject = "Example";
            mail.Body = message;
            smtpClient.Send(mail);


        }
    }
}
