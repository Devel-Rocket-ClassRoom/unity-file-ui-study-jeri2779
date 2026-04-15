using UnityEngine;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;

public class QuaternionConverter : JsonConverter<Quaternion>
{
    public override Quaternion ReadJson(JsonReader reader, Type objectType, Quaternion existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        Quaternion q = Quaternion.identity;
        JObject obj = JObject.Load(reader); 
        q.x = (float)obj["X"];
        q.y = (float)obj["Y"];
        q.z = (float)obj["Z"];
        q.w = (float)obj["W"];
        return q;
        //쿼터니언은 4축 구성으로 xyzw로 구성
    }

    public override void WriteJson(JsonWriter writer, Quaternion value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("X");
        writer.WriteValue(value.x);
        writer.WritePropertyName("Y");
        writer.WriteValue(value.y);
        writer.WritePropertyName("Z");
        writer.WriteValue(value.z);
        writer.WritePropertyName("W");
        writer.WriteValue(value.w);
        writer.WriteEndObject();
    }
}
