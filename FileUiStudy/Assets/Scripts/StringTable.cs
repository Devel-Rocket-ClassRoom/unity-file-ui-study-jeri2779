using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class StringTable : DataTable
{
    public static readonly string UnKnown = "키 없음";

    public class Data
    {
        public string Id { get; set; }
        public string String { get; set; }
    }

    private readonly Dictionary<string, string> table = new Dictionary<string, string>();
    public override void Load(string filename)
    {
        table.Clear();
        
        var path = string.Format(FormatPath,filename);
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<Data>(textAsset.text);
        foreach (var data in list)
        {
            if (!table.ContainsKey(data.Id))
            {
                table.Add(data.Id, data.String);
            }
            else
            {
                Debug.LogWarning($"키 중복'{data.Id} - {filename}'");
            }
        }
    }
    
    public string Get(string key)
    {
        if(!table.ContainsKey(key))
        {
            return UnKnown;
        }
        return table[key];
 
    }
}
