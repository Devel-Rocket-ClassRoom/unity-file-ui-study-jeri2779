using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class LocalizationText : MonoBehaviour
{
  
#if UNITY_EDITOR
    public Language editorLang;
#endif
    public string id;
    
    private string originId;//초기 출력id값 저장용 
    public TextMeshProUGUI text;
 
    private void OnEnable()
    {
        if (Application.isPlaying)
        {
            Variables.OnLanguageChanged += OnChangeLanguage;
            OnChangedId();
        }
#if UNITY_EDITOR
        else
            OnChangeLanguage(editorLang);
#endif
    }
    private void OnDisable()
    {
        if (Application.isPlaying)
        {
            Variables.OnLanguageChanged -= OnChangeLanguage;
        }
    }
    private void OnValidate()
    {
#if UNITY_EDITOR
        OnChangeLanguage(editorLang);
#else
        OnChangedId();
#endif
    }
    public void OnChangedId()
    {
        if (text == null) return;
        text.text = DataTableManager.StringTable.Get(id);
    }
 
#if UNITY_EDITOR
    
    private void OnChangeLanguage()
    {
        if (text == null) return;
     
        text.text = DataTableManager.StringTable.Get(id);
    }
    private void OnChangeLanguage(Language lang)
    {

        if (text == null) return;
        var stringTable = DataTableManager.GetStringTable(lang);
        if (stringTable == null) return;
       
        text.text = stringTable.Get(id);
    }
#endif
  
#if UNITY_EDITOR
    //일괄 변경 기능 과제 구현 ContextMenu 사용
    public void SetLanguage(Language lang)
    {
        editorLang = lang;
        OnChangeLanguage(lang);
    }  
 
    [ContextMenu("한국어")]
    private void SetKorean()
    {
        var allChange = FindObjectsByType<LocalizationText>(FindObjectsSortMode.None);
        foreach (var setlan in allChange)
        {
            setlan.SetLanguage(Language.Korean);
            
        }
    }
    [ContextMenu("영어")]
    private void SetEnglish()
    {
        var all = FindObjectsByType<LocalizationText>(FindObjectsSortMode.None);
        foreach (var setlan in all)
        {
            setlan.SetLanguage(Language.English);
        }
             
    }
    [ContextMenu("일본어")]
    private void SetJapanese()
    {
        var all = FindObjectsByType<LocalizationText>(FindObjectsSortMode.None);
        foreach (var setlan in all)
        {
            setlan.SetLanguage(Language.Japanese);
        }  
        

    }
#endif
}
