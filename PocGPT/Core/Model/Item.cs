namespace PocGPT.Core.Model
{
    public class Item
    {
        public string? Id { get; set; }
        public string? Direction { get; set; }
        public string? Type { get; set; }
        public object? Content { get; set; }
        public DateTime? Date { get; set; }
        public string? Status { get; set; }
        public Metadata? Metadata { get; set; }
    }
}
