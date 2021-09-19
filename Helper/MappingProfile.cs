using System.Linq;
using API.Dto;
using API.Entities;
using API.Entities.Identity;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Nas_Pos.Dto;
using Nas_Pos.Dto.ProductDtos;
using Nas_Pos.Dto.ProductTypeDtos;
using API.Dto.ProductShelves;
using API.Entities.OrderAggregate;
using API.Dto.DeliveryMethod;
using API.Dto.Basket;
using API.Dto.EmployeeBasket;
using API.Dto.Customer;
using Nas_Pos.Dto.PaymentMethod;

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



            CreateMap<Product,GetProductDto>().ForMember( x => x.Shelve , o => o.MapFrom(s => s.ProductShelves.Title))
                                            .ForMember(x => x.ProductTypeName, o => o.MapFrom(s => s.ProductType.Title))
                                            .ForMember(x => x.ProductTypeId, o => o.MapFrom(s => s.ProductType.Id));
            CreateMap<PostProductDto,Product>();
            CreateMap<PutProductDto,Product>().ReverseMap();    

            
           CreateMap<ProductShelves,GetProductShelvesDto>();
           CreateMap<PostProductShelvesDto,ProductShelves>();
           CreateMap<PutProductShelvesDto,ProductShelves>().ReverseMap();

           CreateMap<DeliveryMethod,GetDeliveryMethodDto>();
           CreateMap<PostDeliveryMethodDto,DeliveryMethod>();

           CreateMap<PaymentMethod,GetPaymentMethodDto>();
           CreateMap<PostPaymentMethodDto,PaymentMethod>();


           ///Customer
           CreateMap<Customer,GetCustomerDto>();
           CreateMap<PostCustomerDto,Customer>();
           CreateMap<PutCustomerDto,Customer>();

        //    Employee BASKET

            CreateMap<Basket,EmployeeBasket>().ForMember(x => x.Total, o => o.MapFrom(s => s.BasketItems.Select(c => c.Price).Sum()));
            CreateMap<BasketItem,EmployeeBasketItem>();




            ///ORDER MAP
            CreateMap<PostAddressDto,Address>();
            CreateMap<PostPaymentMethod,PaymentMethod>();
            
        }
    }
}