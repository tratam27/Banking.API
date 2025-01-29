using System.Reflection;
using System.Transactions;
using TTB.Assignment.API.Entities;
using TTB.Assignment.API.Model;
using TTB.Assignment.API.Models.RequestModel;
using TTB.Assignment.API.Repositories;

namespace TTB.Assignment.API.BusinessLogics.TransactionLog
{
    public class TransactionLogService : ITransactionLogService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TransactionLogService> _logger;
        public TransactionLogService(ILogger<TransactionLogService> logger, AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public ResponseModel<string> SaveTransactionLog(TransferRequestModel req)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var fromAccount = _context.Accounts.FirstOrDefault(acc => acc.AccountNumber == req.from_account_id);
                    if (fromAccount == null) return new ResponseModel<string>(400, "ข้อมูลไม่ถูกต้อง (from_account_id not found)");
                    var toAccount = _context.Accounts.FirstOrDefault(acc => acc.AccountNumber == req.to_account_id);
                    if (toAccount == null) return new ResponseModel<string>(400, "ข้อมูลไม่ถูกต้อง (to_account_id not found)");
                    if (req.amount <= 0) return new ResponseModel<string>(400, "ข้อมูลไม่ถูกต้อง (amount must more than 0)");

                    fromAccount.Balance -= req.amount;
                    if(fromAccount.Balance < 0) return new ResponseModel<string>(400, "ยอดเงินไม่เพียงพอ");
                    toAccount.Balance += req.amount;

                    _context.Update(fromAccount);
                    _context.Update(toAccount);
                    _context.SaveChanges();

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

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
