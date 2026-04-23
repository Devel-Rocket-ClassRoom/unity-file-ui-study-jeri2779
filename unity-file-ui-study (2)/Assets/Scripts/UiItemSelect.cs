using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;   

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
        


        uiInvenSlotList.onSelectSlot.RemoveListener(OnSelectItem);
        uiInvenSlotList.onSelectSlot.AddListener(OnSelectItem);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    public void Close()
    {
        panel.SetActive(false);
        uiInvenSlotList.onSelectSlot.RemoveListener(OnSelectItem);
    }

    private void OnSelectItem(SaveItemData saveItemData)
    {
        onItemSelected.Invoke(saveItemData);
         
    }

    public void OnClickUnequip()
    {
        onItemSelected.Invoke(null);
        
    }
    
    
}