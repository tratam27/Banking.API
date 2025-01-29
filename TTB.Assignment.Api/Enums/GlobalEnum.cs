namespace TTB.Assignment.API.Enums
{
    public class GlobalEnum
    {
        public enum TransactionType
        {
            Deposit = 1,
            Withdrawal = 2,
            Transfer = 3
        }
        public enum TransactionStatus
        {
            Pending = 0,
            Completed = 1,
            Fail = 2
        }

        public enum AccountType
        {
            Saving,
            Investing
        }
    }
}
