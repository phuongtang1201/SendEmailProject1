
Web API giving send email service.
- Send an email to the recipient with the option of an automated or customized message.
- To send email, call the get api "/Email"
- If email fails to send it should either be retried until success or a max of 3 times whichever comes first, and can be sent in succession.
- All history details are stored via a database that is hosted on MongoDB cloud.
- Can see all history by call the api "/api/Email" with Get method



*To run web application
- Using Visual Studio
- Clone the project: https://github.com/phuongtang1201/SendEmailProject1.git
- Run the solution by clicking "IIS Express" button on the top bar


*To test on Postman
- To test send email:
  Request type: POST
  URL: https://localhost:44338/Email
  body: 
  {
  "recipient": "tutieumy2001@gmail.com",
  "subject": "no-reply email",
  "body": "This is a system email. We received your application."
   }
- To get all email history:
	Request type: GET
	URL: https://localhost:44338/api/EmailHistory
	No body

- To add a new email history (add new record to database):
	Request type: POST
	URL: https://localhost:44338/api/EmailHistory
	body:
	{
	  "recipient": "tutieumy2001@gmail.com",
	  "subject": "no-reply email",
	  "body": "This is a system email. We received your application."
	}