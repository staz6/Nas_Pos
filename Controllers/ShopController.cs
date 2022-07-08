using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dto.Shop;
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
    [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin )]
    public class ShopController : ControllerBase
    {
        private readonly IGenericRepository<Shop> _repo;
        private readonly IMapper _mapper;
        public ShopController(IGenericRepository<Shop> repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        
        [HttpGet("shop")]
        public async Task<ActionResult<IReadOnlyList<GetShopDto>>> GetShopList()
        {
            string ownderId = (HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value) ;
            var spec = new GetShopWithOwnerIdSpecification(ownderId);
            var obj = await _repo.ListAsyncWithSpec(spec);
            var mapObj = _mapper.Map<IReadOnlyList<GetShopDto>>(obj);
            return Ok(mapObj);
        }

        [HttpGet("shop/{id}")]
        public async Task<ActionResult<GetShopDto>> GetShopById(int id)
        {
            string ownderId = (HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value) ;
            var obj = await _repo.GetById(id);
            if(obj == null) return NotFound(new ApiErrorResponse(ErrorStatusCode.NotFound));
            var mapObj = _mapper.Map<GetShopDto>(obj);
            if(obj.OwnerId != ownderId) return NotFound(new ApiErrorResponse(ErrorStatusCode.NotFound));
            return Ok(mapObj);
        }
        
        [HttpPost("shop")]
        public async Task<ActionResult> PostShop(PostShopDto model)
        {
            if(!ModelState.IsValid) return BadRequest();
            // try{
                var mapObj = _mapper.Map<Shop>(model);
            _repo.Insert(mapObj);
            await _repo.Save();
            return Ok((new ApiErrorResponse(ErrorStatusCode.CreateSuccess)));
            // }
            // catch{
            //     return BadRequest((new ApiErrorResponse(ErrorStatusCode.InvalidRequest)));
            // }
            
            
        }

        [HttpDelete("shop/{id}")]
        public async Task<ActionResult> DeleteShop(int id)
        {
            if(!ModelState.IsValid) return BadRequest();
            try{
            string ownderId = (HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value) ;
            var obj = await _repo.GetById(id);
            if(obj == null) return NotFound(new ApiErrorResponse(ErrorStatusCode.NotFound));
            if(obj.OwnerId != ownderId) return NotFound(new ApiErrorResponse(ErrorStatusCode.NotFound));
                _repo.Delete(id);
            await _repo.Save();
            return Ok((new ApiErrorResponse(ErrorStatusCode.DeleteSuccess)));
            }
            catch{
                return BadRequest((new ApiErrorResponse(ErrorStatusCode.InvalidRequest)));
            }
            
        }

        [HttpGet("getNameAndId")]
        public async Task<ActionResult<IReadOnlyList<GetShopNameAndId>>> getNameAndId()
        {
            string ownderId = (HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value) ;
            var spec = new GetShopWithOwnerIdSpecification(ownderId);
            var obj = await _repo.ListAsyncWithSpec(spec);
            var mapObj = _mapper.Map<IReadOnlyList<GetShopNameAndId>>(obj);
            return Ok(mapObj);
        }

        [HttpPatch("shop/{id}")]
        public async Task<ActionResult> patchProject(int id, JsonPatchDocument<PutShopDto> model)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (model == null) return BadRequest();
            var obj = await _repo.GetById(id);
            if (obj == null) return NotFound();
            var objToPatch = _mapper.Map<PutShopDto>(obj);
            model.ApplyTo(objToPatch);
            _mapper.Map(objToPatch, obj);
            _repo.Update(obj);
            await _repo.Save();
            return Ok((new ApiErrorResponse(ErrorStatusCode.UpdateSuccess)));

        }

        
    }
}