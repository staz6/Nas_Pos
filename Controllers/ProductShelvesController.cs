using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dto.ProductShelves;
using API.Entities;
using API.Helper;
using API.Specification;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Nas_Pos.Helper;
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
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
        [HttpGet("productShelves/{shopId}")]
        public async Task<ActionResult<GetProductShelvesDto>> GetProductShelvesList(int shopId)
        {   
            string ownerId = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;  
            var spec = new GetProductShelvesWithShopId(ownerId,shopId);
            var obj =await  _repo.ListAsyncWithSpec(spec);
            var mapObj = _mapper.Map<IReadOnlyList<GetProductShelvesDto>>(obj);
            return Ok(mapObj);
        }

       
        [HttpPost("productShelves")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
        public async Task<ActionResult> PostProductShelves(PostProductShelvesDto model)
        {
            
            if(!ModelState.IsValid) return BadRequest();
            var mapObj = _mapper.Map<ProductShelves>(model);
            _repo.Insert(mapObj);
            await _repo.Save();
            return Ok((new ApiErrorResponse(ErrorStatusCode.CreateSuccess)));
        }

        [HttpDelete("productShelves/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
        public async Task<ActionResult> DeleteProductShelves(int id)
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

        [HttpPatch("productShelves/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
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
            return Ok((new ApiErrorResponse(ErrorStatusCode.UpdateSuccess)));

        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Employee)]
        [HttpGet("productShelvesForEmployee")]
        public async Task<ActionResult<GetProductShelvesDto>> getProductTypeOnly()
        {
            int shopId = int.Parse(HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value);
            var spec = new GetProductShelvesWithShopId(shopId);
            var obj = await _repo.ListAsyncWithSpec(spec);
            var mapObj = _mapper.Map<IReadOnlyList<GetProductShelvesDto>>(obj);
            return Ok(mapObj);
        }

        

        
    }
}