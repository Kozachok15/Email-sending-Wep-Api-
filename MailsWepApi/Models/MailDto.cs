using Newtonsoft.Json;
using MailsWepApi.DataBase;

namespace MailsWepApi.Models
{
    public class MailDto
    {
        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("recipients")]
        public List<string> Recipients { get; set; }

        public static Mail MapFrom(MailDto mailJSON, string resip, bool res, string exeption)
        {
            return new Mail
            {
                Subject = mailJSON.Subject,
                Body = mailJSON.Body,
                Recipients = resip,
                CreateTime = DateTime.Now,
                Result = res,
                FailedMessage = exeption
            };
        }
    }
}
