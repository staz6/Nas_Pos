using System.Collections.Generic;

namespace Nas_Pos.Dto
{
    public class GetProductTypeDto
    {
        public string Title  { get; set; }
        public IReadOnlyList<GetProductDto> Products{get;set;}
    }
}