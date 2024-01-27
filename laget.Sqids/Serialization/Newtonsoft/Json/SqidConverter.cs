using Newtonsoft.Json;
using System;

namespace laget.Sqids.Serialization.Newtonsoft.Json
{
    public class SqidConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is Sqid sqid)
            {
                serializer.Serialize(writer, sqid.Hash);
                return;
            }

            serializer.Serialize(writer, null);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String)
                throw new JsonException($"Sqids deserializer encountered token {reader.TokenType}, expected String");

            if (reader.Value is string sqid)
                return Sqid.FromString(sqid);

            throw new JsonException($"Failed to read string from json, found {reader.Value} with type {reader.Value.GetType()}");
        }

        public override bool CanConvert(Type objectType) => objectType == typeof(Sqid);
    }
}