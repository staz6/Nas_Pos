using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Config
{
    public class EmployeeBasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            
            builder.HasMany(o => o.BasketItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}