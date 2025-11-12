namespace api.Data.Models
{
    public class MailTemplateTranslation
    {
        public Guid Id { get; set; }
        public Guid RefTemplate { get; set; }
        public string Locale { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string HtmlBody { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }

        public MailTemplate? Template { get; set; } = null!;
    }
}
