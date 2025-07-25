﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace RastreamentoPedido.Core.Converters
{
    public class TimeOnlyConverter : JsonConverter<TimeOnly>
    {
        private readonly string _serializationFormat;
        public TimeOnlyConverter() : this(null) { }

        public TimeOnlyConverter(string? serializationFormat)
        {
            _serializationFormat = serializationFormat ?? "HH:mm:ss";
        }

        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (value == null)
            {
                return TimeOnly.Parse("00:00:00");
            }
            return TimeOnly.Parse(value);
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_serializationFormat));
        }
    }
}
