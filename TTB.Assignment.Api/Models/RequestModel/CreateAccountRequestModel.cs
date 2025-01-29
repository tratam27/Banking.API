using System.ComponentModel.DataAnnotations;

namespace TTB.Assignment.API.Model.RequestModel
{
    public class CreateAccountRequestModel
    {
        public required string AccountNumber { get; set; }
    }
}
