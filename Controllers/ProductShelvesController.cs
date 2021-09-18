using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dto.ProductShelves;
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
    public class ProductShelvesController : ControllerBase
    {
        private readonly IGenericRepository<ProductShelves> _repo;
        private readonly IMapper _mapper;
        public ProductShelvesController(IGenericRepository<ProductShelves> repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet("productShelves")]
        public async Task<ActionResult<GetProductShelvesDto>> GetProductShelvesList()
        {
            
            var obj =await  _repo.GetAll();
            var mapObj = _mapper.Map<IReadOnlyList<GetProductShelvesDto>>(obj);
            return Ok(mapObj);
        }

        // [HttpGet("ProductShelves/{id}")]
        // public async Task<ActionResult> GetProductShelvesById(int id)
        // {
        //     var spec = new GetProductShelvesWithProducts(id);
        //     var obj = await _repo.GetEntityWithSpec(spec);
        //     if(obj == null) return NotFound();
        //     var mapObj = _mapper.Map<GetProductShelvesDto>(obj);
        //     return Ok(mapObj);
        // }
        [HttpPost("productShelves")]
        public async Task<ActionResult> PostProductShelves(PostProductShelvesDto model)
        {
            if(!ModelState.IsValid) return BadRequest();
            var mapObj = _mapper.Map<ProductShelves>(model);
            _repo.Insert(mapObj);
            await _repo.Save();
            return Ok(new ObjectResult(new ApiErrorResponse(ErrorStatusCode.CreateSuccess)));
        }

        [HttpDelete("productShelves/{id}")]
        public async Task<ActionResult> DeleteProductShelves(int id)
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

        [HttpPatch("productShelves/{id}")]
        public async Task<ActionResult> patchProject(int id, JsonPatchDocument<PutProductShelvesDto> model)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (model == null) return BadRequest();
            var obj = await _repo.GetById(id);
            if (obj == null) return NotFound();
            var objToPatch = _mapper.Map<PutProductShelvesDto>(obj);
            model.ApplyTo(objToPatch);
            _mapper.Map(objToPatch, obj);
            _repo.Update(obj);
            await _repo.Save();
            return Ok(new ObjectResult(new ApiErrorResponse(ErrorStatusCode.UpdateSuccess)));

        }

        

        
    }
}