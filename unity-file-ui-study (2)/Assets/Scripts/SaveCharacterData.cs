using System;
using Newtonsoft.Json;

[Serializable]
public class SaveCharacterData
{
    public Guid InstanceId { get; set; }

    
    public CharacterData CharacterData { get; set; }
    public DateTime CreationTime { get; set; }

    public SaveItemData EquippedWeapon { get; set; } = null;
    public SaveItemData EquippedArmor { get; set; } = null;

    public static SaveCharacterData GetRandomCharacter()
    {
        SaveCharacterData newCharacter = new SaveCharacterData();
        newCharacter.CharacterData = DataTableManager.CharacterTable.GetRandom();
        return newCharacter;
    }

    public SaveCharacterData()
    {
        InstanceId = Guid.NewGuid();
        CreationTime = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{InstanceId}\n{CreationTime}\n{CharacterData.Id}";
    }

}
