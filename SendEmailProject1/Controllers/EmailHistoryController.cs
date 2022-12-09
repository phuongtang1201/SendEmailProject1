using Microsoft.AspNetCore.Mvc;
using SendEmailProject1.Models;
using SendEmailProject1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SendEmailProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailHistoryController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        public EmailHistoryController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// Get all email history records every
        /// URL: "api/Email"
        /// HTTP method: GET
        /// </summary>

        // POST api/<ValuesController>
        [HttpGet]
        public ActionResult <List<EmailModel>> Get()
        {
            return _databaseService.Get();
        }

        /// <summary>
        /// Create a new email history record every time sending email
        /// URL: "api/Email"
        /// HTTP method: POST
        /// </summary>
        /// <param name="email">
        /// email content:
        ///     Recipient: mandatory
        ///     Sender: optional
        ///     Body: optional
        ///     Subjcet: optional
        /// </param>

        [HttpPost]
        public IActionResult Post([FromBody] EmailModel email)
        {
            _databaseService.Create(email);
            return CreatedAtAction(nameof(Get), new { id = email.Id }, email);
        }


    }
}
