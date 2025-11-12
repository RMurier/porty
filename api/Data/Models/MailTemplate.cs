namespace api.Data.Models
{
    public class MailTemplate
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public ICollection<MailTemplateTranslation> Translations { get; set; } = new List<MailTemplateTranslation>();
    }
}
