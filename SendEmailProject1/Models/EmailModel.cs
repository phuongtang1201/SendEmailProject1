using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SendEmailProject1.Models
{
    [BsonIgnoreExtraElements]
    public class EmailModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        public string Sender { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Recipient { get; set; }
        public string Subject { get; set; } = "Welcome!";
        public string Body { get; set; } = "This is a system message. Welcome!";
        public DateTime Date { get; set; }
        public string Status { get; set;  } 
    }
}
