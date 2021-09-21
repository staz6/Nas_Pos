using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dto.Customer;
using API.Entities;
using API.Helper;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Nas_Pos.Interface;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IGenericRepository<Customer> _repo;
        private readonly IMapper _mapper;
        public CustomerController(IGenericRepository<Customer> repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet("customer")]
        public async Task<ActionResult<GetCustomerDto>> GetCustomerList()
        {     
            var obj =await  _repo.GetAll();
            var mapObj = _mapper.Map<IReadOnlyList<GetCustomerDto>>(obj);
            return Ok(mapObj);
        }

        // [HttpGet("Customer/{id}")]
        // public async Task<ActionResult> GetCustomerById(int id)
        // {
        //     var spec = new GetCustomerWithProducts(id);
        //     var obj = await _repo.GetEntityWithSpec(spec);
        //     if(obj == null) return NotFound();
        //     var mapObj = _mapper.Map<GetCustomerDto>(obj);
        //     return Ok(mapObj);
        // }
        [HttpPost("customer")]
        public async Task<ActionResult> PostCustomer(PostCustomerDto model)
        {
            if(!ModelState.IsValid) return BadRequest();
            var mapObj = _mapper.Map<Customer>(model);
            _repo.Insert(mapObj);
            await _repo.Save();
            return Ok((new ApiErrorResponse(ErrorStatusCode.CreateSuccess)));
        }

        [HttpDelete("customer/{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
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

        [HttpPatch("customer/{id}")]
        public async Task<ActionResult> patchProject(int id, JsonPatchDocument<PutCustomerDto> model)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (model == null) return BadRequest();
            var obj = await _repo.GetById(id);
            if (obj == null) return NotFound();
            var objToPatch = _mapper.Map<PutCustomerDto>(obj);
            model.ApplyTo(objToPatch);
            _mapper.Map(objToPatch, obj);
            _repo.Update(obj);
            await _repo.Save();
            return Ok((new ApiErrorResponse(ErrorStatusCode.UpdateSuccess)));

        }
    }
}