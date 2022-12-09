using SendEmailProject1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendEmailProject1.Services
{
    public interface IEmailService
    {
        Task<EmailModel> SendEmail(EmailModel request);
        
    }
}
