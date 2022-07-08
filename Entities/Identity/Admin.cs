namespace API.Entities.Identity
{
    public class Admin
    {
        public int Id { get; set; }
        public AppUser AppUser { get; set; }
        public string DisplayName { get; set; }
    }
}