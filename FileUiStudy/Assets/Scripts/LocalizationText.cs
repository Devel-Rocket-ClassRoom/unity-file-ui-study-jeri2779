using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class LocalizationText : MonoBehaviour
{
    //public Language language;

    //public StringTableText text;
    //public StringTable stringTable;



    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Q)) ChangeLanguage(Language.Korean, "Hello");
    //    if(Input.GetKeyDown(KeyCode.A)) ChangeLanguage(Language.Korean, "Bye");
    //    if(Input.GetKeyDown(KeyCode.Z)) ChangeLanguage(Language.Korean, "YouDie");

    //    if (Input.GetKeyDown(KeyCode.W)) ChangeLanguage(Language.English, "Hello");
    //    if(Input.GetKeyDown(KeyCode.S)) ChangeLanguage(Language.English, "Bye");
    //    if(Input.GetKeyDown(KeyCode.X)) ChangeLanguage(Language.English, "YouDie");

    //    if (Input.GetKeyDown(KeyCode.E)) ChangeLanguage(Language.Japanese, "Hello");
    //    if(Input.GetKeyDown(KeyCode.D)) ChangeLanguage(Language.Japanese, "Bye");
    //    if(Input.GetKeyDown(KeyCode.C)) ChangeLanguage(Language.Japanese, "YouDie");


    //}

    //private void ChangeLanguage(Language lang, string key)
    //{
    //    Variables.language = lang;
    //    string result = DataTableManager.StringTable.Get(key);
    //    text.text.text = result;
    //}
#if UNITY_EDITOR
    public Language editorLang;
#endif
    public string id;
    public TextMeshProUGUI text;

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
        if(Application.isPlaying)
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

    // Variables.OnLanguageChanged 이벤트 핸들러 — 런타임 언어 변경 시 자동 호출
    private void OnLanguageChanged()
    {
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
        UnityEditor.EditorApplication.QueuePlayerLoopUpdate();
    }
#endif
    public static void ChangeLanguage(Language prevLang, Language nextLang)
    {
        Variables.language = nextLang;


    }

}
