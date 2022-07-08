using System;
using System.Linq.Expressions;
using API.Entities;
using Nas_Pos.Specification;

namespace API.Specification
{
    public class GetShopWithOwnerIdSpecification : BaseSpecification<Shop>
    {
        public GetShopWithOwnerIdSpecification(string ownerId) : base(x => x.OwnerId == ownerId)
        {
        }
    }
}