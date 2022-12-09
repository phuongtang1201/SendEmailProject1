using SendEmailProject1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace SendEmailProject1.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IMongoCollection<EmailModel> _emails;
        public DatabaseService(IEmailStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _emails = database.GetCollection<EmailModel>(settings.EmailCollectionName);
        }

        //Create a new email history
        public EmailModel Create(EmailModel email)
        {
            _emails.InsertOne(email);
            return email;

        }

        //Get the list of all emails
        public List<EmailModel> Get()

        { 
            var temp = _emails.Find(email => true);
            List<EmailModel> list = temp.ToList();
            return list;
        }

    }
}
