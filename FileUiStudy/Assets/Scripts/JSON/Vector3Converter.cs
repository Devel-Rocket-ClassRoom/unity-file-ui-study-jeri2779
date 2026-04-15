using UnityEngine;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;

public class Vector3Converter : JsonConverter<Vector3>
{
    public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        Vector3 v = Vector3.zero;

        JObject obj = JObject.Load(reader);//JSON 객체를 읽어와서 JObject로 변환
        v.x = (float)obj["X"];//JObject에서 "X"라는 키에 해당하는 값을 읽어와서 float로 변환하여 v.x에 할당
        v.y = (float)obj["Y"];
        v.z = (float)obj["Z"];
        return v;
    }

    public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
    {
        writer.WriteStartObject();//JSON 객체의 시작을 나타냄
        writer.WritePropertyName("X");
        writer.WriteValue(value.x);
        writer.WritePropertyName("Y");
        writer.WriteValue(value.y);
        writer.WritePropertyName("Z");
        writer.WriteValue(value.z);
        writer.WriteEndObject();
    }
}
