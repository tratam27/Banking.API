using TTB.Assignment.API.Model;
using TTB.Assignment.API.Models.RequestModel;

namespace TTB.Assignment.API.BusinessLogics.Transaction
{
    public interface ITransactionService : IDisposable
    {
        Task<ResponseModel<string>> Deposit(DepositRequestModel req);
        Task<ResponseModel<string>> Transfer(TransferRequestModel req);
        Task<ResponseModel<string>> Withdraw(WithdrawRequestModel req);
    }
}
