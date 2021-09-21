using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.Helper;
using API.Specification;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Nas_Pos.Dto;
using Nas_Pos.Dto.ProductDtos;
using Nas_Pos.Helper;
using Nas_Pos.Interface;
using Nas_Pos.Specification;

namespace Nas_Pos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> _repo;
        private readonly IMapper _mapper;
        public ProductController(IGenericRepository<Product> repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet("product")]
        public async Task<ActionResult<GetProductDto>> GetProductList()
        {
            var spec = new GetProductWithShelvesSpecification();
            var obj = await _repo.ListAsyncWithSpec(spec);
            var mapObj = _mapper.Map<IReadOnlyList<GetProductDto>>(obj);
            return Ok(mapObj);
        }

        [HttpGet("product/{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var spec = new GetProductWithShelvesSpecification();
            var obj = await _repo.GetEntityWithSpec(spec);
            if(obj == null) return NotFound();
            var mapObj = _mapper.Map<GetProductDto>(obj);
            return Ok(mapObj);
        }
        [HttpGet("productType")]
        public async Task<ActionResult> GetProductByType(int id)
        {
            var spec = new GetProductWithShelvesSpecification();
            var obj = await _repo.GetEntityWithSpec(spec);
            if(obj == null) return NotFound();
            var mapObj = _mapper.Map<GetProductDto>(obj);
            return Ok(mapObj);
        }
        [HttpPost("product")]
        public async Task<ActionResult> PostProduct(PostProductDto model)
        {
            if(!ModelState.IsValid) return BadRequest();
            try{
                var mapObj = _mapper.Map<Product>(model);
            _repo.Insert(mapObj);
            await _repo.Save();
            return Ok((new ApiErrorResponse(ErrorStatusCode.CreateSuccess)));
            }
            catch{
                return BadRequest((new ApiErrorResponse(ErrorStatusCode.InvalidRequest)));
            }
            
            
        }

        [HttpDelete("product/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
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

        [HttpPatch("product/{id}")]
        public async Task<ActionResult> patchProject(int id, JsonPatchDocument<PutProductDto> model)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (model == null) return BadRequest();
            var obj = await _repo.GetById(id);
            if (obj == null) return NotFound();
            var objToPatch = _mapper.Map<PutProductDto>(obj);
            model.ApplyTo(objToPatch);
            _mapper.Map(objToPatch, obj);
            _repo.Update(obj);
            await _repo.Save();
            return Ok((new ApiErrorResponse(ErrorStatusCode.UpdateSuccess)));

        }

        
    }
}