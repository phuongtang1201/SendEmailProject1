using SendEmailProject1.Models;
using System.Collections.Generic;


namespace SendEmailProject1.Services
{
    public interface IDatabaseService
    {
        List<EmailModel> Get();
        EmailModel Create(EmailModel email);
        
    }
}
