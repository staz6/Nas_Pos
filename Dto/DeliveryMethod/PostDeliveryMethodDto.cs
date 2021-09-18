namespace API.Dto.DeliveryMethod
{
    public class PostDeliveryMethodDto
    {
        public string ShortName { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public int price { get; set; }
    }
}