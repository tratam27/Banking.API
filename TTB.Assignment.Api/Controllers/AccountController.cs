using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Reflection;
using TTB.Assignment.Api.BusinessLogics.Account;
using TTB.Assignment.API.BusinessLogics.Transaction;
using TTB.Assignment.API.Model;
using TTB.Assignment.API.Model.AppSettingModel;
using TTB.Assignment.API.Model.RequestModel;
using TTB.Assignment.API.Models.RequestModel;
using TTB.Assignment.API.Repositories;

namespace TTB.Assignment.API.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly AppSettingModel _setting;
        private readonly AppDbContext _context;
        private readonly IAccountService _accountService;

        public AccountController(IOptions<AppSettingModel> setting, ILogger<AccountController> logger, AppDbContext context
            , IAccountService accountService)
        {
            _logger = logger;
            _setting = setting.Value;
            _context = context;
            _accountService = accountService;
        }

        [Authorize]
        [HttpPost("createSavingAccount")]
        public async Task<IActionResult> CreateSavingAccount(CreateAccountRequestModel req)
        {
            try
            {
                _logger.LogInformation("CreateAccount request body => {req}", req);

                var response = _accountService.CreateAccount(req);

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
        [HttpGet("listSavingAccount")]
        public async Task<IActionResult> ListSavingAccount()
        {
            try
            {
                _logger.LogInformation("ListSavingAccount executed");

                var response = _accountService.ListSavingAccount();

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
