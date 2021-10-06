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

        public IEnumerable<Company> GetCompanies() 
            => _context.Companies.ToList();

        public IEnumerable<Company> GetCompanies(string code) 
            => _context
            .Companies.Where(x => x.companyName.Contains(code))
            .ToList();


        public Company GetCompany(string code)
            => _context
            .Companies.Where(x => code.Equals(x.codeCVM) 
                || code.Equals(x.cnpj))
            .FirstOrDefault();

        public void Save(IEnumerable<Company> companies)
        {
            _context.Companies.AddRange(companies);
            _context.SaveChanges();
            Console.WriteLine($"Inserido ({companies.Count()}) Dados");
        }

        public void DeleteAndCreateDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
    }
}