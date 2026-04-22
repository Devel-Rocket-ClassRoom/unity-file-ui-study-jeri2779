using Mono.Cecil;
using Newtonsoft.Json;
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

public class CharacterData
{
    public string Id { get; set; }
    public CharacterTypes Type { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }

    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Health { get; set; }

    public string Icon { get; set; }

    public string StringName => DataTableManager.StringTable.Get(Name);
    public string StringDesc => DataTableManager.StringTable.Get(Desc);

    [JsonIgnore]
    public Sprite SpriteIcon => Resources.Load<Sprite>($"Icon/{Icon}");

    public override string ToString()
    {
        return $"{Id} / {Type} / {Name} / {Desc} / {Attack} / {Defense} / {Health} / {Icon}";
    }
}

public class CharacterTable : DataTable
{
    private readonly Dictionary<string, CharacterData> table =
        new Dictionary<string, CharacterData>();

    private List<string> charKeyList;

    public override void Load(string filename)
    {
        table.Clear();

        string path = string.Format(FormatPath, filename);
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        List<CharacterData> list = LoadCSV<CharacterData>(textAsset.text);

        foreach (var character in list)
        {
            if (!table.ContainsKey(character.Id))
            {
                table.Add(character.Id, character);
            }
            else
            {
                Debug.LogError("캐릭터 아이디 중복");
            }
        }

        charKeyList = table.Keys.ToList();
    }

    public CharacterData Get(string id)
    {
        if (!table.ContainsKey(id))
        {
            Debug.LogError("캐릭터 아이디 없음");
            return null;
        }
        return table[id];
    }

    public CharacterData GetRandom()
    {
        return Get(charKeyList[Random.Range(0, charKeyList.Count)]);
    }
}
