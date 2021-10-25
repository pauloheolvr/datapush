using DataPush.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace DataPush.Infra.Sql
{
    public class ApplicationContext : DbContext
    {
        const string connectionString = "Server=DESKTOP-AGUEKFL;Database=Estudos;Trusted_Connection=True;";

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<ListedCompany> ListedCompanies { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
            => builder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            builder.UseSqlServer(connectionString);
        }
    }
}