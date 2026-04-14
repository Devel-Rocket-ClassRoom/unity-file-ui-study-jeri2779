using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharTableMain : MonoBehaviour
{
    //name/atk/def/info/icon/
    public Image icon;
    public LocalizationText nameText;
    public LocalizationText infoText;
    public LocalizationText atkText;
    public LocalizationText defText;
    public LocalizationText hpText;
    public LocalizationText typeText;

    public void OnEnable()
    {

    }
    public void SetEmpty()
    {
        Debug.Log("Empty 호출됨");
        icon.sprite = null;
        nameText.id = string.Empty;
        infoText.id = string.Empty;
        atkText.id = string.Empty;
        defText.id = string.Empty;
        hpText.id = string.Empty;
        typeText.id = string.Empty;
        //nameText.text.text = string.Empty;
        //descText.text.text = string.Empty;

    }

    public void SetCharacterData(CharacterData data)
    {
        if (icon == null || nameText == null || infoText == null ||
            atkText == null || defText == null || hpText == null || typeText == null)
        {
            Debug.LogWarning("CharTableMain: Inspector 필드가 할당되지 않았습니다.");
            return;
        }
        icon.sprite = data.Icon;
        nameText.id = data.StringName;
        infoText.id = data.StringInfo;
        atkText.id = data.StringAtk;
        defText.id = data.StringDef;
        hpText.id = data.StringHp;
        typeText.id = data.StringType;
        

        nameText.OnChangedId();
        infoText.OnChangedId();
        atkText.OnChangedId();
        defText.OnChangedId();
        hpText.OnChangedId();
        typeText.OnChangedId();
        Debug.Log("Data 설정됨");
        Debug.Log(data.StringName);
        Debug.Log(DataTableManager.StringTable.Get(data.StringName));


    }

    public void SetCharacterData(string charId)
    {
        CharacterData data = DataTableManager.CharacterTable.Get(charId);
        if (data != null)
        {
            SetCharacterData(data);

        }
    }
}

