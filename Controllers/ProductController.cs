using System;
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
        private readonly IGenericRepository<Shop> _shopRepo;
        public ProductController(IGenericRepository<Product> repo, IMapper mapper, IGenericRepository<Shop> shopRepo)
        {
            _shopRepo = shopRepo;
            _mapper = mapper;
            _repo = repo;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Employee)]
        [HttpGet("productForEmployee")]
        public async Task<ActionResult<GetProductDto>> GetProductList([FromQuery] ProductSpecParams productParams)
        {
            
            int shopId = int.Parse(HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value);
            var spec = new GetProductWithShelvesSpecification(shopId, productParams);
            var obj = await _repo.ListAsyncWithSpec(spec);
            var mapObj = _mapper.Map<IReadOnlyList<GetProductDto>>(obj);
            return Ok(mapObj);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
        [HttpGet("product/{shopId}")]
        public async Task<ActionResult<GetProductDto>> GetProductListAdmin(int shopId, [FromQuery] ProductSpecParams productParams)
        {
            var ownerId = HttpContext.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var shop = await _shopRepo.GetById(shopId);
            if(shop.OwnerId!=ownerId) return NotFound(new ApiErrorResponse(ErrorStatusCode.NotFound));
            var spec = new GetProductWithShelvesSpecification(shopId, productParams);
            var obj = await _repo.ListAsyncWithSpec(spec);
            var mapObj = _mapper.Map<IReadOnlyList<GetProductDto>>(obj);
            return Ok(mapObj);
        }



        [HttpGet("productDetail/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
        public async Task<ActionResult> GetProductById(int id)
        {
            var ownerId = HttpContext.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var spec = new GetProductWithShelvesSpecification(id);
            var obj = await _repo.GetEntityWithSpec(spec);      
            if (obj == null) return NotFound(new ApiErrorResponse(ErrorStatusCode.NotFound));
            var shop = await _shopRepo.GetById(obj.ProductShelves.ShopId);
            if(shop.OwnerId!=ownerId) return NotFound(new ApiErrorResponse(ErrorStatusCode.NotFound));
            var mapObj = _mapper.Map<GetProductDto>(obj);
            return Ok(mapObj);
        }
        [HttpGet("ProductDetailForEmployee/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Employee)]
        public async Task<ActionResult> GetProductByIdForEmployee(int id)
        {
            var shopId = int.Parse(HttpContext.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value);
            var spec = new GetProductWithShelvesSpecification(id);
            var obj = await _repo.GetEntityWithSpec(spec);      
            if (obj == null) return NotFound(new ApiErrorResponse(ErrorStatusCode.NotFound));
            if(obj.ProductType.ShopId!=shopId) return NotFound(new ApiErrorResponse(ErrorStatusCode.NotFound));
            var mapObj = _mapper.Map<GetProductDto>(obj);
            return Ok(mapObj);
        }
        
        [HttpPost("product")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
        public async Task<ActionResult> PostProduct(PostProductDto model)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var mapObj = _mapper.Map<Product>(model);
                _repo.Insert(mapObj);
                await _repo.Save();
                return Ok((new ApiErrorResponse(ErrorStatusCode.CreateSuccess)));
            }
            catch
            {
                return BadRequest((new ApiErrorResponse(ErrorStatusCode.InvalidRequest)));
            }


        }

        [HttpDelete("product/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
        public async Task<ActionResult> DeleteProduct(int id)
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

        [HttpPatch("product/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = " " + Roles.Admin + "   " + Roles.Employee + "")]
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