using DataPush.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataPush.Domain.Infra.ClassMap
{
    public class ListedCompaniesMap : IEntityTypeConfiguration<ListedCompany>
    {
        public void Configure(EntityTypeBuilder<ListedCompany> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}