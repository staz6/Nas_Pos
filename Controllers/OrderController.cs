using System;
using System.Threading.Tasks;
using API.Dto;
using API.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        public OrderController(IOrderService service)
        {
            _service = service;
        }
        [HttpPost("order")]
        public async Task<ActionResult> postOrder(BasketDto model)
        {
            
            try{
                await _service.AddOrder(model);
                return Ok();
            }
            catch(Exception ex){
                return BadRequest(new ObjectResult(ex.Message));
            }
        }
    }
}