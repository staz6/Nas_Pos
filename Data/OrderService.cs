using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dto;
using API.Entities;
using API.Entities.OrderAggregate;
using API.Helper;
using API.Interface;

namespace API.Data
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddOrder(BasketDto model)
        {
            try
            {
                var customer = await _unitOfWork.Repository<Customer>().GetById(model.CustomerId);
                if(customer == null) throw new Exception(ErrorStatusCode.CustomerNotFound);
                var items = new List<OrderItem>();
                foreach (var item in model.BasketProducts)
                {
                    var productItem = await _unitOfWork.Repository<Product>().GetById(item.ProductId);
                    var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Title, productItem.PictureUrl);
                    var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                    items.Add(orderItem);
                }

            }
            catch (Exception)
            {
                throw new Exception(ErrorStatusCode.CustomerNotFound);
            }
        }
    }
}