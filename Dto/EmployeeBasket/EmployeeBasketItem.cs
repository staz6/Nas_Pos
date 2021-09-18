namespace API.Dto.EmployeeBasket
{
    public class EmployeeBasketItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public string PictureUrl { get; set; }
    }
}