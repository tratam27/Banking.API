namespace TTB.Assignment.API.Models.RequestModel
{
    public class WithdrawRequestModel
    {
        public required string account_id { get; set; }
        public required decimal amount { get; set; }
    }
}
