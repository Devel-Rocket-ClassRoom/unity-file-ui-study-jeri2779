using UnityEngine;
using UnityEngine.UI;

public class CharTableSelect : MonoBehaviour
{

    public string charId;
    public Image icon;
    public LocalizationText nameText;
    public LocalizationText typeText;

    public CharTableMain charInfo;

    private void OnValidate()
    {
        Debug.Log("OnValidate");
        OnChangeItemId();
    }
    private void OnEnable()
    {
        OnChangeItemId();
    }


    public void OnChangeItemId()
    {
        CharacterData data = DataTableManager.CharacterTable.Get(charId);
        if (data != null)
        {
            if (icon == null || nameText == null)
            {
                Debug.LogWarning("CharTableSelect: Inspector 필드가 할당되지 않았습니다.");
                return;
            }
            icon.sprite = data.Icon;
            nameText.id = data.StringName;
            nameText.OnChangedId();
        }
        else
        {
            Debug.LogWarning($"캐릭터 데이터 없음'{charId}'");
        }
    }

    public void OnClick()
    {
        charInfo.SetCharacterData(charId);
    }
}

