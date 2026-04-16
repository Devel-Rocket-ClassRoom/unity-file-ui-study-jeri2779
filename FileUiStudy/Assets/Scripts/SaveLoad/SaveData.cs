using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public abstract class SaveData
{
    public int Version { get; protected set; }
    public abstract SaveData VersionUp();
}
[System.Serializable]
public class SaveDataV1 : SaveData 
{  
    public string PlayerName {  get; set; } = string.Empty;
    public SaveDataV1()
    {
        Version = 1;
    }

    public override SaveData VersionUp()
    {
        var saveDataV2 = new SaveDataV2();
        saveDataV2.Name = PlayerName;
        return saveDataV2;  
    }
}
[System.Serializable]
public class SaveDataV2 : SaveData
{
    public string Name { get; set; } = string.Empty;
    public int Gold { get; set; } = 0;
    public SaveDataV2()
    {
        Version = 2;
    }
    public override SaveData VersionUp()
    {
       var saveDataV3 = new SaveDataV3();
        saveDataV3.Name = Name;
        saveDataV3.Gold = Gold;
        //saveDataV3.ItemId = SaveLoadManager.CreateRandomItems();
        return saveDataV3;
    }
}
[System.Serializable]
//public class SaveDataV3 : SaveData
//{
//    //아이템 리스트 추가(ItemTable "ItemId")
//    //랜덤하게 아이템 추가
//    //V1,V2, -> V3 마이그레이션
//    public string Name { get; set; }
//    public int Gold { get; set; }
//    public List<string> ItemId { get; set; } = new List<string>();

//    public SaveDataV3()
//    {
//        Version = 3;
//    }
//    public override SaveData VersionUp()
//    {
         
//        return this;
//    }


//}

public class SaveDataV3 : SaveDataV2
{
    public List<string> ItemId { get; set; } = new List<string>();
    public SaveDataV3()
    {
        Version = 3;
    }

    public override SaveData VersionUp()
    {
        return this;
    }
    

}

