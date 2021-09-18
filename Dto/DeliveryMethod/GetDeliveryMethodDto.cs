namespace API.Dto.DeliveryMethod
{
    public class GetDeliveryMethodDto
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public int price { get; set; }
    }
}