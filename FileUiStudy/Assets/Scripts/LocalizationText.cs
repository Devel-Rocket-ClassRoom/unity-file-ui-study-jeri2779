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

    private void Awake()
    {
         
        originId = id;
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeAllId("Hello");
        if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeAllId("Bye");
        if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeAllId("YouDie");
    }
    private void ChangeAllId(string newId)
    {
        var all = FindObjectsByType<LocalizationText>(FindObjectsSortMode.None);
        foreach (var locText in all)
        {
            locText.id = newId;  
            locText.OnChangedId();
        }
    }


    private void OnEnable()
    {
        if (Application.isPlaying)
        {
            Variables.OnLanguageChanged += OnLanguageChanged;
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
            Variables.OnLanguageChanged -= OnLanguageChanged;
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
    private void OnChangedId()
    {
        if (text == null) return;
        text.text = DataTableManager.StringTable.Get(id);
    }

     
    private void OnLanguageChanged()
    {
        id = originId;
        OnChangedId();
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
      
        UnityEditor.EditorUtility.SetDirty(this);//if유니티 에디터에서 사용되는 메서드. 변경 사항을 알리는 역할.
    }
#endif
    public void UpdateText()
    {
        OnChangedId();
    }
#if UNITY_EDITOR
    //일괄 변경 기능 과제 구현 ContextMenu 사용
    public void SetLanguage(Language lang)
    {
        editorLang = lang;
        OnChangeLanguage(lang);
    }  
 
    [ContextMenu("언어/한국어로 변경")]
    private void SetKorean()
    {
         var allChange = FindObjectsByType<LocalizationText>(FindObjectsSortMode.None);
        foreach (var setlan in allChange)
        {
            setlan.editorLang = Language.Korean;
            
        }
    }
    [ContextMenu("언어/영어로 변경")]
    private void SetEnglish()
    {
        var all = FindObjectsByType<LocalizationText>(FindObjectsSortMode.None);
        foreach (var setlan in all)
        {
            setlan.SetLanguage(Language.English);
        }
             
    }

    [ContextMenu("언어/일본어로 변경")]
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
