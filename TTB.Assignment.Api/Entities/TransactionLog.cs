namespace TTB.Assignment.API.Entities
{
    public class TransactionLog
    {
        public long LogId { get; set; }
        public int UserId { get; set; } // Foreign key
        public string Action { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string? IPAddress { get; set; }
        public string? Details { get; set; }

        
        public User User { get; set; } = null!;
    }
}
