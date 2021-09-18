using Nas_Pos.Entities.Identity;

namespace API.Entities.OrderAggregate
{
    public class PaymentMethod : BaseClass
    {
        public string Type { get; set; }
        public string Description { get; set; }
    }
}