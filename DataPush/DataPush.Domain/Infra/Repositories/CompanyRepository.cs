using DataPush.Domain.Entities;
using DataPush.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataPush.Infra.Sql.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationContext _context;

        public CompanyRepository(ApplicationContext context) 
            => _context = context;

        public void Save(IEnumerable<Company> companies)
        {
            _context.Companies.AddRange(companies);
            _context.SaveChanges();
            Console.WriteLine($"Inserido ({companies.Count()}) Dados");
        }
    }
}