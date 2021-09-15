using System.Threading.Tasks;
using API.Dto;
using API.Entities.Identity;
using API.Helper;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// For the actual implementation goto AccountRepository
        /// </summary>
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepo;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IAccountRepository accountRepo, IMapper mapper, ILogger<AccountController> logger)
        {
            _logger = logger;
            _accountRepo = accountRepo;
            _mapper = mapper;
        }

        public async Task<ActionResult<string>> Login(LoginDto model)
        {
            if(!ModelState.IsValid) return BadRequest();
            try{
                await _accountRepo.Login(model);
                return "ok";
            }
            catch{
                return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidLogin));
            }
        }
        public async Task<ActionResult> RegisterEmployee(RegisterEmployeeDto model)
        {
            if(!ModelState.IsValid) return BadRequest();
            var obj = _mapper.Map<Employee>(model);
            try{
                await _accountRepo.RegisterEmployee(obj);
                return Ok(new ObjectResult(new ApiErrorResponse(ErrorStatusCode.ValidRegister)));
            }
            catch{
                return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidRegister));
            }
        }

        public async Task<ActionResult> RegisterCustomer(RegisterCustomerDto model)
        {
             if(!ModelState.IsValid) return BadRequest();
            var obj = _mapper.Map<Customer>(model);
            try{
                await _accountRepo.RegisterCustomer(obj);
                return Ok(new ObjectResult(new ApiErrorResponse(ErrorStatusCode.ValidRegister)));
            }
            catch{
                return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidRegister));
            }
        }
        
    }
}