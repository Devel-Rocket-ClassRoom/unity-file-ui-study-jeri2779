using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class KeyboardWindowAns : GenericWindow
{
    private readonly StringBuilder sb = new StringBuilder();    
    public TextMeshProUGUI inputField;
    public GameObject rootKeyboard;

    public int maxCharacters = 10;
    private float timer = 0f;
    private float cursorDelay = 0.5f;
    private bool blink;

    private void Awake()
    {
        var keys = rootKeyboard.GetComponentsInChildren<Button>();
        foreach(var button in keys)
        {
            string key = button.GetComponentInChildren<TextMeshProUGUI>().text;
            button.onClick.AddListener(() => OnKey(key));
        }   
    }
    private void Update()
    {
        
        timer += Time.deltaTime;
        if (timer >= cursorDelay)
        {
            blink = !blink;
            timer = 0f;
            UpdateField();
        }
        
    }

    public override void Open()
    {
        sb.Clear();
        sb.Append(OptionManager.optionData.name);
        timer = 0f;
        blink = false;
        base.Open();
        if (sb.Length >= maxCharacters)
            inputField.SetText(sb);                              
        else
            inputField.text = sb.ToString() + (blink ? "_" : "");  
    }
    public override void Close()
    {
         base.Close();  
    }

    public void OnKey(string key)
    {
        if (sb.Length < maxCharacters)
        {
            sb.Append(key);
            inputField.text = sb.ToString();
        }
        UpdateField();
    }
    public void OnCancel()
    {
        windowManager.Open(0);
        sb.Clear();
        UpdateField();
    }
    public void OnDelete()
    {
        if(sb.Length > 0)
        {
            sb.Length -= 1;
        }
        UpdateField();
    }
    public void OnAccept()
    {
        OptionManager.optionData.name = sb.ToString().ToUpper();
        OptionManager.SaveOptions();
        Debug.Log($"name saved: {OptionManager.optionData.name}");
        windowManager.Open(3);
    }
    private void UpdateField()
    {
        bool showCursor = sb.Length < maxCharacters && !blink;//
        if(showCursor)
        {
            sb.Append("_");
        }
        inputField.SetText(sb);
        if(showCursor)
        {
            sb.Length -= 1;
        }
    }
}
