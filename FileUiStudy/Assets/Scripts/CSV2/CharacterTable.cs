using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;
using UnityEngine;
// 1. CSV 파일 만들기 (ID / 이름 / 설명 / 공격력.... / 초상화 or 아이콘 ...)
// 2. DataTable 상속 CharacterTable 구현
// 3. DataTableManager에 등록
// 4. 테스트 패널 만들기

public class CharacterData
{
    public string Id { get; set; }
    public string Type { get; set; }
    [Name("Name")]
    public string charName { get; set; }
    [Name("Info")]
    public string Description { get; set; }
    [Name("Atk")]
    public float AttackPower { get; set; }
    [Name("Def")]
    public float DefensePower { get; set; }
    public float Hp { get; set; }
    [Name("Icon")]
    public string iconImage { get; set; }

    public string StringName => DataTableManager.StringTable.Get(charName);
    public string StringInfo => DataTableManager.StringTable.Get(Description);

    public string StringAtk => DataTableManager.StringTable.Get(AttackPower.ToString());
    public string StringDef => DataTableManager.StringTable.Get(DefensePower.ToString());
    public string StringHp => DataTableManager.StringTable.Get(Hp.ToString());
    public string StringType => DataTableManager.StringTable.Get(Type);
    public Sprite Icon => Resources.Load<Sprite>($"Icons/{iconImage}");
    public override string ToString()
    {
        return $"Id: {Id} / {charName} / {Description} / {AttackPower} / {DefensePower} / {Hp} / {iconImage}";
    }

}
public class CharacterTable : DataTable
{
    private readonly Dictionary<string, CharacterData> charTable = new Dictionary<string, CharacterData>();
    public override void Load(string filename)
    {
        charTable.Clear();

        var path = string.Format(FormatPath, filename);
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        List<CharacterData> list = LoadCSV<CharacterData>(textAsset.text);
        foreach (var data in list)
        {
            if (!charTable.ContainsKey(data.Id))
            {
                charTable.Add(data.Id, data);
            }
            else
            {
                Debug.LogWarning($"캐릭터 중복'{data.Id} - {filename}'");
            }
        }
    }
    public CharacterData Get(string id)
    {
        if (!charTable.ContainsKey(id))
        {
            Debug.LogWarning($"캐릭터 없음'{id}'");
            return null;
        }
        return charTable[id];

    }
}
