using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dto.EmployeeBasket;
using API.Helper;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nas_Pos.Helper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _service;
        private readonly IMapper _mapper;
        public BasketController(IBasketService service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Employee + "," + Roles.Admin + "")]
        [HttpPost("basket/{productId}/{quantity}")]
        public async Task<ActionResult> AddItem(int productId, decimal quantity)
        {
            string userId = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            try
            {
                await _service.addItem(userId, productId, quantity);
                return Ok(new ObjectResult(new ApiErrorResponse(ErrorStatusCode.ItemAdd)));
            }
            catch(Exception)
            {
                return BadRequest(new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidRequest)));
            }

        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Employee + "," + Roles.Admin + "")]
        [HttpGet("basket")]
        public async Task<ActionResult<EmployeeBasket>> getBasket()
        {
            string userId = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            try
            {
                var basket = await _service.getBasket(userId);
                var mapObject = _mapper.Map<EmployeeBasket>(basket);
                return Ok(mapObject);
            }
            catch
            {
                return BadRequest(new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidRequest)));
            }

        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Employee + "," + Roles.Admin + "")]
        [HttpDelete("basket/{id}")]
        public async Task<ActionResult> deleteBasket(int id){
            string userId = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            try
            {
                await _service.deleteBasket(id);
                return Ok(new ObjectResult(new ApiErrorResponse(ErrorStatusCode.DeleteSuccess)));
            }
            catch(Exception ex)
            {
                return BadRequest(new ObjectResult(ex.Message));
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Employee + "," + Roles.Admin + "")]
        [HttpPut("basket/{productId}")]
        public async Task<ActionResult> removeItem(int productId){
             string userId = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            try
            {
                await _service.removeItem(userId,productId);
                return Ok(new ObjectResult(new ApiErrorResponse(ErrorStatusCode.UpdateSuccess)));
            }
            catch(Exception ex)
            {
                return BadRequest(new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidRequest)));
            }
        }
        

    }
}