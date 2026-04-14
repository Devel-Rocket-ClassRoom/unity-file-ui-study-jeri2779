using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTableTest2 : MonoBehaviour
{

    public Image icon;
    public LocalizationText nameText;
    public LocalizationText descText;

    public void OnEnable()
    {
        
    }
    public void SetEmpty()
    {
        Debug.Log("Empty 호출됨");
        icon.sprite = null;
        nameText.id = string.Empty;
        descText.id = string.Empty;
        //nameText.text.text = string.Empty;
        //descText.text.text = string.Empty;

    }

    public void SetItemData(ItemData data)
    {
        
        icon.sprite = data.SpriteIcon;
        nameText.id = data.Name;
        descText.id = data.Desc;

        nameText.OnChangedId();
        descText.OnChangedId();
        Debug.Log("Data 설정됨");
        Debug.Log(data.Name);
        Debug.Log(DataTableManager.StringTable.Get(data.Name));


    }

    public void SetItemData(string itemId)
    {
        ItemData data = DataTableManager.itemTable.Get(itemId);
        if (data != null)
        {
            SetItemData(data);
             
        }
    }
}
