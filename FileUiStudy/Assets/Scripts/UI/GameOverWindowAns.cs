using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;
using System.Collections;

public class GameOverWindowAns : GenericWindow
{
    public TextMeshProUGUI leftStatLevel;
    public TextMeshProUGUI leftStatValue;
    public TextMeshProUGUI rightStatLevel;
    public TextMeshProUGUI rightStatValue;
    public TextMeshProUGUI scoreValue;

    public Button nextButton;
 
    //선형 보간을 사용해보라는 피드백이 있었기에 우선 참고해야 함.
    public const int totalStats = 6;//총 스탯 수. 일단 4개로 가정.
    private const int statsPerColumn = 3; // 한 열에 표시할 스탯 수. 3으로 가정.

    public float statsDelay = 0.5f; // 스탯이 하나씩 나타나는 간격.
    public float scoreDuration = 2f; // 점수가 증가하는데 걸리는 시간.   

    public int[] statRolls =  new int[totalStats];
    private int finalScore;
    private TextMeshProUGUI[] statsLabels;
    private TextMeshProUGUI[] statsValues;
    
    private Coroutine routine;

    private void Awake()
    {
        statsLabels = new TextMeshProUGUI[] { leftStatLevel, rightStatLevel };
        statsValues = new TextMeshProUGUI[] { leftStatValue, rightStatValue };
        nextButton.onClick.AddListener(OnNext);

    }
    public override void Open()
    {
        if(routine != null)
        {
            StopCoroutine(routine);
            routine = null;   
        }
        base.Open();
        ResetStats();
        routine = StartCoroutine(CoplayGameOverRoutine());
    }

    public override void Close()
    {
        if(routine != null)
        {
            StopCoroutine(routine);
            routine = null;   
        }
            base.Close();
    }

    public void OnNext()
    {
        windowManager.Open(0);
    }

   

    private void ResetStats()
    {
        for(int i = 0; i < statRolls.Length; i++)
        {
            statRolls[i] = Random.Range(0, 1000);
        }
        finalScore = Random.Range(0, 100000);

        for (int i = 0;i < statsLabels.Length; i++)
        {
           
            statsLabels[i].text = string.Empty;
            statsValues[i].text = string.Empty;
        }
        scoreValue.text = $"{0:D9}";
    }

    private IEnumerator CoplayGameOverRoutine()
    {
        for(int i = 0; i < totalStats; i++)
        {
            yield return new WaitForSeconds(statsDelay);
            int column = i / statsPerColumn;
            var labelText = statsLabels[column];
            var valueText = statsValues[column];   

            string newline = i % statsPerColumn == 0 ? string.Empty : "\n";
            labelText.text = $"{labelText.text}{newline} Stat {(i % 3) + 1}";
            //valueText.text = $"{valueText.text}{newline} {statRolls[i]:D4}";

            string baseValue = valueText.text;

             
            float flowT = 0f;
            while (flowT < 1f)
            {
                flowT += Time.deltaTime / scoreDuration;
                int currentscore = Mathf.FloorToInt(Mathf.Lerp(0, statRolls[i], flowT));
                valueText.text = $"{baseValue}{newline}{currentscore:D4}";
                yield return null;
            }
            // 확정값 고정
            valueText.text = $"{baseValue}{newline}{statRolls[i]:D4}";
        }

        int current = 0;
        float t = 0f;
        while(t < 1f)
        {
            t += Time.deltaTime / scoreDuration;
            current = Mathf.FloorToInt(Mathf.Lerp(0, finalScore, t));
            scoreValue.text = $"{current:D9}";
            yield return null;
        }
        scoreValue.text = $"{finalScore:D9}";
        routine = null;
    }
}
