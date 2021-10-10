using DataPush.Domain.Repositories;
using DataPush.Infra.Sql;
using DataPush.Infra.Sql.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace DataPush.Domain.Entities
{
    public class DownloadAndProcess
    {
        private ListedCompany _listedCompany;
        private readonly ApplicationContext _context;
        private readonly ILogger<DownloadAndProcess> _logger;
        private readonly ICompanyRepository _companyRepository;
        private const string _url = "https://sistemaswebb3-listados.b3.com.br/listedCompaniesProxy/CompanyCall";

        public DownloadAndProcess(ICompanyRepository companyRepository)
        {
            var options = new DbContextOptions<ApplicationContext>();
            _context = new ApplicationContext(options);
            _companyRepository = companyRepository;
            _logger = GetLogger<DownloadAndProcess>();
        }

        public void Run()
        {
            var companies = FetchDataFromB3();
            SaveListedCompanies(companies);
        }

        private IEnumerable<Company> FetchDataFromB3()
        {
            FetchListedCompanies();
            var stopwatch = new Stopwatch();
            if (_listedCompany != null && _listedCompany.Companies != null && _listedCompany.Companies.Any())
            {
                stopwatch.Start();
                _logger.LogInformation($"Obtendo dados de {_listedCompany.Companies.Count} empresas");
                foreach (var company in _listedCompany.Companies)
                {
                    var index = _listedCompany.Companies.IndexOf(company);
                    _logger.LogInformation($"Obtendo dados da empresa ({company.companyName}). Índice: {index}");
                    try
                    {
                        _logger.LogInformation($"Empresa ({company.companyName}) obtida com sucesso. Índice: {index}");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Erro ao obter empresa ({company.cnpj}). Índice: {index}.", ex);
                    }
                }
                stopwatch.Stop();
            }
            _logger.LogInformation($"Processo finalizado em {stopwatch.Elapsed.TotalSeconds} segundos");
            return _listedCompany.Companies;
        }

        private void SaveListedCompanies(IEnumerable<Company> companies)
            => _companyRepository.Save(companies);

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

        private static string JsonToEncodedString(object json) =>
            Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(json)));

        private static ILogger<TClass> GetLogger<TClass>()
        {
            var serilogLogger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            var loggerFactory = new LoggerFactory()
                .AddSerilog(serilogLogger)
                .CreateLogger<TClass>();
            return loggerFactory;
        }
    }
}