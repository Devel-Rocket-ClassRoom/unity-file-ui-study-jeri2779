using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiCharacterInfo : MonoBehaviour
{
    public static readonly string FormatCommon = "{0}: {1}";

    public Image charImageIcon;
    public TextMeshProUGUI textCharName;
    public TextMeshProUGUI textCharDescription;
    public TextMeshProUGUI textCharType;
    public TextMeshProUGUI textCharAttack;
    public TextMeshProUGUI textCharDefense;
    public TextMeshProUGUI textCharHealth;

    public UiInvenSlot weaponSlot;
    public UiInvenSlot armorSlot;
    public UiItemSelect itemSelectPopup;
    public void SetEmpty()
    {
        charImageIcon.sprite = null;
        textCharName.text = string.Empty;
        textCharDescription.text = string.Empty;
        textCharType.text = string.Empty;
        textCharAttack.text = string.Empty;
        textCharDefense.text = string.Empty;
        textCharHealth.text = string.Empty;

        weaponSlot.SetEmpty();
        armorSlot.SetEmpty();
    }

    public void SetSaveCharacterData(SaveCharacterData saveCharacterData)
    {
        var st = DataTableManager.StringTable;
        var data = saveCharacterData.CharacterData;

        charImageIcon.sprite = data.SpriteIcon;
        textCharName.text = string.Format(FormatCommon, st.Get("NAME"), data.StringName);
        textCharDescription.text = string.Format(FormatCommon, st.Get("DESC"), data.StringDesc);
        string typeId = data.Type.ToString().ToUpper();
        textCharType.text = string.Format(FormatCommon, st.Get("TYPE"), st.Get(typeId));
        textCharAttack.text = string.Format(FormatCommon, st.Get("CHARATTACK"), data.Attack);
        textCharDefense.text = string.Format(FormatCommon, st.Get("CHARDEFENSE"), data.Defense);
        textCharHealth.text = string.Format(FormatCommon, st.Get("CHARHEALTH"), data.Health);

        if (saveCharacterData.EquippedWeapon != null)
        {
            weaponSlot.SetItem(saveCharacterData.EquippedWeapon);
        }
        else
        {
            weaponSlot.SetEmpty();
        }
       
        if (saveCharacterData.EquippedArmor != null)
        {
            armorSlot.SetItem(saveCharacterData.EquippedArmor);
        }
        else
        {
            armorSlot.SetEmpty();
        }   
    }
}
