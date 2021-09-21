using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dto.DeliveryMethod;
using API.Entities.OrderAggregate;
using API.Helper;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Nas_Pos.Interface;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryMethodController : ControllerBase
    {
        private readonly IGenericRepository<DeliveryMethod> _repo;
        private readonly IMapper _mapper;
        public DeliveryMethodController(IGenericRepository<DeliveryMethod> repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet("deliveryMethod")]
        public async Task<ActionResult<GetDeliveryMethodDto>> GetDeliveryMethodList()
        {
            
            var obj = await _repo.GetAll();
            var mapObj = _mapper.Map<IReadOnlyList<GetDeliveryMethodDto>>(obj);
            return Ok(mapObj);
        }

        [HttpGet("deliveryMethod/{id}")]
        public async Task<ActionResult> GetDeliveryMethodById(int id)
        {
            
            var obj = await _repo.GetById(id);
            if(obj == null) return NotFound();
            var mapObj = _mapper.Map<GetDeliveryMethodDto>(obj);
            return Ok(mapObj);
        }
        
        [HttpPost("deliveryMethod")]
        public async Task<ActionResult> PostDeliveryMethod(PostDeliveryMethodDto model)
        {
            if(!ModelState.IsValid) return BadRequest();
            try{
                var mapObj = _mapper.Map<DeliveryMethod>(model);
            _repo.Insert(mapObj);
            await _repo.Save();
            return Ok((new ApiErrorResponse(ErrorStatusCode.CreateSuccess)));
            }
            catch{
                return BadRequest((new ApiErrorResponse(ErrorStatusCode.InvalidRequest)));
            }
            
            
        }

        [HttpDelete("deliveryMethod/{id}")]
        public async Task<ActionResult> DeleteDeliveryMethod(int id)
        {
            if(!ModelState.IsValid) return BadRequest();
            try{
                _repo.Delete(id);
            await _repo.Save();
            return Ok((new ApiErrorResponse(ErrorStatusCode.DeleteSuccess)));
            }
            catch{
                return BadRequest((new ApiErrorResponse(ErrorStatusCode.InvalidRequest)));
            }
            
        }

        // [HttpPatch("deliveryMethod/{id}")]
        // public async Task<ActionResult> patchProject(int id, JsonPatchDocument<PutDeliveryMethodDto> model)
        // {
        //     if (!ModelState.IsValid) return BadRequest();
        //     if (model == null) return BadRequest();
        //     var obj = await _repo.GetById(id);
        //     if (obj == null) return NotFound();
        //     var objToPatch = _mapper.Map<PutDeliveryMethodDto>(obj);
        //     model.ApplyTo(objToPatch);
        //     _mapper.Map(objToPatch, obj);
        //     _repo.Update(obj);
        //     await _repo.Save();
        //     return Ok((new ApiErrorResponse(ErrorStatusCode.UpdateSuccess)));

        // }

        
    }
        
    }
