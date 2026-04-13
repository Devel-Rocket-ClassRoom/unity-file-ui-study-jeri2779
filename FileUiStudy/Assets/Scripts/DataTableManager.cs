using System.Collections.Generic;
using UnityEngine;
public static class DataTableManager
{
    private static readonly Dictionary<string, DataTable> tables =
        new Dictionary<string, DataTable>();

    public static StringTable StringTable => Get<StringTable>(DataTableIds.String);

#if UNITY_EDITOR
    public static StringTable GetStringTable(Language lang)
    {
        return Get<StringTable>(DataTableIds.StringTableIds[(int)lang]);
    }
#endif

    static DataTableManager()
    {
        Init();
    }

    private static void Init()
    {
#if !UNITY_EDITOR
        var stringTable = new StringTable();
        stringTable.Load(DataTableIds.String);
        tables.Add(DataTableIds.String, stringTable);
#else
        foreach (var id in DataTableIds.StringTableIds)
        {
            var stringTable = new StringTable();
            stringTable.Load(id);
            tables.Add(id, stringTable);
        }
#endif
    }

    public static T Get<T>(string id) where T : DataTable
    {
        if (!tables.ContainsKey(id))
        {
            Debug.LogError("테이블 없음");
            return null;
        }
        return tables[id] as T;
    }

    public static void ChangeLanguage(Language lang)
    {
        string newId = DataTableIds.StringTableIds[(int)lang];
        if (tables.ContainsKey(newId)) 
        {
            return;
        }
        string oldId = string.Empty;
        foreach(var id in DataTableIds.StringTableIds)
        {
            if(tables.ContainsKey(id))
            {
                oldId = id;
                break;
            }
        }
        var stringTable = tables[oldId];
        stringTable.Load(DataTableIds.StringTableIds[(int)lang]);
        tables.Remove(oldId);
        tables.Add(newId, stringTable);
      


    }
}

//#if UNITY_EDITOR
//    [ContextMenu("언어/한국어로 변경")]
//    private void SetKorean()
//    {
//        editorLang = Language.Korean;
//        OnChangeLanguage(editorLang);
//    }

//    [ContextMenu("언어/영어로 변경")]
//    private void SetEnglish()
//    {
//        editorLang = Language.English;
//        OnChangeLanguage(editorLang);
//    }

//    [ContextMenu("언어/일본어로 변경")]
//    private void SetJapanese()
//    {
//        editorLang = Language.Japanese;
//        OnChangeLanguage(editorLang);
//    }

 
//#endif
