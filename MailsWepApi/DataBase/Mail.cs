using System;
using System.Collections.Generic;

namespace MailsWepApi.DataBase
{
    public partial class Mail
    {
        public int Id { get; set; }
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string Recipients { get; set; } = null!;
        public DateTime CreateTime { get; set; }
        public bool Result { get; set; }
        public string? FailedMessage { get; set; }
    }
}
