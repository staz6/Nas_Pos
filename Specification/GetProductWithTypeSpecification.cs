using API.Entities;
using Nas_Pos.Specification;

namespace API.Specification
{
    public class GetProductWithTypeSpecification : BaseSpecification<Product>
    {
        public GetProductWithTypeSpecification(int id) : base(x => x.ProductTypeId==id)
        {
            AddInclude(x => x.ProductShelves);
            AddInclude(x => x.ProductType);
        }
    }
}