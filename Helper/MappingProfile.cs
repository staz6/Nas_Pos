using API.Dto;
using API.Entities;
using API.Entities.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Nas_Pos.Dto;
using Nas_Pos.Dto.ProductDtos;
using Nas_Pos.Dto.ProductTypeDtos;

namespace Nas_Pos.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterEmployeeDto,Employee>();


            CreateMap<ProductType,PutProductTypeDto>().ReverseMap();
            CreateMap<ProductType,GetProductTypeDto>();
            CreateMap<PostProductTypeDto,ProductType>();
            CreateMap<ProductType,GetProductTypeOnlyDto>();



            CreateMap<Product,GetProductDto>();
            CreateMap<PostProductDto,Product>();
            CreateMap<PutProductDto,Product>().ReverseMap();    
            
        }
    }
}