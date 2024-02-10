# About

This is an email sending service written in C# using ASP.NET Core Web API. The program responds to POST and GET requests at the URL: api/mails/.

+ The body of the POST request is a JSON file. The method processes the JSON file and sends the email.
JSON Model:
```json
[
  {
    "subject": "string",
    "body": "string",
    "recipients": ["string", "string"]
  }
]
```

+ All emails are logged into an MS SQL Server database with additional fields (timestamp of sending, sending result, and error text if any).

+ By sending a GET request, you can retrieve all emails from the database in JSON format.

## Frameworks & Libs

+ Microsoft.EntityFrameworkCore.SqlServer
+ MailKit
+ Newtonsoft.Json

## How to Use

The SMTP server settings and the database connection string are stored in a configuration file, and the project includes all necessary migrations for the database. To test this service, you need to configure your SMTP server data, create a database named MailsWebApi in MS SQL Server, and apply the existing migrations.
