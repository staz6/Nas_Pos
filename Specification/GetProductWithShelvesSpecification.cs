using API.Entities;
using Nas_Pos.Specification;

namespace API.Specification
{
    public class GetProductWithShelvesSpecification : BaseSpecification<Product>
    {
        public GetProductWithShelvesSpecification(int shopId,ProductSpecParams productParams) 
            : base(x => (x.ProductType.ShopId == shopId) && (x.ProductShelves.ShopId==shopId) &&
                (string.IsNullOrEmpty(productParams.Search) || x.Title.ToLower().Contains
                (productParams.Search)) &&
                (!productParams.TypeID.HasValue || x.ProductTypeId==productParams.TypeID) &&
                (!productParams.ShelvesId.HasValue || x.ProductShelvesId == productParams.ShelvesId)
                )
        {
            AddInclude(x => x.ProductShelves);
            AddInclude(x => x.ProductType);
            AddOrderBy(x => x.Title);
            ApplyPaging(productParams.PageSize *(productParams.PageIndex -1), productParams.PageSize );
            if(!string.IsNullOrEmpty(productParams.Sort)){
                switch(productParams.Sort){
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Title);
                        break;
                }
            }
        }
      
        public GetProductWithShelvesSpecification(int id) : base(x => x.Id==id)
        {
            AddInclude(x => x.ProductShelves);
            AddInclude(x => x.ProductType);
        }
        
    }
}