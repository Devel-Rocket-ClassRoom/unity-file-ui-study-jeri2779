using UnityEngine;
using TMPro;

public class ItemDataManager : MonoBehaviour
{
    

    public static ItemTable itemTable => DataTableManager.itemTable;

    public TextMeshProUGUI itemName;
     


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //버튼 클릭시 아이템 데이터 가져오기
    public void OnClickButtonSword()
    {
        var itemData = itemTable.Get("Item1");
        if (itemData == null) return;
        if (itemName != null) itemName.text = itemData.Name;
        Debug.Log(itemData.Name);
    }
    public void OnClickButtonShield()
    {
        var itemData = itemTable.Get("Item2");
        if (itemData == null) return;
        if (itemName != null) itemName.text = itemData.Name;
        Debug.Log(itemData.Name);
    }

    public void OnClickButtonPotion()
    {
        var itemData = itemTable.Get("Item3");
        if (itemData == null) return;
        if (itemName != null) itemName.text = itemData.Name;
        Debug.Log(itemData.Name);
    }
    public void OnClickButtonBow()
    {
        var itemData = itemTable.Get("Item4");
        if (itemData == null) return;
        if (itemName != null) itemName.text = itemData.Name;
        Debug.Log(itemData.Name);
    }


}
 
 