using UnityEngine;
using UnityEngine.UI;
using TMPro;

 
[RequireComponent(typeof(Button))]
public class CharTableMain : MonoBehaviour
{
    
    public Image icon;

  
    public LocalizationText nameText;
    public LocalizationText infoText;
    public LocalizationText typeText;


    public TextMeshProUGUI atkText;
    public TextMeshProUGUI defText;
    public TextMeshProUGUI hpText;
    public void SetEmpty()
    {
        icon.sprite = null;

        nameText.id = string.Empty;
        infoText.id = string.Empty;
        typeText.id = string.Empty;

        atkText.text = string.Empty;
        defText.text = string.Empty;
        hpText.text = string.Empty;
    }

    public void SetCharacterData(CharacterData data)
    {
        if (icon == null || nameText == null || infoText == null ||
            typeText == null || atkText == null || defText == null || hpText == null)
        {
            Debug.LogWarning("할당X");
            return;
        }

        icon.sprite = data.Icon;

        nameText.id = data.CharName;
        nameText.OnChangedId();

        infoText.id = data.Description;
        infoText.OnChangedId();

        typeText.id = data.Type;
        typeText.OnChangedId();


        atkText.text = ($"ATK : {data.StringAtk}");
        defText.text = ($"DEF : {data.StringDef}");
        hpText.text = ($"HP : {data.StringHp}");

        Debug.Log($"캐릭터 : {data}");
    }

    public void SetCharacterData(string charId)
    {
        CharacterData data = DataTableManager.CharacterTable.Get(charId);
        if (data != null)
            SetCharacterData(data);
    }
}

