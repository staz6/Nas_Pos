using System.Threading.Tasks;
using API.Dto;
using API.Entities.Identity;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace API.Data.Identity
{
    public class AccountRepository : IAccountRepository
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IdentityDbContext _context;
        private readonly ILogger<AccountRepository> _logger;
        private readonly IMapper _mapper;

        public AccountRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AccountRepository> logger, IMapper mapper,
             RoleManager<IdentityRole> roleManager, ITokenService tokenService, IdentityDbContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public Task Login(LoginDto model)
        {
            throw new System.NotImplementedException();
        }

        public Task RegisterCustomer(Customer model)
        {
            throw new System.NotImplementedException();
        }

        public Task RegisterEmployee(Employee model)
        {
            throw new System.NotImplementedException();
        }
    }
}