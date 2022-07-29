using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Model
{
    public class MSMQModel
    {
        MessageQueue messageQueue = new MessageQueue();
        private void sendDatatoQueue(string Token)
        {
            messageQueue.Path = @".\private$\Token";
            if(!MessageQueue.Exists(messageQueue.Path))
            {
                MessageQueue.Create(messageQueue.Path);
            }
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
            messageQueue.Send(Token);
            messageQueue.BeginReceive();
            messageQueue.Close();
        }

        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = messageQueue.EndReceive(e.AsyncResult);
            string Token = msg.Body.ToString();
            string Subject = "FundooNote Reset Link";
            string Body = "Token Generated To Reset Password\n" +"Session Token : "+Token;
            var SMTP = new SmtpClient("smtp@gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("nantha.test123@gmail.com", "Nantha@123"),
                EnableSsl = true,
            };
            SMTP.Send("nantha.test123@gmail.com", "nantha.test123@gmail.com", Subject, Body);
            messageQueue.BeginReceive();
        }
    }
    
}
