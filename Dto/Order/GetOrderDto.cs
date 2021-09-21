using System.Collections.Generic;
using API.Dto.Customer;

namespace API.Dto.Order
{
    public class GetOrderDto
    {
        public int Id { get; set; }
        public GetCustomerIdNameDto Customer { get; set; }
        public string OrderDate { get; set; } 
        public GetAddressDto ShipToAddress { get; set; }
        
        public string DeliveryMethod { get; set; }
        public int  DecimalMethodPrice { get; set; }
        public string  PaymentMethod{get;set;}
        public IReadOnlyList<GetOrderItemDto> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public string Status  { get; set; }
        public decimal Total{get;set;}
    }
}