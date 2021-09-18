using System;
using System.Runtime.Serialization;

namespace API.Entities.OrderAggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Payment Received")]
        PaymentReceived,
        [EnumMember(Value = "Credit")]
        Credit,
        [EnumMember(Value = "PaymentFailed")]
        PaymentFailed,
        [EnumMember(Value = "Shipped")]
        Shipped
    }
}