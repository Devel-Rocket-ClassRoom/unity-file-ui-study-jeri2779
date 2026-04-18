using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Newtonsoft.Json;
using System; 

public class DifficultyWindow : GenericWindow
{
    public Toggle[] toggles;
    public Button[] buttons;

    public int selected;

    private class DifficultySettings
    {
        public string selectedDifficulty;
    }

    private string[] difficultyLevels = { "Easy", "Normal", "Hard" };
    private string fileName = "difficulty_settings.json"; // 난이도 설정을 저장할 파일 이름


    private void Awake()
    {
        toggles[0].onValueChanged.AddListener(OnEasy);
        toggles[1].onValueChanged.AddListener(OnNormal);
        toggles[2].onValueChanged.AddListener(OnHard);

        buttons[0].onClick.AddListener(OnCancel);
        buttons[1].onClick.AddListener(OnApply);
    }

    public override void Open()
    {
        base.Open();
        selected = Array.IndexOf(difficultyLevels, 
                    OptionManager.optionData.difficulty);
        if (selected < 0) selected = 0;

        toggles[selected].isOn = true;
        
    }
    public override void Close()
    {
        base.Close();
        toggles[selected].isOn = false;
    }
    public void OnEasy(bool active)
    {
        if (active)
        {
             
            selected = 0;
            difficultyLevels[selected] = "Easy";
        }
    }
    public void OnNormal(bool active)
    {
        if (active)
        {
            
            selected = 1;
            difficultyLevels[selected] = "Normal";
        }
    }
    public void OnHard(bool active)
    {
        if (active)
        {
           
            selected = 2;
            difficultyLevels[selected] = "Hard";
        }
    }
    public void OnCancel()
    {
        windowManager.Open(0);

    }
    public void OnApply()
    {
        //apply버튼을 눌렀다면 선택한 난이도가 json형태로 저장되어야 함.

        OptionManager.optionData.difficulty = difficultyLevels[selected];
        OptionManager.SaveOptions();
        Debug.Log($"Difficulty saved: {difficultyLevels[selected]}");
        windowManager.Open(1);
         
    }

    private void SaveDifficultySettings()
    {
        DifficultySettings settings = new DifficultySettings
        {
            selectedDifficulty = difficultyLevels[selected]
        };
        string json = JsonConvert.SerializeObject(settings);
        System.IO.File.WriteAllText(System.IO.Path.Combine(Application.persistentDataPath, fileName), json);
    }
}

     
//todo
//난이도 선택후 저장 
