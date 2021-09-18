using System.Collections.Generic;
using Nas_Pos.Entities.Identity;

namespace API.Entities
{
    public class Basket : BaseClass
    { 
        
        public string EmployeeId { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}