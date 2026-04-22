using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq.Expressions;
using System.Collections;
public class GameOverWindow : GenericWindow
{
    public TextMeshProUGUI leftStatLabel;
    public TextMeshProUGUI leftStatValue;
    public TextMeshProUGUI rightStatLabel;
    public TextMeshProUGUI rightStatValue;
    public TextMeshProUGUI scoreValue;

    public Button nextButton;

    public float statsDelay = 1f;
    public float scoreDuration = 2f;

    private const int totalStats = 6;
    private const int statsPerColumn = 3;

    private int[] statsRolls = new int[totalStats];
    private int finalScore;

    private TextMeshProUGUI[] statsLabels;
    private TextMeshProUGUI[] statsValues;

    private Coroutine routine;

    private void Awake()
    {
        statsLabels = new TextMeshProUGUI[] {leftStatLabel, rightStatLabel};
        statsValues = new TextMeshProUGUI[] {leftStatValue, rightStatValue};

        nextButton.onClick.AddListener(OnNext);
    }

    public override void Open()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
        base.Open();
        ResetStats();
        routine = StartCoroutine(CoPlayGameOverRoutine());
    }

    public override void Close()
    {
        if (routine != null)
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
        for (int i = 0; i < totalStats; ++i)
        {
            statsRolls[i] = Random.Range(0, 1000);
        }
        finalScore = Random.Range(0, 100000000);

        for (int i = 0; i < statsLabels.Length; ++i)
        {
            statsLabels[i].text = string.Empty;
            statsValues[i].text = string.Empty;
        }

        scoreValue.text = $"{0:D9}";
    }

    private IEnumerator CoPlayGameOverRoutine()
    {
        for (int i = 0 ; i < totalStats; ++i)
        {
            yield return new WaitForSeconds(statsDelay);

            int column = i / statsPerColumn;
            var labelText = statsLabels[column];
            var valueText = statsValues[column];
            string newline = (i % statsPerColumn == 0)  ? string.Empty : "\n";
            labelText.text = $"{labelText.text}{newline}Stat {i}";
            valueText.text = $"{valueText.text}{newline}{statsRolls[i]:D4}";
        }

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / scoreDuration;
            int current = Mathf.FloorToInt(Mathf.Lerp(0, finalScore, t));
            scoreValue.text = $"{current:D9}";
            yield return null;
        }
        
        scoreValue.text = $"{finalScore:D9}";
        routine = null;
    }
}
