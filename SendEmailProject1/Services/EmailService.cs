﻿using SendEmailProject1.Models;
using System;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit.Text;
using Microsoft.Extensions.Configuration;
using System.Net.Http;


namespace SendEmailProject1.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private HttpClient client = new HttpClient();
        public EmailService(IConfiguration config)
        {
            _config = config;
            client.BaseAddress = new Uri("https://localhost:44338/api/");
        }

        
        /// <summary>
        /// Send Email service using smtp
        /// Email credentials of sender is set up in appsettings.json
        /// </summary>
        /// <param name="request">
        /// request content:
        ///     Recipient: mandatory
        ///     Sender: optional
        ///     Body: optional
        ///     Subjcet: optional
        /// </param>

        public async Task<EmailModel> SendEmail(EmailModel request)
        {
            
            int attempt = 0;
            bool sendSuccess = false;
            var email = new MimeMessage();

            //This email object will be used to call add emaill history api
            EmailModel emailHistory = new EmailModel();
            emailHistory.Sender = _config.GetSection("EmailUsername").Value;
            emailHistory.Recipient = request.Recipient;
            emailHistory.Subject = request.Subject;
            emailHistory.Body = request.Body;
            emailHistory.Status = "not send";

            
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(request.Recipient));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value,
                _config.GetSection("EmailPassword").Value);

            //If email fails to send, it should either be retied until success or a max of 3 times whichever comes first and cen be sent in succession
            while (!sendSuccess && attempt <= 3)
            {
                attempt++;
                try
                {
                    string res = await smtp.SendAsync(email);
                    sendSuccess = true;

                    //Add to email history with date and status
                    emailHistory.Date = DateTime.Now;
                    emailHistory.Status = sendSuccess ? "success" : "failed";

                    await AddEmailHistory(emailHistory);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught: {0}",
                    ex.ToString());

                    //Add to email history with date and status
                    emailHistory.Date = DateTime.Today;
                    emailHistory.Status = sendSuccess ? "success" : "failed";

                    await AddEmailHistory(emailHistory);
                }

            }

            //disconnect from the client
            smtp.Disconnect(true);

            //and dispose of the client object
            smtp.Dispose();

            return emailHistory;
        }


        // Add email history to database
        public async Task<bool> AddEmailHistory(EmailModel request)
        {
            try
            {
                //Call post api "/EmailHistory"
                var response = client.PostAsJsonAsync("EmailHistory", request).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    return response.IsSuccessStatusCode;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    ex.ToString());
            }

            return false;
        }
    }
}
