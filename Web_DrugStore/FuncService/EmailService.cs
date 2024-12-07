using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using Web_DrugStore.ViewModel;

namespace Web_DrugStore.FuncService
{
    public static class EmailService
    {
        public static void SendMail(string toEmail, string subject, string body)
        {
            var mail = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("nhokljlom99@gmail.com", "hldi aidq yqjy uusr"),
                EnableSsl = true
            };
            var message = new MailMessage
            {
                From = new MailAddress("nhokljlom99@gmail.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(toEmail));
            mail.Send(message);
        }
    }
}