using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dto;
using API.Dto.Order;
using API.Entities.OrderAggregate;
using API.Helper;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        private readonly IMapper _mapper;
        public OrderController(IOrderService service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }
        [HttpPost("order")]
        public async Task<ActionResult> postOrder(BasketDto model)
        {

            try
            {
                await _service.AddOrder(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest((ex.Message));
            }
        }
        [HttpGet("order")]
        public async Task<ActionResult<IReadOnlyList<GetOrderDto>>> GetOrder()
        {
            var obj = await _service.GetAllOrder();
            var mapObj = _mapper.Map<IReadOnlyList<GetOrderDto>>(obj);
            return Ok(mapObj);
        }
        [HttpGet("customerOrder/{customerId}")]
        public async Task<ActionResult<IReadOnlyList<GetOrderDto>>>  GetOrderByCustomerId(string customerId)
        {
            var obj = await _service.GetOrderByCustomerId(customerId);
            if(obj == null) return NotFound(new ApiErrorResponse(ErrorStatusCode.NotFound));
            var mapObj = _mapper.Map<IReadOnlyList<GetOrderDto>>(obj);
            return Ok(mapObj);
        }
        [HttpGet("order/id")]
        public async Task<ActionResult<GetOrderDto>> GetOrderById(int id)
        {
            var obj = await _service.GetOrderById(id);
            if(obj == null) return NotFound(new ApiErrorResponse(ErrorStatusCode.NotFound));
            var mapObj = _mapper.Map<GetOrderDto>(obj);
            return Ok(mapObj);
        }
        
        [HttpGet("sortOrder/{statusId}")]
        public async Task<ActionResult<IReadOnlyList<GetOrderDto>>>  GetOrderBySort(int statusId)
        {
            var obj = await _service.GetOrderBySort(statusId);
            if(obj == null) return NotFound(new ApiErrorResponse(ErrorStatusCode.NotFound));
            var mapObj = _mapper.Map<IReadOnlyList<GetOrderDto>>(obj);
            return Ok(mapObj);
        }

    }
}