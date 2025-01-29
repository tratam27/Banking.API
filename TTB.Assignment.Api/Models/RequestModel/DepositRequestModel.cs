namespace TTB.Assignment.API.Models.RequestModel
{
    public class DepositRequestModel
    {
        public required string account_id { get; set; }
        public required decimal amount { get; set; }
    }
}
