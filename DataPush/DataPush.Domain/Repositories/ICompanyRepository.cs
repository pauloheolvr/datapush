using DataPush.Domain.Entities;
using System.Collections.Generic;

namespace DataPush.Domain.Repositories
{
    public interface ICompanyRepository
    {
        void DeleteAndCreateDatabase();
        void Save(IEnumerable<Company> companies);
        IEnumerable<Company> GetCompanies(string code);
        IEnumerable<Company> GetCompanies();
        Company GetCompany(string code);

    }
}