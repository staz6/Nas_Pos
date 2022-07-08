using System.Security.Cryptography.X509Certificates;
using API.Entities.OrderAggregate;
using Nas_Pos.Specification;

namespace API.Specification
{
    public class GetDeliveryMethodWithShopSpecification : BaseSpecification<DeliveryMethod>
    {
        public GetDeliveryMethodWithShopSpecification(int shopId) : base( x => x.ShopId==shopId)
        {
        }

        public GetDeliveryMethodWithShopSpecification(int shopId,string ownerId) : base( x => x.ShopId==shopId && x.Shop.OwnerId == ownerId )
        {
            AddInclude( x => x.Shop);
        }
    }
}