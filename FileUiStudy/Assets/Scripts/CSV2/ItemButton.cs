using UnityEngine;
using TMPro;
using UnityEngine.UI;
// 버튼 4개 각각에 부착
// 아이콘 + 이름 표시, 클릭 시 중앙 패널에 데이터 전달
public class ItemButton : MonoBehaviour
{
    public string id;

    public Image icon;
    public TextMeshProUGUI textname;
    public TextMeshProUGUI desc;//중앙용

    public ItemButton centerButton;

    private ItemData itemData;

    private void Start()
    {
        var data = DataTableManager.itemTable.Get(id);
        if (data != null)
        {
            if (icon != null) icon.sprite = data.SpriteIcon;
            if (textname != null) textname.text = data.StringName;
        }
    }
    public void OnClickCenter()
    {
        var data = DataTableManager.itemTable.Get(id);
        if (data != null)
        {
            centerButton.SetItem(data);
        }
    }
    public void SetItem(ItemData data)
    {
        itemData = data;
        if (icon != null) icon.sprite = data.SpriteIcon;
        if (textname != null) textname.text = data.StringName;
        if (desc != null) desc.text = itemData.StringDesc;
    }
 

    
    
}
