using API.Entities;
using Nas_Pos.Specification;

namespace API.Specification
{
    public class GetProductWithShelvesSpecification : BaseSpecification<Product>
    {
        public GetProductWithShelvesSpecification()
        {
            AddInclude(x => x.ProductShelves);
            AddInclude(x => x.ProductType);
        }
        public GetProductWithShelvesSpecification(int id) : base(x => x.Id==id)
        {
            AddInclude(x => x.ProductShelves);
            AddInclude(x => x.ProductType);
        }
        
    }
}