﻿using Krosoft.Extensions.Core.Models.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace Krosoft.Extensions.Core.Converters;

public class KrosoftMetierExceptionConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => objectType == typeof(KrosoftMetierException);

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.StartObject)
        {
            var properties = new Dictionary<string, object>();
            serializer.Populate(reader, properties);
            var array = properties.GetValueOrDefault(nameof(KrosoftMetierException.Erreurs)) as JArray;
            var erreurs = new HashSet<string>();
            if (array != null)
            {
                var o = array.ToObject<List<string>>();
                if (o == null)
                {
                    throw new InvalidOperationException();
                }

                erreurs = o.ToHashSet();
            }

            return new KrosoftMetierException(erreurs);
        }

        return null;
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}