using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

public class ColorConverter : JsonConverter<Color>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        Color c = default;
        JObject obj = JObject.Load(reader);
        c.r = (float)obj["R"];
        c.g = (float)obj["G"];
        c.b = (float)obj["B"];
        c.a = (float)obj["A"];
        return c;
    }

    public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
    {
         writer.WriteStartObject(); 
         writer.WritePropertyName("R");
        writer.WriteValue(value.r);
        writer.WritePropertyName("G");
        writer.WriteValue(value.g);
        writer.WritePropertyName("B");
        writer.WriteValue(value.b);
        writer.WritePropertyName("A");
        writer.WriteValue(value.a);
        writer.WriteEndObject();
    }
}
