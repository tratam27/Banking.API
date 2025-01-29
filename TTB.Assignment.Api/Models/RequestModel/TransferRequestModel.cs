namespace TTB.Assignment.API.Models.RequestModel
{
    public class TransferRequestModel
    {
        public required string from_account_id { get; set; }
        public required string to_account_id { get; set; }
        public required decimal amount { get; set; }
    }
}
