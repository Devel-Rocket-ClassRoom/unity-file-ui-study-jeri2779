using UnityEngine;
using TMPro;

[ExecuteInEditMode]  
public class StringTableText : MonoBehaviour
{
    public string id;
    public string key;
    public TextMeshProUGUI text;

 
    private void OnValidate()
    {
        OnChangedId();
    }

    public void OnChangedId()
    {
        if (text == null) return;  
        text.text = DataTableManager.StringTable.Get(id);
    }
    public void OnChangedKey()
    {
        if (text == null) return;  
        text.text = DataTableManager.StringTable.Get(key);
    }   

}
