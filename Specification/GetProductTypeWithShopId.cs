using System.Security.Cryptography.X509Certificates;
using System;
using System.Linq.Expressions;
using API.Entities;
using Nas_Pos.Specification;

namespace API.Specification
{
    public class GetProductTypeWithShopId : BaseSpecification<ProductType>
    {
        public GetProductTypeWithShopId(int id) : base(x => x.ShopId==id)
        {
            AddInclude(x => x.Shop);
        }
        public GetProductTypeWithShopId(string ownerId,int id) : base(x => x.ShopId==id && x.Shop.OwnerId==ownerId)
        {
            AddInclude(x => x.Shop);
        }
    }
}