using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.Helper;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Nas_Pos.Dto;
using Nas_Pos.Dto.ProductTypeDtos;
using Nas_Pos.Interface;
using Nas_Pos.Specification;

namespace Nas_Pos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductTypeController : ControllerBase
    {
        private readonly IGenericRepository<ProductType> _repo;
        private readonly IMapper _mapper;
        public ProductTypeController(IGenericRepository<ProductType> repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        // [HttpGet("productType")]
        // public async Task<ActionResult<GetProductTypeDto>> GetProductTypeList()
        // {
        //     var spec = new GetProductTypeWithProducts();
        //     var obj =await  _repo.ListAsyncWithSpec(spec);
        //     var mapObj = _mapper.Map<IReadOnlyList<GetProductTypeDto>>(obj);
        //     return Ok(mapObj);
        // }

        // [HttpGet("productType/{id}")]
        // public async Task<ActionResult> GetProductTypeById(int id)
        // {
        //     var spec = new GetProductTypeWithProducts(id);
        //     var obj = await _repo.GetEntityWithSpec(spec);
        //     if(obj == null) return NotFound();
        //     var mapObj = _mapper.Map<GetProductTypeDto>(obj);
        //     return Ok(mapObj);
        // }
        [HttpPost("productType")]
        public async Task<ActionResult> PostProductType(PostProductTypeDto model)
        {
            if(!ModelState.IsValid) return BadRequest();
            var mapObj = _mapper.Map<ProductType>(model);
            _repo.Insert(mapObj);
            await _repo.Save();
            return Ok((new ApiErrorResponse(ErrorStatusCode.CreateSuccess)));
        }

        [HttpDelete("productType/{id}")]
        public async Task<ActionResult> DeleteProductType(int id)
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

        [HttpPatch("productType/{id}")]
        public async Task<ActionResult> patchProject(int id, JsonPatchDocument<PutProductTypeDto> model)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (model == null) return BadRequest();
            var obj = await _repo.GetById(id);
            if (obj == null) return NotFound();
            var objToPatch = _mapper.Map<PutProductTypeDto>(obj);
            model.ApplyTo(objToPatch);
            _mapper.Map(objToPatch, obj);
            _repo.Update(obj);
            await _repo.Save();
            return Ok((new ApiErrorResponse(ErrorStatusCode.UpdateSuccess)));

        }

        [HttpGet("productTypeOnly")]
        public async Task<ActionResult<GetProductTypeOnlyDto>> getProductTypeOnly()
        {
            var obj = await _repo.GetAll();
            var mapObj = _mapper.Map<IReadOnlyList<GetProductTypeOnlyDto>>(obj);
            return Ok(mapObj);
        }

        
    }
}