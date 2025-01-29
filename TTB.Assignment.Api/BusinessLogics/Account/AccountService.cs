using TTB.Assignment.API.BusinessLogics.Transaction;
using TTB.Assignment.API.Model.RequestModel;
using TTB.Assignment.API.Entities;
using TTB.Assignment.API.Repositories;
using TTB.Assignment.API.Model;
using System.Reflection;
using static TTB.Assignment.API.Enums.GlobalEnum;

namespace TTB.Assignment.Api.BusinessLogics.Account
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AccountService> _logger;
        public AccountService(ILogger<AccountService> logger, AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public ResponseModel<TTB.Assignment.API.Entities.Account> CreateAccount(CreateAccountRequestModel account)
        {
            try
            {
                if (account == null) return new ResponseModel<TTB.Assignment.API.Entities.Account>(400, "ข้อมูลไม่ถูกต้อง");
                if (string.IsNullOrEmpty(account.AccountNumber)) return new ResponseModel<TTB.Assignment.API.Entities.Account>(400, "ข้อมูลไม่ถูกต้อง (AccountNumber is required)");

                var newAccount = new TTB.Assignment.API.Entities.Account(account.AccountNumber, AccountType.Saving.ToString());
                _context.Accounts.Add(newAccount);
                _context.SaveChanges();

                return new ResponseModel<TTB.Assignment.API.Entities.Account>(200, "สร้างบัญชีสำเร็จ",newAccount);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(MethodBase.GetCurrentMethod().Name + "occur error exception => {message}", ex.ToString());
                return new ResponseModel<TTB.Assignment.API.Entities.Account>(500, "ระบบขัดข้อง");
            }
        }

        public ResponseModel<List<TTB.Assignment.API.Entities.Account>> ListSavingAccount()
        {
            try
            {
                var accounts = _context.Accounts.Where(x => x.AccountType == AccountType.Saving.ToString()).ToList();

                return new ResponseModel<List<TTB.Assignment.API.Entities.Account>>(200, $"บัญชีออมทรัพย์ทั้งหมด {accounts.Count} รายการ", accounts);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(MethodBase.GetCurrentMethod().Name + "occur error exception => {message}", ex.ToString());
                return new ResponseModel<List<TTB.Assignment.API.Entities.Account>>(500, "ระบบขัดข้อง");
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
