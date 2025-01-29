namespace TTB.Assignment.API.Entities
{
    public class Account
    {
        public int AccountId { get; set; }
        //public int UserId { get; set; } // Foreign key
        public string AccountNumber { get; set; } = string.Empty;
        public string AccountType { get; set; } = "Savings"; // "Savings" or "Investing"
        public decimal Balance { get; set; } = 0.00m;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        
        public List<Transaction> Transactions { get; set; } = new();

        //Create new account constructor
        public Account(string accountNumber, string accountType)
        {
            this.AccountNumber = accountNumber;
            this.AccountType = accountType;
            this.Balance = 0;
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }
    }
}
