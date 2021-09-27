using DataPush.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataPush.Infra.Sql.ClassMap
{
    public class CompanyStockMap : IEntityTypeConfiguration<CompanyStock>
    {
        public void Configure(EntityTypeBuilder<CompanyStock> builder)
        {
            builder.ToTable("CompanyStock");
            builder.HasNoKey();
            builder.HasMany(x => x.EarningsOnMoney);
        }
    }
}