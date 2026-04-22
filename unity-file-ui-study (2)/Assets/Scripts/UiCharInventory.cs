using UnityEngine;
using TMPro;

public class UiCharInventory : MonoBehaviour
{
    public TMP_Dropdown charSorting;
    public TMP_Dropdown charFiltering;

    public UiCharacterSlotList uiCharacterSlotList;
    public UiCharacterInfo uiCharacterInfo; // 추가

    private SaveCharacterData currentData;

    private void OnEnable()
    {
        OnLoad();
        charSorting.value = (int)SaveLoadManager.Data.charSortingOption;
        charFiltering.value = (int)SaveLoadManager.Data.charFilteringOption;

        OnChangeFiltering(charFiltering.value);
        OnChangeSorting(charSorting.value);

        // 슬롯 선택 시 캐릭터 정보 표시 연결
        uiCharacterSlotList.onSelectSlot.AddListener(OnSelectSlot);
        uiCharacterInfo.itemSelectPopup.onItemSelected.AddListener(OnEquipItem);
    }

    private void OnDisable()
    {
        uiCharacterSlotList.onSelectSlot.RemoveListener(OnSelectSlot);
        uiCharacterInfo.itemSelectPopup.onItemSelected.RemoveListener(OnEquipItem);
    }

    private void OnSelectSlot(SaveCharacterData saveCharacterData)
    {
        Debug.Log("OnSelectSlot 호출됨: " + (saveCharacterData == null ? "null" : saveCharacterData.CharacterData.Id));
        if (saveCharacterData != null)
        {
            currentData = saveCharacterData;
            uiCharacterInfo.SetSaveCharacterData(saveCharacterData);
        }
        else
            uiCharacterInfo.SetEmpty();
    }

    public void OnChangeSorting(int index)
    {
        uiCharacterSlotList.Sorting = (UiCharacterSlotList.SortingOptions)index;
    }

    public void OnChangeFiltering(int index)
    {
        uiCharacterSlotList.Filtering = (UiCharacterSlotList.FilteringOptions)index;
    }

    public void OnSave()
    {
        SaveLoadManager.Data.CharacterList = uiCharacterSlotList.GetSaveCharacterDataList();
        SaveLoadManager.Data.charSortingOption = (UiCharacterSlotList.SortingOptions)charSorting.value;
        SaveLoadManager.Data.charFilteringOption = (UiCharacterSlotList.FilteringOptions)charFiltering.value;
        SaveLoadManager.Save();
    }

    public void OnLoad()
    {
        SaveLoadManager.Load();
        charSorting.value = (int)SaveLoadManager.Data.charSortingOption;
        charFiltering.value = (int)SaveLoadManager.Data.charFilteringOption;
        OnChangeFiltering(charFiltering.value);
        OnChangeSorting(charSorting.value);
        uiCharacterSlotList.SetSaveCharacterDataList(SaveLoadManager.Data.CharacterList);
    }

    public void OnCreateCharacter()
    {
        uiCharacterSlotList.AddRandomCharacter();
    }

    public void OnRemoveCharacter()
    {
        uiCharacterSlotList.RemoveCharacter();
    }

    public void OnEquipItem(SaveItemData saveItemData)
    {
        if (uiCharacterInfo.itemSelectPopup.CurrentFilter == UiInvenSlotList.FilteringOptions.Weapon)
            currentData.EquippedWeapon = saveItemData;
        else
            currentData.EquippedArmor = saveItemData;

        uiCharacterInfo.SetSaveCharacterData(currentData);
    }

    public void OnClickWeaponSlot()
    {
        Debug.Log("WeaponSlot 클릭됨, currentData: " + (currentData == null ? "null" : currentData.CharacterData.Id));
        if (currentData == null) return;
        uiCharacterInfo.itemSelectPopup.Open(UiInvenSlotList.FilteringOptions.Weapon);
    }

    public void OnClickArmorSlot()
    {
        if (currentData == null) return;
        uiCharacterInfo.itemSelectPopup.Open(UiInvenSlotList.FilteringOptions.Equip);
    }
}
