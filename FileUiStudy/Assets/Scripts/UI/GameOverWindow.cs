using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameOverWindow : GenericWindow
{
    public TextMeshProUGUI leftStatLevel;
    public TextMeshProUGUI leftStatValue;

    public TextMeshProUGUI rightStatLevel;
    public TextMeshProUGUI rightStatValue;

    public TextMeshProUGUI scoreValue;

    public Button nextButton;

    public float minScore = 0f;
    public float maxScore = 1000f;
    public float currentScore;   // 점수가 증가하는걸 보여주는 함수에서 사용할 변수.
    public float targetScore;    // targetScore는 우선 랜덤으로 부여.
    public float scoreduration = 2f; // 점수가 증가하는데 걸리는 시간.

    public float totalScore; // 최종 점수. 0부터 증가하는 식으로.

    private const int lineText = 3;
    private TextMeshProUGUI[] statLevels;
    private TextMeshProUGUI[] statValues;

    private void Awake()
    {
        statLevels = new TextMeshProUGUI[] { leftStatLevel, rightStatLevel };
        statValues = new TextMeshProUGUI[] { leftStatValue, rightStatValue };
        nextButton.onClick.AddListener(OnNext);
    }

    public override void Open()
    {
        base.Open();

        foreach (var t in statLevels) t.text = "";
        foreach (var t in statValues) t.text = "";
        scoreValue.text = "0";
        totalScore = 0f;

        StartCoroutine(SetScoring());
    }

    public override void Close()
    {
        StopAllCoroutines();
        base.Close();
    }

    public void SetScoreRange()
    {
        targetScore = Random.Range(minScore, maxScore);
    }

    private IEnumerator SetScoring()
    {
        // 왼쪽(0), 오른쪽(1) 순차 출력
        for (int col = 0; col < statLevels.Length; col++)
        {
            for (int i = 0; i < lineText; i++)
            {
                SetScoreRange();
                currentScore = 0f;
                totalScore  += targetScore;

                string newline   = i == 0 ? "" : "\n";
                string baseValue = statValues[col].text;

                statLevels[col].text += $"{newline}Stat";
                yield return StartCoroutine(IncreaseScore(statValues[col], baseValue, newline));
            }
        }

        // 합산 점수 출력
        targetScore  = totalScore;
        currentScore = 0f;
        yield return StartCoroutine(IncreaseScore());
    }

    // 스탯 값 0 → targetScore 애니메이션 (baseValue + newline 으로 이전 줄 유지)
    public IEnumerator IncreaseScore(TextMeshProUGUI tmp, string baseValue, string newline)
    {
        float elapsed = 0f;
        while (elapsed < scoreduration)
        {
            elapsed      += Time.deltaTime;
            currentScore  = Mathf.Lerp(0f, targetScore, elapsed / scoreduration);
            tmp.text = $"{baseValue}{newline}{Mathf.FloorToInt(currentScore)}";
            yield return null;
        }
        currentScore = targetScore;
        tmp.text = $"{baseValue}{newline}{Mathf.FloorToInt(targetScore)}";
    }

    // 합산 점수 0 → totalScore 애니메이션
    public IEnumerator IncreaseScore()
    {
        float elapsed = 0f;
        while (elapsed < scoreduration)
        {
            elapsed      += Time.deltaTime;
            currentScore  = Mathf.Lerp(0f, targetScore, elapsed / scoreduration);
            scoreValue.text = Mathf.FloorToInt(currentScore).ToString();
            yield return null;
        }
        currentScore = targetScore;
        scoreValue.text = Mathf.FloorToInt(targetScore).ToString();
    }

    public void OnNext()
    {
        windowManager.Open(1);
    }
}
