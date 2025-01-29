using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Transactions;
using TTB.Assignment.API.Entities;
using TTB.Assignment.API.Model;
using TTB.Assignment.API.Models.RequestModel;
using TTB.Assignment.API.Repositories;
using static TTB.Assignment.API.Enums.GlobalEnum;

namespace TTB.Assignment.API.BusinessLogics.Transaction
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TransactionService> _logger;
        public TransactionService(ILogger<TransactionService> logger, AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<ResponseModel<string>> Deposit(DepositRequestModel req)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var account = await _context.Accounts.FirstOrDefaultAsync(acc => acc.AccountNumber == req.account_id);
                    if (account == null) return new ResponseModel<string>(400, "ข้อมูลไม่ถูกต้อง (account_id not found)");
                    if (req.amount <= 0) return new ResponseModel<string>(400, "ข้อมูลไม่ถูกต้อง (amount must more than 0)");

                    account.Balance += req.amount;
                    account.UpdatedAt = DateTime.Now;

                    SaveTransaction(account,TransactionType.Deposit,req.amount,null,Enums.GlobalEnum.TransactionStatus.Completed,null);

                    await _context.SaveChangesAsync();

                    transaction.Commit();
                    return new ResponseModel<string>(200, "การฝากเงินสำเร็จ");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogInformation(MethodBase.GetCurrentMethod().Name + "occur error exception => {message}", ex.ToString());
                    return new ResponseModel<string>(500, "ระบบขัดข้อง");
                }
            }
        }

        public async Task<ResponseModel<string>> Withdraw(WithdrawRequestModel req)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var account = await _context.Accounts.FirstOrDefaultAsync(acc => acc.AccountNumber == req.account_id);
                    if (account == null) return new ResponseModel<string>(400, "ข้อมูลไม่ถูกต้อง (account_id not found)");
                    if (req.amount <= 0) return new ResponseModel<string>(400, "ข้อมูลไม่ถูกต้อง (amount must more than 0)");

                    account.Balance -= req.amount;
                    if(account.Balance < 0) return new ResponseModel<string>(403, "ยอดเงินไม่เพียงพอ");
                    account.UpdatedAt = DateTime.Now;

                    SaveTransaction(account, TransactionType.Withdrawal, req.amount, null, Enums.GlobalEnum.TransactionStatus.Completed, null);

                    await _context.SaveChangesAsync();

                    transaction.Commit();
                    return new ResponseModel<string>(200, "การถอนเงินสำเร็จ");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogInformation(MethodBase.GetCurrentMethod().Name + "occur error exception => {message}", ex.ToString());
                    return new ResponseModel<string>(500, "ระบบขัดข้อง");
                }
            }
        }

        public async Task<ResponseModel<string>> Transfer(TransferRequestModel req)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var fromAccount = await _context.Accounts.FirstOrDefaultAsync(acc => acc.AccountNumber == req.from_account_id);
                    if (fromAccount == null) return new ResponseModel<string>(400, "ข้อมูลไม่ถูกต้อง (from_account_id not found)");
                    var toAccount = await _context.Accounts.FirstOrDefaultAsync(acc => acc.AccountNumber == req.to_account_id);
                    if (toAccount == null) return new ResponseModel<string>(400, "ข้อมูลไม่ถูกต้อง (to_account_id not found)");
                    if (req.amount <= 0) return new ResponseModel<string>(400, "ข้อมูลไม่ถูกต้อง (amount must more than 0)");

                    fromAccount.Balance -= req.amount;
                    if(fromAccount.Balance < 0) return new ResponseModel<string>(400, "ยอดเงินไม่เพียงพอ");
                    toAccount.Balance += req.amount;

                    fromAccount.UpdatedAt = DateTime.Now;
                    toAccount.UpdatedAt = DateTime.Now;

                    SaveTransaction(fromAccount, TransactionType.Transfer, req.amount, toAccount, Enums.GlobalEnum.TransactionStatus.Completed, null);

                    await _context.SaveChangesAsync();
                    transaction.Commit();
                    return new ResponseModel<string>(200, "การโอนเงินสำเร็จ");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogInformation(MethodBase.GetCurrentMethod().Name + "occur error exception => {message}", ex.ToString());
                    return new ResponseModel<string>(500, "ระบบขัดข้อง");
                }
            }
        }

        private async void SaveTransaction(Account account, TransactionType transactionType,
            decimal amount, Account? referenceAccount,
            Enums.GlobalEnum.TransactionStatus status,string? note)
        {
            try
            {
                _context.Transactions.Add(new Entities.Transaction()
                {
                    AccountId = account.AccountId,
                    AccountNumber = account.AccountNumber,
                    TransactionType = transactionType.ToString(),
                    Amount = amount,
                    ReferenceAccountId = referenceAccount?.AccountId,
                    ReferenceAccountNo = referenceAccount?.AccountNumber,
                    Status = status.ToString(),
                    Timestamp = DateTime.Now,
                    Notes = note
                });

                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
