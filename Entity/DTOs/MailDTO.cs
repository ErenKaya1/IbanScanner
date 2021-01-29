using System.Collections.Generic;

namespace Entity.DTOs
{
    public class MailDTO
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public List<string> To { get; set; } = new List<string>();
        public string From { get; set; }
    }
}