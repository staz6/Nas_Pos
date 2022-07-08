using System.Linq;
using API.Dto;
using API.Entities;
using API.Entities.Identity;
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
using API.Dto.Order;
using API.Entities.Ledger;
using API.Dto.Ledger;
using API.Dto.Shop;

namespace Nas_Pos.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterEmployeeDto,AppUser>();
            CreateMap<RegisterCustomerDto,AppUser>();
            CreateMap<RegisterAdminDto,AppUser>();
            CreateMap<RegisterEmployeeDto,Employee>();
            CreateMap<RegisterAdminDto,Admin>();
            CreateMap<RegisterCustomerDto,CustomerIdentity>();

            CreateMap<ProductType,PutProductTypeDto>().ReverseMap();
            CreateMap<ProductType,GetProductTypeDto>();
            CreateMap<PostProductTypeDto,ProductType>();
            CreateMap<ProductType,GetProductTypeOnlyDto>();



            CreateMap<Product,GetProductDto>().ForMember( x => x.Shelve , o => o.MapFrom(s => s.ProductShelves.Title))
                                            .ForMember(x => x.ProductTypeName, o => o.MapFrom(s => s.ProductType.Title))
                                            .ForMember(x => x.ProductTypeId, o => o.MapFrom(s => s.ProductType.Id))
                                            .ForMember(x => x.PictureUrl, o => o.MapFrom(m => "http://api.pos.nastechltd.co/"+m.PictureUrl));
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
        //    CreateMap<Customer,GetCustomerDto>();
        //    CreateMap<PostCustomerDto,Customer>();
        //    CreateMap<PutCustomerDto,Customer>();

        //    Employee BASKET

            CreateMap<Basket,EmployeeBasket>().ForMember(x => x.Total, o => o.MapFrom(s => s.BasketItems.Select(c => c.Price).Sum()));
            CreateMap<BasketItem,EmployeeBasketItem>();

        // CreateShop
            CreateMap<PostShopDto,Shop>();
            CreateMap<Shop,GetShopDto>();
            CreateMap<Shop,GetShopNameAndId>();
            




            ///ORDER MAP
            CreateMap<PostAddressDto,Address>();
            CreateMap<PostPaymentMethod,PaymentMethod>();

            CreateMap<Order,GetOrderDto>().ForMember(x => x.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                                    .ForMember( x=> x.DecimalMethodPrice, o => o.MapFrom(s => s.DeliveryMethod.price))
                                    .ForMember( x => x.PaymentMethod, o => o.MapFrom(s => s.PaymentMethod.Type));
            CreateMap<OrderItem,GetOrderItemDto>();
            // CreateMap<Customer,GetCustomerIdNameDto>().ForMember(x => x.FullName, o => o.MapFrom(s => s.FirstName+" "+s.LastName));
            CreateMap<Address,GetAddressDto>();


            ///Ledger
            CreateMap<Ledger,GetLedgerDto>().ForMember(x => x.OrderId, o => o.MapFrom(s => s.Order.Id));
                                       
            CreateMap<Transaction,GetTransactionDto>();
            CreateMap<PostTransactionDto,Transaction>();
            
        }
    }
}