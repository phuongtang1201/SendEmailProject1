using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendEmail.Core
{
    public class Email
    {
       
        public string Sender { get; set; }
        public string Recipent { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Date { get; set; }
    }
}
