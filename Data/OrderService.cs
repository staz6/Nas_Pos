using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dto;
using API.Dto.Ledger;
using API.Entities;
using API.Entities.Ledger;
using API.Entities.OrderAggregate;
using API.Helper;
using API.Interface;
using API.Specification;
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
                    
                    var transaction = new List<Transaction>();
                var transactionItem = new Transaction(model.AmountPaid,paymentMethod.Type,paymentMethod.Description,true);
                transaction.Add(transactionItem);
                
                 var ledge = new Ledger(order,subTotal+deliveryMethod.price, model.AmountPaid ,subTotal-model.AmountPaid,model.AmountPaid < subTotal? true:false, transaction );
                _unitOfWork.Repository<Ledger>().Insert(ledge);
                }
                else{
                     var order = new Order(customer, DateTime.Now, paymentMethod, items, subtotal: subTotal, model.OrderStatus);
                      _unitOfWork.Repository<Order>().Insert(order);
                    
                var transaction = new List<Transaction>();
                var transactionItem = new Transaction(model.AmountPaid,paymentMethod.Type,paymentMethod.Description,true);
                transaction.Add(transactionItem);
                
                 var ledge = new Ledger(order,subTotal, model.AmountPaid ,subTotal-model.AmountPaid,model.AmountPaid < subTotal? true:false, transaction );
                _unitOfWork.Repository<Ledger>().Insert(ledge);
                }
                
                var result = await _unitOfWork.Complete();
               if(result <=0) throw new Exception("Something went wrong, please try again later");  
               

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<IReadOnlyList<Order>> GetAllOrder()
        {
            var spec = new OrdersWIthItemAndOrderingSpecifcation();
            return await _unitOfWork.Repository<Order>().ListAsyncWithSpec(spec);
        }
        public async Task<IReadOnlyList<Order>> GetAllOrderById(int id)
        {
            var spec = new OrdersWIthItemAndOrderingSpecifcation(id);
            return await _unitOfWork.Repository<Order>().ListAsyncWithSpec(spec);
        }

        public async Task<Order> GetOrderById(int id)
        {
            var spec = new OrdersWIthItemAndOrderingSpecifcation(id,"A");
            return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrderBySort(int id)
        {
            var spec = new OrdersWIthItemAndOrderingSpecifcation(id,"A","A");
            return await _unitOfWork.Repository<Order>().ListAsyncWithSpec(spec);
        }

        public async Task AddTransaction(int ledgerId, Transaction dto)
        {
            try{
                var spec = new LedgerWithOrderAndTransaction(ledgerId);
            var ledger = await _unitOfWork.Repository<Ledger>().GetEntityWithSpec(spec);

            ledger.Transactions.Append(dto);
            var result = await _unitOfWork.Complete();
            if(result <= 0) throw new Exception();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            
        }

       
    }
}