using System;
using System.Threading.Tasks;
using API.Dto;
using API.Entities.Identity;
using API.Helper;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nas_Pos.Helper;

namespace API.Data.Identity
{
    public class AccountRepository : IAccountRepository
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IdentityDbContext _context;
        private readonly ILogger<AccountRepository> _logger;
        private readonly IMapper _mapper;

        public AccountRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ILogger<AccountRepository> logger, IMapper mapper,
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

        

        public async Task<string> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if(result.Succeeded)
            {
                var roleName = await _userManager.GetRolesAsync(user);

                
                string Token = _tokenService.CreateToken(user, roleName[0]);
                return Token;
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task RegisterAdmin(Employee model, string password)
        {
            try
            {
                model.UserName=model.Email;
                var email = _userManager.FindByEmailAsync(model.Email);
                if(email !=null) throw new Exception("User with that email already exist");
                var role =await _roleManager.RoleExistsAsync(Roles.Admin);
                if(!role){
                    await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));
                }
                var result = await _userManager.CreateAsync(model, password);
                if (!result.Succeeded) throw new Exception();

                await _userManager.AddToRoleAsync(model, Roles.Admin);
                
               
                
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task RegisterCustomer(CustomerIdentity model, string password)
        {
              try
            {
                var user = await  _context.Customers.FirstOrDefaultAsync( x=> x.CustomerId == model.CustomerId);
                if(user != null) throw new Exception("User already exist");
                 model.UserName=model.Email;
                var email = _userManager.FindByEmailAsync(model.Email);
                if(email !=null) throw new Exception("User with that email already exist");
                var role =await _roleManager.RoleExistsAsync(Roles.Customer);
                if(!role){
                    await _roleManager.CreateAsync(new IdentityRole(Roles.Customer));
                }
                var result = await _userManager.CreateAsync(model, password);
                if (!result.Succeeded) throw new Exception();

                await _userManager.AddToRoleAsync(model, Roles.Customer);
                
               
                
            }
            catch (Exception ex)
            {
                throw new Exception( ex.Message);
            }
        }

        public async Task RegisterEmployee(Employee model,string password)
        {
             try
            {
                model.UserName=model.Email;
                var email = _userManager.FindByEmailAsync(model.Email);
                if(email !=null) throw new Exception("User with that email already exist");
                var role =await _roleManager.RoleExistsAsync(Roles.Employee);
                if(!role){
                    await _roleManager.CreateAsync(new IdentityRole(Roles.Employee));
                }
                var result = await _userManager.CreateAsync(model, password);
                if (!result.Succeeded) throw new Exception();

                await _userManager.AddToRoleAsync(model, Roles.Employee);
                
               
                
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }

        public async Task CustomerResetPassword(CustomerResetPasswordDto model)
        {
            try{
            var user = await  _context.Customers.FirstOrDefaultAsync( x=> x.CustomerId == model.Id);
            if(user == null) throw new Exception(ErrorStatusCode.CustomerNotFound);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
            }
            catch(Exception ex){
                throw new Exception(ex.Message);
            }
            
        }
    }
}