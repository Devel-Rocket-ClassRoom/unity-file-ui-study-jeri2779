public enum Language
{
    Korean,
    English,
    Japanese
}

public static class Variables
{
    public static event System.Action OnLanguageChanged;
    private static Language languages = Language.Korean;

    public static Language language
    {
        get => languages;
        set
        {
            if (languages == value) return;
            languages = value;
            DataTableManager.ChangeLanguage(languages);
            OnLanguageChanged?.Invoke();
        }
    }
}

public static class DataTableIds
{
    public static readonly string[] StringTableIds = new string[]
    {
        "StringTableKr",
        "StringTableEn",
        "StringTableJp"
    };
    public static string String => StringTableIds[(int)Variables.language];
}
