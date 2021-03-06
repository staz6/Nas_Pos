using System;
using System.Linq.Expressions;
using API.Entities.OrderAggregate;
using Nas_Pos.Specification;

namespace API.Specification
{
    public class OrdersWIthItemAndOrderingSpecifcation : BaseSpecification<Order>
    {
        public OrdersWIthItemAndOrderingSpecifcation()
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(x => x.OrderItems);
            AddInclude(x => x.Customer);
        }
        public OrdersWIthItemAndOrderingSpecifcation(int id,string x): base(x => x.Id==id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(x => x.OrderItems);
            AddInclude(x => x.Customer);
        }
        

        public OrdersWIthItemAndOrderingSpecifcation(int id) : base(x => x.Customer.Id==id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(x => x.OrderItems);
            AddInclude(x => x.Customer);
        }

        public OrdersWIthItemAndOrderingSpecifcation(int id,string x, string y) : base(x => ((int)x.Status)==id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(x => x.OrderItems);
            AddInclude(x => x.Customer);
        }
    }
}