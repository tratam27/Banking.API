using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Reflection;
using TTB.Assignment.API.BusinessLogics.Transaction;
using TTB.Assignment.API.Model;
using TTB.Assignment.API.Model.AppSettingModel;
using TTB.Assignment.API.Models.RequestModel;
using TTB.Assignment.API.Repositories;

namespace TTB.Assignment.API.Controllers
{
    [ApiController]
    [Route("transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly AppSettingModel _setting;
        private readonly AppDbContext _context;
        private readonly ITransactionService _transactionService;

        public TransactionController(IOptions<AppSettingModel> setting, ILogger<TransactionController> logger, AppDbContext context
            , ITransactionService transactionService)
        {
            _logger = logger;
            _setting = setting.Value;
            _context = context;
            _transactionService = transactionService;
        }

        [Authorize]
        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit(DepositRequestModel req)
        {
            try
            {
                _logger.LogInformation("Deposit request body => {req}", req);

                var response = await _transactionService.Deposit(req);

                _logger.LogInformation(MethodBase.GetCurrentMethod().Name + " success with response => {response}", response);

                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(MethodBase.GetCurrentMethod().Name + "occur error exception => {message}", ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel<object>(StatusCodes.Status500InternalServerError, ex.ToString()));
            }
        }
        [Authorize]
        [HttpPost("withdraw")]
        public async Task<IActionResult> Transfer(WithdrawRequestModel req)
        {
            try
            {
                _logger.LogInformation("Withdraw request body => {req}", req);

                var response = await _transactionService.Withdraw(req);

                _logger.LogInformation(MethodBase.GetCurrentMethod().Name + " success with response => {response}", response);

                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(MethodBase.GetCurrentMethod().Name + "occur error exception => {message}", ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel<object>(StatusCodes.Status500InternalServerError, ex.ToString()));
            }
        }
        [Authorize]
        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer(TransferRequestModel req)
        {
            try
            {
                _logger.LogInformation("Transfer request body => {req}", req);

                var response = await _transactionService.Transfer(req);

                _logger.LogInformation(MethodBase.GetCurrentMethod().Name + " success with response => {response}", response);

                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(MethodBase.GetCurrentMethod().Name + "occur error exception => {message}", ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel<object>(StatusCodes.Status500InternalServerError, ex.ToString()));
            }
        }
    }
}
