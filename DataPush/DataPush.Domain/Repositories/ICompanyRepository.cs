using DataPush.Domain.Entities;
using System.Collections.Generic;

namespace DataPush.Domain.Repositories
{
    public interface ICompanyRepository
    {
        void Save(IEnumerable<Company> companies);
    }
}