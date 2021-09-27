using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace DataPush.Domain.Entities
{
    public class DownloadAndProcess
    {
        private const string _url = "https://sistemaswebb3-listados.b3.com.br/listedCompaniesProxy/CompanyCall";
        private readonly ILogger<DownloadAndProcess> _logger;
        private ListedCompany _listedCompany;

        public DownloadAndProcess(ILogger<DownloadAndProcess> logger)
        {
            _logger = logger;
        }

        public void Run()
        {
            FetchDataFromB3();
            SaveListedCompanies();
        }

        private void FetchDataFromB3()
        {
            FetchListedCompanies();
            if (_listedCompany != null && _listedCompany.Companies != null && _listedCompany.Companies.Any())
            {
                _logger.LogInformation($"Obtendo dados de {_listedCompany.Companies.Count} empresas");
                foreach (var company in _listedCompany.Companies)
                {
                    var index = _listedCompany.Companies.IndexOf(company);
                    _logger.LogInformation($"Obtendo dados da empresa ({company.companyName}). Índice: {index}");
                    try
                    {
                        FetchCompanyStock(company);
                        _logger.LogInformation($"Empresa ({company.companyName}) obtida com sucesso. Índice: {index}");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Erro ao obter empresa ({company.cnpj}). Índice: {index}.", ex);
                    }
                }
            }
        }

        private void SaveListedCompanies()
        {

        }

        private void FetchListedCompanies()
        {
            var parameters = new
            {
                language = "pt-br",
                pageNumber = 1,
                pageSize = 99999
            };
            _listedCompany = GetData<ListedCompany>("GetInitialCompanies", parameters);
        }

        private void FetchCompanyStock(Company company)
        {
            var parameters = new
            {
                company.issuingCompany,
                language = "pt-br"
            };

            var companyStock = GetData<IEnumerable<CompanyStock>>("GetListedSupplementCompany", parameters)?.FirstOrDefault();
            if (companyStock is null) return;

            company.CompanyData = companyStock;
        }

        private T GetData<T>(string functionName, object json) where T : class
        {
            var attempt = 1; var maxAttempts = 5;

            while (attempt <= maxAttempts)
            {
                var encodedJson = JsonToEncodedString(json);
                using var webClient = new WebClient();
                var result = webClient.DownloadString($"{_url}/{functionName}/{encodedJson}");
                if (!"".Equals(result))
                    return JsonSerializer.Deserialize<T>(result);

                _logger.LogWarning($"Falha ao obter a função: {functionName}. Tentativa: {attempt++}");
            }
            return null;
        }

        private string JsonToEncodedString(object json) =>
            Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(json)));
    }
}
