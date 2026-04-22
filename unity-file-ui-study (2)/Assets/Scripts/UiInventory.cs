using UnityEngine;
using TMPro;

public class UiInventory : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;

    public UiInvenSlotList uiInvenSlotList;

    private void OnEnable()
    {
        OnLoad();
        sorting.value = (int)SaveLoadManager.Data.SortingOption;
        filtering.value = (int)SaveLoadManager.Data.FilteringOption;

        OnChangeFiltering(filtering.value);
        OnChangeSorting(sorting.value);
    }

    public void OnChangeSorting(int index)
    {
        uiInvenSlotList.Sorting = (UiInvenSlotList.SortingOptions)index;
    }

    public void OnChangeFiltering(int index)
    {
        uiInvenSlotList.Filtering = (UiInvenSlotList.FilteringOptions)index;
    }

    public void OnSave()
    {
        SaveLoadManager.Data.ItemList = uiInvenSlotList.GetSaveItemDataList();
        SaveLoadManager.Data.SortingOption = (UiInvenSlotList.SortingOptions)sorting.value;
        SaveLoadManager.Data.FilteringOption = (UiInvenSlotList.FilteringOptions)filtering.value;
        SaveLoadManager.Save();
    }

    public void OnLoad()
    {
        SaveLoadManager.Load();
        sorting.value = (int)SaveLoadManager.Data.SortingOption;
        filtering.value = (int)SaveLoadManager.Data.FilteringOption;
        OnChangeFiltering(filtering.value);
        OnChangeSorting(sorting.value);
        uiInvenSlotList.SetSaveItemDataList(SaveLoadManager.Data.ItemList);
    }

    public void OnCreateItem()
    {
        uiInvenSlotList.AddRandomItem();
    }

    public void OnRemoveItem()
    {
        uiInvenSlotList.RemoveItem();
    }
}
