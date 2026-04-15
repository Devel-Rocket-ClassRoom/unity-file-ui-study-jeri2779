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
        
        OnChangeItemId();
    }
    private void OnEnable()
    {
        OnChangeItemId();
    }


    public void OnChangeItemId()
    {
        CharacterData data = DataTableManager.CharacterTable.Get(charId);
        
        if (data== null)
        {
                Debug.LogWarning($"[CharTableSelect] 캐릭터 데이터 없음: '{charId}'");
                return;
        }
        if (icon == null || nameText == null)
            {
                Debug.LogWarning(" 필드가 할당되지 않았습니다.");
                return;
            }
         
            icon.sprite = data.Icon;
            nameText.id = data.CharName;
            nameText.OnChangedId();

        if (typeText != null)
        {
            typeText.id = data.Type;
            typeText.OnChangedId();
        }
    }

    public void OnClick()
    {
        if (charInfo == null)
        {
            Debug.LogWarning("[CharTableSelect] charInfo가 연결되지 않았습니다.");
            return;
        }
        charInfo.SetCharacterData(charId);
    }
}

