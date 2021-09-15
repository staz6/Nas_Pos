using API.Entities;

namespace Nas_Pos.Specification
{
    public class GetProductTypeWithProducts : BaseSpecification<ProductType>
    {
        public GetProductTypeWithProducts() 
        {
            AddInclude(x => x.Products);
        }
        public GetProductTypeWithProducts(int id) : base(x => x.Id==id)
        {
            AddInclude(x => x.Products);
        }
    } 
}