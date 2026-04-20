//using System;
//using System.Collections.Generic;
//using UnityEngine;

//[Serializable]
//public abstract class SaveData
//{ 
//    public int Version { get; protected set; } 

//    public abstract SaveData VersionUp();
//}

//[Serializable]
//public class SaveDataV1 : SaveData
//{
//    public string PlayerName { get; set; } = string.Empty;

//    public SaveDataV1()
//    {
//        Version = 1;
//    }

//    public override SaveData VersionUp()
//    {
//        var saveData = new SaveDataV2();
//        saveData.Name = PlayerName;
//        saveData.Gold = 0;
//        return saveData;
//    }
//}

//public class SaveDataV2 : SaveData
//{
//    public string Name { get; set; } = string.Empty;
//    public int Gold;

//    public SaveDataV2()
//    {
//        Version = 2;
//    }

//    public override SaveData VersionUp()
//    {
//        var saveData = new SaveDataV3();
//        saveData.Name = Name;
//        saveData.Gold = Gold;
//        return saveData;
//    }
//}

//public class SaveDataV3 : SaveData
//{
//    public string Name { get; set; } = string.Empty;
//    public int Gold;
//    public List<SaveItemData> ItemList = new List<SaveItemData>();

//    public SaveDataV3()
//    {
//        Version = 3;
//    }

//    public override SaveData VersionUp()
//    {
//        // 현재 최신 버전. 호출되면 안 되는 경로.
//        throw new InvalidOperationException("SaveDataV3은 최신 버전입니다. VersionUp() 호출 대상이 아닙니다.");
//    }
//}