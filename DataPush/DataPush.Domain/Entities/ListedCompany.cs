using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataPush.Domain.Entities
{
    public class ListedCompany
    {
        public Page page { get; set; }
        [JsonPropertyName("results")]
        public List<Company> Companies { get; set; }
    }

    public class Page
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalRecords { get; set; }
        public int totalPages { get; set; }
    }
}