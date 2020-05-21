using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LearningCore.Common.Extentions
{
    /// <summary>
    /// 扩展 JsonSerializer.Deserialize 返回dynamic类型对象
    /// </summary>
    public class DynamicJsonConverter : JsonConverter<dynamic>
    {
        public override dynamic Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.True:return true;
                case JsonTokenType.False: return false;
                case JsonTokenType.Number: {
                        if (reader.TryGetInt64(out long l))
                        {
                            return l;
                        }

                        return reader.GetDouble();
                    };
                case JsonTokenType.String:
                    {
                        if (reader.TryGetDateTime(out DateTime datetime))
                        {
                            return datetime;
                        }

                        return reader.GetString();
                    };
                case JsonTokenType.StartObject:
                    {
                        using JsonDocument documentV = JsonDocument.ParseValue(ref reader);
                        return ReadObject(documentV.RootElement);
                    };
                default:break;
            }
            // Use JsonElement as fallback.
            // Newtonsoft uses JArray or JObject.
            JsonDocument document = JsonDocument.ParseValue(ref reader);
            return document.RootElement.Clone();
        }
        private object ReadObject(JsonElement jsonElement)
        {
            IDictionary<string, object> expandoObject = new ExpandoObject();
            foreach (var obj in jsonElement.EnumerateObject())
            {
                var k = obj.Name;
                var value = ReadValue(obj.Value);
                expandoObject[k] = value;
            }
            return expandoObject;
        }
        private object? ReadValue(JsonElement jsonElement)
        {
            object? result = null;
            switch (jsonElement.ValueKind)
            {
                case JsonValueKind.Object:
                    result = ReadObject(jsonElement);
                    break;
                case JsonValueKind.Array:
                    result = ReadList(jsonElement);
                    break;
                case JsonValueKind.String:
                    //TODO: Missing Datetime&Bytes Convert
                    result = jsonElement.GetString();
                    break;
                case JsonValueKind.Number:
                    //TODO: more num type
                    result = 0;
                    if (jsonElement.TryGetInt64(out long l))
                    {
                        result = l;
                    }
                    break;
                case JsonValueKind.True:
                    result = true;
                    break;
                case JsonValueKind.False:
                    result = false;
                    break;
                case JsonValueKind.Undefined:
                case JsonValueKind.Null:
                    result = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return result;
        }

        private object? ReadList(JsonElement jsonElement)
        {
            IList<object?> list = new List<object?>();
            foreach (var item in jsonElement.EnumerateArray())
            {
                list.Add(ReadValue(item));
            }
            return list.Count == 0 ? null : list;
        }
        public override void Write(Utf8JsonWriter writer, dynamic value, JsonSerializerOptions options)
        {
           // throw new NotImplementedException();
        }
    }
}
