using Mono.Cecil;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/*
Id, Type, Name, Desc, Value, Cost, Icon
ITEM_1, Weapon, ITEM_1_NAME, ITEM_1_DESC,10,500, Icon_Sword01
ITEM_2, Equip, ITEM_2_NAME, ITEM_2_DESC,20,200, Icon_Shield01
ITEM_3, Weapon, ITEM_3_NAME, ITEM_3_DESC,5,1000, Icon_Bow01
ITEM_4, Consumable, ITEM_4_NAME, ITEM_4_DESC,50,50, Icon_Heart01
*/

public class ItemData
{
    public string Id { get; set; }
    public ItemTypes Type { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public int Value { get; set; }
    public int Cost { get; set; }
    public string Icon { get; set; }

    public string StringName => DataTableManager.StringTable.Get(Name);
    public string StringDesc => DataTableManager.StringTable.Get(Desc);
    public Sprite SpriteIcon => Resources.Load<Sprite>($"Icon/{Icon}");

    public override string ToString()
    {
        return $"{Id} / {Type} / {Name} / {Desc} / {Value} / {Cost} / {Icon}";
    }
}

public class ItemTable : DataTable
{
    private readonly Dictionary<string, ItemData> table =
        new Dictionary<string, ItemData>();

    private List<string> keyList;

    public override void Load(string filename)
    {
        table.Clear();

        string  path = string.Format(FormatPath, filename);
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        List<ItemData> list = LoadCSV<ItemData>(textAsset.text);

        foreach (var item in list)
        {
            if (!table.ContainsKey(item.Id))
            {
                table.Add(item.Id, item);
            }
            else
            {
                Debug.LogError("아이템 아이디 중복");
            }
        }

        keyList = table.Keys.ToList();
    }

    public ItemData Get(string id)
    {
        if (!table.ContainsKey(id))
        {
            Debug.LogError("아이템 아이디 없음");
            return null;
        }
        return table[id];
    }

    public ItemData GetRandom()
    {
        return Get(keyList[Random.Range(0, keyList.Count)]);
    }
}
