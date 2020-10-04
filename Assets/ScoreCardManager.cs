using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreCardManager : MonoBehaviour
{
    [SerializeField]
    protected LevelScoreCol templateScoreColumn;

    [SerializeField]
    protected Button nextlevelButton;

    [SerializeField]
    protected Button mainmenuButton;

    private LevelScoreCol sumColumn;

    private CanvasGroup canvasGroup;

    private List<LevelScoreCol> scoreColumns = new List<LevelScoreCol>();

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitScorecard(List<int> scores, UnityAction nextlevelCallback, UnityAction mainMenuCallback)
    {
        for (int i = 0; i < scores.Count; i++)
        {
            var column = Instantiate(templateScoreColumn, templateScoreColumn.transform.parent);
            column.gameObject.SetActive(true);
            column.level.SetText($"{i+1}");
            column.score.SetText($"{scores[i]}");
            scoreColumns.Add(column);
        }

        sumColumn = Instantiate(templateScoreColumn, templateScoreColumn.transform.parent);
        sumColumn.gameObject.SetActive(true);
        sumColumn.level.SetText("SUM");
        sumColumn.score.SetText("0");

        nextlevelButton.onClick.AddListener(nextlevelCallback);
        mainmenuButton.onClick.AddListener(mainMenuCallback);
        HideScoreCard();
    }

    public void UpdateScores(List<int> scores, bool finished = false)
    {
        var sum = 0;
        for (int i = 0; i < scores.Count; i++)
        {
            scoreColumns[i].score.SetText($"{scores[i]}");
            sum += scores[i];
        }

        sumColumn.score.SetText($"{sum}");

        var canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;

        if (finished)
        {
            nextlevelButton.gameObject.SetActive(false);
        } 
        else
        {
            nextlevelButton.interactable = true;
        }
    }

    public void HideScoreCard()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }

    public void ShowMainMenuButton()
    {
    }
}
