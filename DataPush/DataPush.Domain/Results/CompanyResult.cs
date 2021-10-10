namespace DataPush.Domain.Results
{
    public record CompanyResult(
        string CompanyName,
        string Cnpj,
        string Issuer,
        string TradingName,
        string Segment,
        string CodeCvm);
}