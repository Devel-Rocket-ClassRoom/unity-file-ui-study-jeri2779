using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class KeyboardWindow : GenericWindow
{
    /*
     *  1. 키 버튼 누르면 텍스트에 글자 추가
        2. 글자수 제한 (최대 N자)
        3. DELETE 누르면 마지막 글자 삭제
        4. ACCEPT 누르면 다음 패널로 이동
        5. CANCEL 누르면 이전 패널로 이동
        6. Header의 _텍스트는 입력 가능시 깜빡이는 연출 구현.

     */

    public TextMeshProUGUI inputText;
 
    public const int maxInputLength = 10;
    private bool showCursor = false;
    public Button[] keyButtons;
    public Button[] inputButton;


    private string currentInput = string.Empty;

    private void Awake()
    {
        foreach (var button in keyButtons)
        {
            string key  = button.GetComponentInChildren<TextMeshProUGUI>().text;
            button.onClick.AddListener(() => OnKeyPress(key));
        }
            inputButton[0].onClick.AddListener(OnCancelButton);
            inputButton[1].onClick.AddListener(OnDelete);
            inputButton[2].onClick.AddListener(OnAccept);
        
    }

    public override void Open()
    {
        base.Open();
        currentInput = string.Empty;
        inputText.text = string.Empty;
        StartCoroutine(BlinkCursor());
    }
    public override void Close()
    {
        StopAllCoroutines();
        base.Close();
    }
    public void OnNext()
    {
        windowManager.Open(1);
    }

    public void OnCancel()
    {
        windowManager.Open(0);
    }

    
    public void OnKeyPress(string key)
    {
        if(currentInput.Length >= maxInputLength)
        {
            return;
        }
        currentInput += key;
        inputText.text = currentInput;  
    }
    public void InputKey()
    {

    }
    public void OnDelete()
    {
        if(currentInput != string.Empty)
        {
            currentInput = currentInput.Substring(0, currentInput.Length - 1);
            Display();
        }
    }
    public void OnAccept()
    { 
        OnNext();
    }
     public void OnCancelButton()
    {
        OnCancel();
    }
    public bool OnKeyDown(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            return true;
        }
        return false;
    }
    private void Display()
    {
        inputText.text = currentInput + (showCursor ? "_" : "");
    }
    private IEnumerator BlinkCursor()
    {
        while (true)
        {
            showCursor = !showCursor;
            yield return new WaitForSeconds(0.5f); // 깜빡이는 간격
            Display();
        }
    }
}
