using UnityEngine;
using UnityEngine.Events;

public class UiItemSelect : MonoBehaviour
{
    public GameObject panel; 
    public UiInvenSlotList uiInvenSlotList;
    public UnityEvent<SaveItemData> onItemSelected;

    public UiInvenSlotList.FilteringOptions CurrentFilter { get; private set; }
    private void Start()
    {
        //Open(UiInvenSlotList.FilteringOptions.Weapon);
    }

    public void Open(UiInvenSlotList.FilteringOptions filter)
    {
        CurrentFilter = filter;
        panel.SetActive(true);
        uiInvenSlotList.SetSaveItemDataList(SaveLoadManager.Data.ItemList);
        uiInvenSlotList.Filtering = filter;

        // RemoveListener 후 AddListener로 중복 방지
        uiInvenSlotList.onSelectSlot.RemoveListener(OnSelectItem);
        uiInvenSlotList.onSelectSlot.AddListener(OnSelectItem);
    }

    public void Close()
    {
        uiInvenSlotList.onSelectSlot.RemoveListener(OnSelectItem);
        panel.SetActive(false);
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