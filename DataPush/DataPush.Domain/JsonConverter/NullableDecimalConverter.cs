using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DataPush.Domain.JsonConverter
{
    public class NullableDecimalConverter : JsonConverter<decimal?>
    {
        public override decimal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            decimal.TryParse(reader.GetString(), NumberStyles.Any, CultureInfo.GetCultureInfo("pt-BR"), out decimal value) switch
            {
                true => value,
                false => null
            };

        public override void Write(Utf8JsonWriter writer, decimal? value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString());
    }
}