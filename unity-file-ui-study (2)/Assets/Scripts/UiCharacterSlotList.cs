using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiCharacterSlotList : MonoBehaviour
{
    public enum SortingOptions
    {
         
        CreationTimeAsscding,
        CreationTimeDeccending,
        NameAccending,
        NameDeaccending,
        AtkAccending,
        AtkDeaccending,
        DefAccending,
        DefDeaccending,
        HpAccending,
        HpDeaccending,
    }

    public enum FilteringOptions
    {
        None,
        Knight,
        Archer,
        Mage,

    }

    public readonly System.Comparison<SaveCharacterData>[] comparisons =
    {
        //should be changed to character data.
        (lhs, rhs) => lhs.CreationTime.CompareTo(rhs.CreationTime),
        (lhs, rhs) => rhs.CreationTime.CompareTo(lhs.CreationTime),

        (lhs, rhs) => lhs.CharacterData.StringName.CompareTo(rhs.CharacterData.StringName),
        (lhs, rhs) => rhs.CharacterData.StringName.CompareTo(lhs.CharacterData.StringName),

        (lhs, rhs) => lhs.CharacterData.Attack.CompareTo(rhs.CharacterData.Attack),
        (lhs, rhs) => rhs.CharacterData.Attack.CompareTo(lhs.CharacterData.Attack),

        (lhs, rhs) => lhs.CharacterData.Defense.CompareTo(rhs.CharacterData.Defense),
        (lhs, rhs) => rhs.CharacterData.Defense.CompareTo(lhs.CharacterData.Defense),

        (lhs, rhs) => lhs.CharacterData.Health.CompareTo(rhs.CharacterData.Health),
        (lhs, rhs) => rhs.CharacterData.Health.CompareTo(lhs.CharacterData.Health),
    };

    public readonly System.Func<SaveCharacterData, bool>[] filterings =
    {
        (x) => true,
        (x) => x.CharacterData.Type == CharacterTypes.Knight,
        (x) => x.CharacterData.Type == CharacterTypes.Archer,
        (x) => x.CharacterData.Type == CharacterTypes.Mage,
    };

    public UiCharacterSlot prefab;
    public ScrollRect scrollRect;

    private List<UiCharacterSlot> uiSlotList = new List<UiCharacterSlot >();

    private List<SaveCharacterData> saveCharacterDataList;

    private SortingOptions sorting = SortingOptions.CreationTimeAsscding;
    private FilteringOptions filtering = FilteringOptions.None;

    public SortingOptions Sorting
    {
        get => sorting;
        set
        {
            if (sorting != value)
            {
                sorting = value;
                UpdateSlots();
            }
        }
    }

    public FilteringOptions Filtering
    {
        get => filtering;
        set
        {
            if (filtering != value)
            {
                filtering = value;
                UpdateSlots();
            }
        }
    }

    private int selectedSlotIndex = -1;

    public UnityEvent onUpdateSlots;
    public UnityEvent<SaveCharacterData> onSelectSlot;

    private void OnSelectSlot(SaveCharacterData saveCharacterData)
    {
        //Debug.Log(saveCharacterData);
    }

    private void Start()
    {
        onSelectSlot.AddListener(OnSelectSlot);
    }

    private void OnDisable()
    {
        saveCharacterDataList = null;
    }

    public void SetSaveCharacterDataList(List<SaveCharacterData> source)
    {
        saveCharacterDataList = source.ToList();
        UpdateSlots();
    }

    public List<SaveCharacterData> GetSaveCharacterDataList()
    {
        return saveCharacterDataList;
    }

    private void UpdateSlots()
    {
        var list = saveCharacterDataList.Where(filterings[(int)filtering]).ToList();
        list.Sort(comparisons[(int)sorting]);

        if (uiSlotList.Count < list.Count)
        {
            for (int i = uiSlotList.Count; i < list.Count; ++i)
            {
                var newSlot = Instantiate(prefab, scrollRect.content);
                newSlot.slotIndex = i;
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                newSlot.button.onClick.AddListener(() =>
                {
                    selectedSlotIndex = newSlot.slotIndex;
                    onSelectSlot.Invoke(newSlot.SaveCharacterData);
                });

                uiSlotList.Add(newSlot);
            }
        }

        for (int i = 0; i < uiSlotList.Count; ++i)
        {
            if (i < list.Count)
            {
                uiSlotList[i].gameObject.SetActive(true);
                uiSlotList[i].SetItem(list[i]);
            }
            else
            {
                uiSlotList[i].gameObject.SetActive(false);
                uiSlotList[i].SetEmpty();
            }
        }

        selectedSlotIndex = -1;
        onUpdateSlots.Invoke();
    }

    public void AddRandomCharacter()
    {
        saveCharacterDataList.Add(SaveCharacterData.GetRandomCharacter());
        UpdateSlots();
    }

    public void RemoveCharacter()
    {
        if (selectedSlotIndex == -1)
        {
            return;
        }

        saveCharacterDataList.Remove(uiSlotList[selectedSlotIndex].SaveCharacterData);
        UpdateSlots();
    }
}
