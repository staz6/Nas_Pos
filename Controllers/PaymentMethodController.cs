using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities.OrderAggregate;
using API.Helper;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nas_Pos.Dto.PaymentMethod;
using Nas_Pos.Interface;

namespace Nas_Pos.Controllers
{
    [ApiController]
    [Route("api/[controller")]
    public class PaymentMethodController : ControllerBase
    {
         private readonly IGenericRepository<PaymentMethod> _repo;
        private readonly IMapper _mapper;
        public PaymentMethodController(IGenericRepository<PaymentMethod> repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet("deliveryMethod")]
        public async Task<ActionResult<GetPaymentMethodDto>> GetPaymentMethodList()
        {
            
            var obj = await _repo.GetAll();
            var mapObj = _mapper.Map<IReadOnlyList<GetPaymentMethodDto>>(obj);
            return Ok(mapObj);
        }

        [HttpGet("deliveryMethod/{id}")]
        public async Task<ActionResult> GetPaymentMethodById(int id)
        {
            
            var obj = await _repo.GetById(id);
            if(obj == null) return NotFound();
            var mapObj = _mapper.Map<GetPaymentMethodDto>(obj);
            return Ok(mapObj);
        }
        
        [HttpPost("deliveryMethod")]
        public async Task<ActionResult> PostPaymentMethod(PostPaymentMethodDto model)
        {
            if(!ModelState.IsValid) return BadRequest();
            try{
                var mapObj = _mapper.Map<PaymentMethod>(model);
            _repo.Insert(mapObj);
            await _repo.Save();
            return Ok(new ObjectResult(new ApiErrorResponse(ErrorStatusCode.CreateSuccess)));
            }
            catch{
                return BadRequest(new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidRequest)));
            }
            
            
        }

        [HttpDelete("deliveryMethod/{id}")]
        public async Task<ActionResult> DeletePaymentMethod(int id)
        {
            if(!ModelState.IsValid) return BadRequest();
            try{
                _repo.Delete(id);
            await _repo.Save();
            return Ok(new ObjectResult(new ApiErrorResponse(ErrorStatusCode.DeleteSuccess)));
            }
            catch{
                return BadRequest(new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidRequest)));
            }
            
        }

        // [HttpPatch("deliveryMethod/{id}")]
        // public async Task<ActionResult> patchProject(int id, JsonPatchDocument<PutPaymentMethodDto> model)
        // {
        //     if (!ModelState.IsValid) return BadRequest();
        //     if (model == null) return BadRequest();
        //     var obj = await _repo.GetById(id);
        //     if (obj == null) return NotFound();
        //     var objToPatch = _mapper.Map<PutPaymentMethodDto>(obj);
        //     model.ApplyTo(objToPatch);
        //     _mapper.Map(objToPatch, obj);
        //     _repo.Update(obj);
        //     await _repo.Save();
        //     return Ok(new ObjectResult(new ApiErrorResponse(ErrorStatusCode.UpdateSuccess)));

        // }

    }
}