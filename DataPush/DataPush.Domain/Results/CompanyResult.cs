namespace DataPush.Domain.Results
{
    public record CompanyResult(
        string CompanyName,
        string Issuer,
        string TradingName,
        string Segment,
        string CodeCvm);
}