using System;
using System.Collections.Generic;
using Nas_Pos.Entities.Identity;

namespace API.Entities.OrderAggregate
{
    #nullable enable
    public class Order : BaseClass
    {
        public Order()
        {
        }

        public Order(Customer customer, DateTime orderDate,
        PaymentMethod paymentMethod ,IReadOnlyList<OrderItem> orderItems, decimal subtotal, OrderStatus status)
        {
            Customer = customer;
            PaymentMethod=paymentMethod;
            OrderDate = orderDate;          
            OrderItems = orderItems;
            Subtotal = subtotal;
            Status = status;
        }

        public Order(Customer customer, DateTime orderDate, Address shipToAddress, 
        DeliveryMethod deliveryMethod, PaymentMethod paymentMethod ,IReadOnlyList<OrderItem> orderItems, decimal subtotal, OrderStatus status)
        {
            Customer = customer;
            PaymentMethod=paymentMethod;
            OrderDate = orderDate;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;
            Status = status;
        }

        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public Address? ShipToAddress { get; set; }
        
        public DeliveryMethod? DeliveryMethod { get; set; }
        public PaymentMethod PaymentMethod{get;set;}
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public OrderStatus Status  { get; set; }

        public decimal GetTotal(){
            if(DeliveryMethod != null)
            {
                return Subtotal + DeliveryMethod.price;
            }
            return Subtotal;
            
        }

    }
}