using System.ComponentModel.DataAnnotations.Schema;

namespace TTB.Assignment.API.Entities
{
    public class Transaction
    {
        public long TransactionId { get; set; }
        public int AccountId { get; set; } // Foreign key
        public string AccountNumber { get; set; }
        public string TransactionType { get; set; } = "Deposit"; // "Deposit", "Withdrawal", "Transfer"
        public decimal Amount { get; set; }
        public int? ReferenceAccountId { get; set; } // For transfers
        public string? ReferenceAccountNo { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending"; // "Pending", "Completed", "Failed"
        public string? Notes { get; set; }

        
        public Account Account { get; set; }
        public Account? ReferenceAccount { get; set; }
    }
}
