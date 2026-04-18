using UnityEngine;
using System.IO;
using Newtonsoft.Json;


[System.Serializable]
public class OptionData
{
    public string name = string.Empty;  
    public string difficulty = "Easy";
    public int score = 0;
  
}
public static class OptionManager
{ 
    public static OptionData optionData = new OptionData();

    public static void SaveOptions()
    {
        string dir = Application.persistentDataPath;
        string path = Path.Combine(dir, "options.json");


        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
             
        string json = JsonConvert.SerializeObject(optionData, Formatting.Indented);
        File.WriteAllText(path, json);
    }   

    public static void LoadOptions()
    {
        string path = Path.Combine(Application.persistentDataPath, "options.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            optionData = JsonConvert.DeserializeObject<OptionData>(json);
        }
        Debug.Log($"Name={optionData.name}, " +
                    $"Difficulty={optionData.difficulty}, " +
                    $"Score={optionData.score}");
    }
}
