using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataPush.Domain.Entities
{
    public class ListedCompany
    {
        public Guid Id  { get; set; }
        public Page page { get; set; }
        [JsonPropertyName("results")]
        public List<Company> Companies { get; set; }
    }

    public class Page
    {
        [Key]
        public Guid Id { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalRecords { get; set; }
        public int totalPages { get; set; }
    }
}