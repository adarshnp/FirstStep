using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.onMatchesUpdate += UpdateMatchesUI;
        GameManager.instance.onTurnUpdate += UpdateTurnsUI;
        GameManager.instance.onScoreUpdate += UpdateLevelScoreUI;
        GameManager.instance.onMatchWin += OpenMatchCompletionUI;
        GameManager.instance.onNextLevel += CloseMatchCompletionUI;
    }

    #region GameScore

    public TMP_Text matchesUI;
    private void UpdateMatchesUI(int value)
    {
        matchesUI.text = value.ToString();
    }

    public TMP_Text turnsUI;
    private void UpdateTurnsUI(int value)
    {
        turnsUI.text = value.ToString();
    }

    public TMP_Text levelScoreUI;
    private void UpdateLevelScoreUI(int value)
    {
        levelScoreUI.text = value.ToString();
    }
    #endregion


    #region MatchCompletion

    public GameObject matchCompletionUI;
    public GameObject gameBoard;
    public GameObject nextLevelButton;
    public GameObject scoreTextUI;
    public TMP_Text scoreUI;
    private void OpenMatchCompletionUI()
    {
        gameBoard.SetActive(false);
        matchCompletionUI.SetActive(true);
        scoreUI.text = GameManager.instance.currentScore.ToString();
        if (GameManager.instance.IsLastLevel())
        {
            nextLevelButton.SetActive(false);
        }
        else
        {
            nextLevelButton.SetActive(true);
        }
    }
    private void CloseMatchCompletionUI()
    {
        gameBoard.SetActive(true);
        matchCompletionUI.SetActive(false);
    }
    #endregion

}
