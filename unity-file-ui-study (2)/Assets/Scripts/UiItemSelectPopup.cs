using UnityEngine;
using UnityEngine.Events;

public class UiItemSelectPopup : MonoBehaviour
{
    public UiInvenSlotList uiInvenSlotList;
    public UnityEvent<SaveItemData> onItemSelected;

    public UiInvenSlotList.FilteringOptions CurrentFilter { get; private set; }
    private void Start()
    {
        //Open(UiInvenSlotList.FilteringOptions.Weapon);
    }

    public void Open(UiInvenSlotList.FilteringOptions filter)
    {
        Debug.Log("Open 호출됨: " + filter);
        CurrentFilter = filter;
        gameObject.SetActive(true);

        uiInvenSlotList.SetSaveItemDataList(SaveLoadManager.Data.ItemList);
        uiInvenSlotList.Filtering = filter;
        uiInvenSlotList.onSelectSlot.AddListener(OnSelectItem);
    }

    public void Close()
    {
        uiInvenSlotList.onSelectSlot.RemoveListener(OnSelectItem);
        gameObject.SetActive(false);
    }

    private void OnSelectItem(SaveItemData saveItemData)
    {
        onItemSelected.Invoke(saveItemData);
        Close();
    }

    public void OnClickUnequip()
    {
        onItemSelected.Invoke(null);
        Close();
    }
}