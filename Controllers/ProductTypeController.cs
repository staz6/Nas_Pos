using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Entities;
using API.Helper;
using API.Specification;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Nas_Pos.Dto;
using Nas_Pos.Dto.ProductTypeDtos;
using Nas_Pos.Helper;
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
        private readonly IGenericRepository<Shop> _shopRepo;
        public ProductTypeController(IGenericRepository<ProductType> repo, IMapper mapper, IGenericRepository<Shop> shopRepo)
        {
            _shopRepo = shopRepo;
            _mapper = mapper;
            _repo = repo;
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
        [HttpGet("productType/{shopId}")]
        public async Task<ActionResult<GetProductTypeOnlyDto>> GetProductTypeList(int shopId)
        {
            string ownerId = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var spec = new GetProductTypeWithShopId(ownerId,shopId);
            var obj =await  _repo.ListAsyncWithSpec(spec);
            var mapObj = _mapper.Map<IReadOnlyList<GetProductTypeOnlyDto>>(obj);
            return Ok(mapObj);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
        [HttpPost("productType")]
        public async Task<ActionResult> PostProductType(PostProductTypeDto model)
        {
            if (!ModelState.IsValid) return BadRequest();
            var mapObj = _mapper.Map<ProductType>(model);
            var shop = await _shopRepo.GetById(model.ShopId);
            if(shop == null) return NotFound(new ApiErrorResponse(ErrorStatusCode.NotFound));
            _repo.Insert(mapObj);
            await _repo.Save();
            return Ok((new ApiErrorResponse(ErrorStatusCode.CreateSuccess)));
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
        [HttpDelete("productType/{id}")]
        public async Task<ActionResult> DeleteProductType(int id)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                _repo.Delete(id);
                await _repo.Save();
                return Ok((new ApiErrorResponse(ErrorStatusCode.DeleteSuccess)));
            }
            catch
            {
                return BadRequest((new ApiErrorResponse(ErrorStatusCode.InvalidRequest)));
            }

        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
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

        [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Employee)]
        [HttpGet("productTypeForEmployee")]
        public async Task<ActionResult<GetProductTypeOnlyDto>> getProductTypeOnly()
        {
            int shopId = int.Parse(HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value);
            var spec = new GetProductTypeWithShopId(shopId);
            var obj = await _repo.ListAsyncWithSpec(spec);
            var mapObj = _mapper.Map<IReadOnlyList<GetProductTypeOnlyDto>>(obj);
            return Ok(mapObj);
        }


    }
}