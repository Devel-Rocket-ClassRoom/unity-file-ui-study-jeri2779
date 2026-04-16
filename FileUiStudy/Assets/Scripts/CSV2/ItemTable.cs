using System.Collections.Generic;
using UnityEngine;


public class ItemData
{
    public string Id { get; set; }
    public ItemType Type { get; set; }
    public string Name { get; set; }    
    public string Desc { get; set; }
    public int Value { get; set; }
    public int Cost { get; set; }
    public string Icon { get; set; }

    public string StringName => DataTableManager.StringTable.Get(Name);
    public string StringDesc => DataTableManager.StringTable.Get(Desc);

    public Sprite SpriteIcon => Resources.Load<Sprite>($"Icons/{Icon}");
    public override string ToString()
    {
        return $"Id: {Id} / {Type}/{Name}/ {Desc}/{Value} / {Cost}/ {Icon}";
    }

}

public class ItemTable : DataTable
{
    private readonly Dictionary<string, ItemData> table = new Dictionary<string, ItemData>();
  
    public override void Load(string filename)
    {
        table.Clear();
        
        var path = string.Format(FormatPath,filename);
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        List<ItemData> list = LoadCSV<ItemData>(textAsset.text);
        foreach (var data in list)
        {
            if (!table.ContainsKey(data.Id))
            {
                table.Add(data.Id, data);
            }
            else
            {
                Debug.LogWarning($"아이템  중복'{data.Id} - {filename}'");
            }
        }
    }
    public ItemData Get(string id)
    {
        if(!table.ContainsKey(id))
        {
            Debug.LogWarning($"아이템 없음'{id}'");
            return null;
        }
        return table[id];
  
    }
    public IReadOnlyList<string> GetAllIds()//딕셔너리가 private이므로 외부에서 키 읽기용 
    {
        return new List<string>(table.Keys);
    }
}

