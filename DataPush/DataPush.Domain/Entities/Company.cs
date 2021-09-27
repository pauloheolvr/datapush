namespace DataPush.Domain.Entities
{
    public class Company
    {
        public string codeCVM { get; set; }
        public string issuingCompany { get; set; }
        public string companyName { get; set; }
        public string tradingName { get; set; }
        public string cnpj { get; set; }
        public string segment { get; set; }
        public CompanyStock CompanyData { get; set; }
    }
}