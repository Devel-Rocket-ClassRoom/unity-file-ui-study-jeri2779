using UnityEngine;
using UnityEngine.UI;

public class StartWindow : GenericWindow
{
    public Button continueButton;
    public Button startButton;
    public Button optionButton;

    public bool canCountinue = false;

    private void Awake()
    {
        continueButton.onClick.AddListener(OnContinue);
        startButton.onClick.AddListener(OnNewGame);
        optionButton.onClick.AddListener(OnOption);
    }
    //private void Start()
    //{
    //    Open();
    //}

    public override void Open()
    {
         
        continueButton.gameObject.SetActive(canCountinue);
        if(!canCountinue)
        {
            firstSelected = startButton.gameObject;
        }
        base.Open();
    }
    public override void Close()
    {
        base.Close();
    }
    public void OnContinue()
    {
        Debug.Log("OnContinue");
        windowManager.Open(1);
    }

    public void OnNewGame()
    {
        Debug.Log("OnNewGame");
        windowManager.Open(2);
    }
    public void OnOption()
    {
        Debug.Log("OnOption");
        windowManager.Open(3);
    }
}
