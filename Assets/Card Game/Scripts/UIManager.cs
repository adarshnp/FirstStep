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
        GameManager.instance.onMatchWin += OpenMatchCompletionUI;
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
    #endregion
    #region MatchCompletion
    public GameObject matchCompletionUI;
    public GameObject gameBoard;
    private void OpenMatchCompletionUI()
    {
        gameBoard.SetActive(false);
        matchCompletionUI.SetActive(true);
    }
    #endregion
}
