using DataPush.Domain.Entities;
using DataPush.Domain.Repositories;
using DataPush.Domain.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DataPush.API.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly DownloadAndProcess _downloadAndProcess;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
            _downloadAndProcess = new DownloadAndProcess(companyRepository);
        }

        /// <summary>
        /// Método que obtém as empresas e popula o banco
        /// </summary>
        /// <returns>Mensagem</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("v1/companies")]
        public IActionResult PostCompanies()
        {
            _companyRepository.DeleteAndCreateDatabase();
            _downloadAndProcess.Run();
            return Ok("Dados Salvos");
        }

        /// <summary>
        /// Método que obtém todas as empresas listadas do site B3
        /// B3 é a bolsa de valores oficial do Brasil
        /// </summary>
        /// <returns>Empresas</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("v1/companies")]
        public IActionResult GetCompanies() 
        {
            var companies = _companyRepository.GetCompanies();
            if (companies is null || !companies.Any()) return BadRequest("Empresas não encontradas");
            return Ok(ParseToResult(companies));
        }

        /// <summary>
        /// Método que obtém uma empresa pelo código/cpnj
        /// </summary>
        /// <param name="code">Código da empresa</param>
        /// <returns>Empresa</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("v1/companies/{code}")]
        public IActionResult GetCompany([FromRoute] string code) 
        {
            var company = _companyRepository.GetCompany(code);
            if (company is null) return BadRequest("Empresa não encontrada");
            return Ok(ParseToResult(company));
        }
        
        /// <summary>
        /// Método que obtém as empresas que contenham o nome passado
        /// </summary>
        /// <param name="name">Nome da empresa</param>
        /// <returns>Empresas</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("v1/companies/{name}/filter")]
        public IActionResult GetCompanyByFilter([FromRoute] string name) 
        {
            var company = _companyRepository.GetCompanies(name);
            if (company is null) return BadRequest("Empresa não encontrada");
            return Ok(ParseToResult(company));
        }

        private IEnumerable<CompanyResult> ParseToResult(IEnumerable<Company> companies) => companies.Select(ParseToResult);

        private CompanyResult ParseToResult(Company company)
        {
            if (company is null) return null;
            return new (
                company.companyName,
                company.cnpj,
                company.issuingCompany,
                company.tradingName,
                company.segment,
                company.codeCVM);
        }
    }
}