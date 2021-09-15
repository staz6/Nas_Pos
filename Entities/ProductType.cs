using System.Collections.Generic;
using Nas_Pos.Entities.Identity;

namespace API.Entities
{
    public class ProductType : BaseClass
    {
        public string Title { get; set; }
        public IReadOnlyList<Product> Products{get;set;}
    }
}