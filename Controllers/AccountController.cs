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
                return Ok(new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidLogin)));
            }
        }
        [HttpPost("register")]
        public async Task<ActionResult> RegisterEmployee(RegisterEmployeeDto model)
        {
            if(!ModelState.IsValid) return BadRequest();
            var obj = _mapper.Map<Employee>(model);
            try{
                await _accountRepo.RegisterEmployee(obj,model.Password);
                return Ok(new ObjectResult(new ApiErrorResponse(ErrorStatusCode.ValidRegister)));
            }
            catch{
                return BadRequest(new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidRegister)));
            }
        }
        [HttpPost("registerAdmin")]
        public async Task<ActionResult> RegisterAdmin(RegisterEmployeeDto model)
        {
            if(!ModelState.IsValid) return BadRequest();
            var obj = _mapper.Map<Employee>(model);
            try{
                await _accountRepo.RegisterAdmin(obj,model.Password);
                return Ok(new ObjectResult(new ApiErrorResponse(ErrorStatusCode.ValidRegister)));
            }
            catch{
                return BadRequest(new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidRegister)));
            }
        }

        
        
    }
}