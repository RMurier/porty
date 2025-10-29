namespace api.Data.Models
{
    public class Certificate
    {
        public Guid? Id { get; set; }
        public required string Name { get; set; }
        public DateTime? Date { get; set; }
        public string? Url { get; set; }
    }
}