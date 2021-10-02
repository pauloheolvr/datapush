using DataPush.Domain.JsonConverter;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataPush.Domain.Entities
{
    public class CompanyStock
    {
        [JsonPropertyName("cashDividends")]
        public List<EarningsOnMoney> EarningsOnMoney { get; set; }

    }

    public class EarningsOnMoney
    {
        public string assetIssued { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? paymentDate { get; set; }
        [JsonConverter(typeof(NullableDecimalConverter))]
        public decimal? rate { get; set; }
        public string relatedTo { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? approvedOn { get; set; }
        public string isinCode { get; set; }
        public string label { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? lastDatePrior { get; set; }
        public string? remarks { get; set; }
    }
}