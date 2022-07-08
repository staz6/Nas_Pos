using System;
using System.Linq.Expressions;
using API.Entities;
using Nas_Pos.Specification;

namespace API.Specification
{
    public class GetProductShelvesWithShopId : BaseSpecification<ProductShelves>
    {
        public GetProductShelvesWithShopId(int id) : base(x => x.ShopId==id)
        {
            AddInclude(x => x.Shop);
        }

        public GetProductShelvesWithShopId(string ownerId,int id) : base(x => x.ShopId==id && x.Shop.OwnerId==ownerId)
        {
            AddInclude(x => x.Shop);
        }
        
    }
}