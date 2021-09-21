using API.Entities.Ledger;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Config
{
    public class LedgerConfigurations : IEntityTypeConfiguration<Ledger>
    {
        public void Configure(EntityTypeBuilder<Ledger> builder)
        {
            builder.HasMany(o => o.Transactions).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}