using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DataPush.Domain.JsonConverter
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        private const string FORMAT = "dd/MM/yyyy";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            DateTime.ParseExact(reader.GetString(), FORMAT, CultureInfo.InvariantCulture);

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions? options) =>
            writer.WriteStringValue(value.ToString());
    }
}