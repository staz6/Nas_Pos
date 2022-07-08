using System;
using System.Threading.Tasks;
using API.Dto;
using API.Entities.Identity;
using API.Helper;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto model)
        {
            if(!ModelState.IsValid) return BadRequest();
            try{
                string token = await _accountRepo.Login(model);
                return token;
            }
            catch{
                return Ok((new ApiErrorResponse(ErrorStatusCode.InvalidLogin)));
            }
        }
        [HttpPost("registerEmployee")]
        public async Task<ActionResult> RegisterEmployee(RegisterEmployeeDto model)
        {
            if(!ModelState.IsValid) return BadRequest();
            var employee = _mapper.Map<Employee>(model);
            var obj = _mapper.Map<AppUser>(model);
            try{
                await _accountRepo.RegisterEmployee(employee,obj,model.Password);
                return Ok((new ApiErrorResponse(ErrorStatusCode.ValidRegister)));
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("registerAdmin")]
        public async Task<ActionResult> RegisterAdmin(RegisterAdminDto model)
        {
            if(!ModelState.IsValid) return BadRequest();
            var obj = _mapper.Map<AppUser>(model);
            var admin = _mapper.Map<Admin>(model);
            try{
                await _accountRepo.RegisterAdmin(admin,obj,model.Password);
                return Ok((new ApiErrorResponse(ErrorStatusCode.ValidRegister)));
            }
            catch{
                return BadRequest((new ApiErrorResponse(ErrorStatusCode.InvalidRegister)));
            }
        }

        [HttpPost("registerCustomer")]
        public async Task<ActionResult> RegisteCustomer(RegisterCustomerDto model)
        {
            if(!ModelState.IsValid) return BadRequest();
            var obj = _mapper.Map<AppUser>(model);
            var customer = _mapper.Map<CustomerIdentity>(model);
            try{
                await _accountRepo.RegisterCustomer(customer,obj,model.Password);
                return Ok((new ApiErrorResponse(ErrorStatusCode.ValidRegister)));
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("resetCustomerPassword")]
        public async Task<ActionResult> ResetPassworCustomer(CustomerResetPasswordDto model)
        {
            if(!ModelState.IsValid) return BadRequest();
  
            try{
                await _accountRepo.CustomerResetPassword(model);
                return Ok((new ApiErrorResponse(ErrorStatusCode.ValidRegister)));
            }
            catch(Exception ){
                return BadRequest((new ApiErrorResponse(ErrorStatusCode.InvalidRegister)));
            }
        }

        
        
    }
}