using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dto;
using API.Entities;
using API.Entities.OrderAggregate;
using API.Helper;
using API.Interface;
using AutoMapper;

namespace API.Data
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task AddOrder(BasketDto model)
        {
            try
            {
                var paymentMethod = await _unitOfWork.Repository<PaymentMethod>().GetById(model.PaymentMethodId);
                var customer = await _unitOfWork.Repository<Customer>().GetById(model.CustomerId);
                if (customer == null) throw new Exception(ErrorStatusCode.CustomerNotFound);
                var items = new List<OrderItem>();
                foreach (var item in model.BasketProducts)
                {
                    var productItem = await _unitOfWork.Repository<Product>().GetById(item.ProductId);
                    var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Title, productItem.PictureUrl);
                    var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                    var stock  = productItem.Stock-item.Quantity;
                    if(stock <0){
                        throw new Exception("Not of stock of product type : "+item.ProductName);
                    }
                    productItem.Stock=stock;
                    items.Add(orderItem);
                }
                var subTotal = items.Sum(item => item.Price * item.Quantity);
                if (model.DeliveryMethodId != 0)
                {
                    var address = _mapper.Map<Address>(model.Address);
                    var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetById(model.DeliveryMethodId);
                    var order = new Order(customer, DateTime.Now, address, deliveryMethod, paymentMethod, items, subTotal, model.OrderStatus);
                     _unitOfWork.Repository<Order>().Insert(order);
                }
                else{
                     var order = new Order(customer, DateTime.Now, paymentMethod, items, subtotal: subTotal, model.OrderStatus);
                      _unitOfWork.Repository<Order>().Insert(order);
                }
                var result = await _unitOfWork.Complete();
               if(result <=0) throw new Exception("Something went wrong, please try again later");  
               

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}