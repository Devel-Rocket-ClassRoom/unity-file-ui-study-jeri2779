using UnityEngine;
using UnityEngine.UI;

public class ItemTableTest1 : MonoBehaviour
{
     
    public string itemId;
    public Image icon;
    public LocalizationText nameText;

    public ItemTableTest2 itemInfo;


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
        ItemData data = DataTableManager.itemTable.Get(itemId);
        if(data != null )
        {
            icon.sprite = data.SpriteIcon;
            nameText.id = data.Name;
            nameText.OnChangedId();
        }
    }

    public void OnClick()
    {
        itemInfo.SetItemData(itemId);
    }
}
