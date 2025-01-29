using TTB.Assignment.API.Model.RequestModel;
using TTB.Assignment.API.Model;

namespace TTB.Assignment.Api.BusinessLogics.Account
{
    public interface IAccountService : IDisposable
    {
        ResponseModel<TTB.Assignment.API.Entities.Account> CreateAccount(CreateAccountRequestModel account);
        ResponseModel<List<TTB.Assignment.API.Entities.Account>> ListSavingAccount();
    }
}
