using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendEmailProject1.Models
{
    public interface IEmailStoreDatabaseSettings
    {
        string EmailCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

    }
}
