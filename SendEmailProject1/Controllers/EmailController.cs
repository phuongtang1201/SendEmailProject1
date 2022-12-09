using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using SendEmailProject1.Models;
using SendEmailProject1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendEmailProject1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailServie)
        {
            _emailService = emailServie;
        }


        /// <summary>
        /// This api to send email to recipient's email with customized body and subject.
        /// URL: "/Email"
        /// HTTP method: GET
        /// </summary>
        /// <param name="request">
        /// request content:
        ///     Recipient: mandatory
        ///     Sender: optional
        ///     Body: optional
        ///     Subjcet: optional
        /// </param>

        [HttpPost]
        public IActionResult SendEmail(EmailModel request)
        {
           //calling send email service
           var response =  _emailService.SendEmail(request);
            
            if (response == null || !response.Result.Status.Equals("success"))
                return StatusCode(500);

            return Ok(response.Result);
        }
    }
}
