using System.Collections.Generic;

namespace API.Dto.EmployeeBasket
{
    public class EmployeeBasket
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public int Total { get; set; }
        public List<EmployeeBasketItem> BasketItems { get; set; }
    }
}