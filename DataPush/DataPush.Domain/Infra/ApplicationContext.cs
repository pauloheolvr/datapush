using DataPush.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataPush.Infra.Sql
{
    public class ApplicationContext : DbContext
    {
        public DbSet<ListedCompany> ListedCompanies { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString = "Data Source=localhost;Initial Catalog=Estudos;User Id=sa;Password=@Elifreitas0;";
            optionsBuilder
                .UseSqlServer(connectionString, x => x.CommandTimeout(20)
                .EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null));
        }

        protected override void OnModelCreating(ModelBuilder builder)
            => builder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }
}