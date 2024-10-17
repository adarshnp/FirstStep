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
        GameManager.instance.onHighScoreUpdate += UpdateHighScoreUI;

        GameManager.instance.onMatchWin += OpenMatchCompletionUI;
        GameManager.instance.onNextLevel += CloseMatchCompletionUI;
        GameManager.instance.onGameSessionStart += CloseMatchCompletionUI;
        GameManager.instance.onEnterMainMenu += ToggleContinueButton;
    }

    #region GameScore

    [SerializeField] private TMP_Text matchesUI;
    private void UpdateMatchesUI(int value)
    {
        matchesUI.text = value.ToString();
    }

    [SerializeField] private TMP_Text turnsUI;
    private void UpdateTurnsUI(int value)
    {
        turnsUI.text = value.ToString();
    }

    [SerializeField] private TMP_Text levelScoreUI;
    private void UpdateLevelScoreUI(int value)
    {
        levelScoreUI.text = value.ToString();
    }

    [SerializeField] private TMP_Text highScoreUI;
    private void UpdateHighScoreUI(int value)
    {
        highScoreUI.text = value.ToString();
    }
    #endregion

    #region MatchCompletion

    [SerializeField] private GameObject matchCompletionUI;
    [SerializeField] private GameObject gameBoard;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private GameObject scoreTextUI;
    [SerializeField] private TMP_Text scoreUI;
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

    #region MAIN_MENU
    [SerializeField] private GameObject continueButton;
    private void ToggleContinueButton()
    {
        if (GameManager.instance.IsLastLevel())
        {
            continueButton.SetActive(false);
        }
        else
        {
            Debug.Log("continue button enabled");
            continueButton.SetActive(true);
        }
    }
    #endregion
}
