using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendEmailProject1.Models
{
    //read and store database settings that defines in appsettings.json
    public class EmailStoreDatabaseSettings : IEmailStoreDatabaseSettings
    {
        public string EmailCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
