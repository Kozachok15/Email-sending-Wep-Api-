using MailsWepApi.Models;
using MailsWepApi.Services;
using Microsoft.AspNetCore.Mvc;
using MailsWepApi.DataBase;
using Org.BouncyCastle.Cms;
using Newtonsoft.Json;
using MailsWepApi.Services.Settings;
using Microsoft.Extensions.Options;

namespace MailsWepApi.Controllers
{
    [ApiController]
    [Route("api/mails")]
    public class MailsController : ControllerBase
    {
        private List<MailDto> storedMails;

        private readonly IConfiguration configuration;
        private readonly IOptions<SmtpSettings> smtpSettings;

        public MailsController(IConfiguration configuration, IOptions<SmtpSettings> smtpSettings)
        {
            this.configuration = configuration;
            this.smtpSettings = smtpSettings;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<MailDto> mails)
        {
            if (mails == null || mails.Count == 0)
            {
                return BadRequest("Invalid JSON payload");
            }
            List<Task> sendTasks = new List<Task>();
            EmailService emailService = new EmailService(smtpSettings);
            storedMails = mails;

            string errorMessage = String.Empty;
            try
            {
                foreach (MailDto mail in storedMails)
                {
                    // Асинхронно отправляем каждое письмо
                    foreach (string recipient in mail.Recipients)
                    {
                        sendTasks.Add(emailService.SendEmailAsync(recipient, mail.Subject, mail.Body));
                    }
                }

                // Дожидаемся окончания отправки всех писем
                await Task.WhenAll(sendTasks);

                foreach (MailDto mail in storedMails)
                {
                    string recipients = string.Empty;
                    foreach (string recipient in mail.Recipients)
                    {
                        recipients += $"{recipient}, ";
                    }

                    using (var context = new DBContext(configuration))
                    {
                        Mail mailDB = MailDto.MapFrom(mail, recipients, true, errorMessage);
                        context.Mail.Add(mailDB);
                        context.SaveChanges();
                    }
                }
                

                return Ok("Mail received and stored successfully");
            }
            catch (Exception ex)
            {
                foreach (MailDto mail in storedMails)
                {
                    string recipients = string.Empty;
                    foreach (string recipient in mail.Recipients)
                    {
                        recipients += $"{recipient}, ";
                    }

                    errorMessage = ex.Message;                    

                    using (var context = new DBContext(configuration))
                    {
                        Mail mailDB = MailDto.MapFrom(mail, recipients, false, errorMessage);
                        context.Mail.Add(mailDB);
                        context.SaveChanges();
                    }
                }

                return BadRequest("Mail received but not stored successfully");
            }

            
        }

        [HttpGet]
        public IActionResult Get()
        {
            var jsonResponses = new List<string>();            
            using (var context = new DBContext(configuration))
            {
                var mails = context.Mail;
                
                foreach(var mail in mails)
                {
                    string jsonRequest = JsonConvert.SerializeObject(mail);
                    jsonResponses.Add(jsonRequest);
                }
            }

            return Ok(jsonResponses);
        }
    }
}
