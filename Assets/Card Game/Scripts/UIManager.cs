using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text matchesUI;
    public TMP_Text turnsUI;

    private void Start()
    {
        GameManager.instance.onMatchesUpdate += UpdateMatchesUI;
        GameManager.instance.onTurnUpdate += UpdateTurnsUI;
    }
    private void UpdateMatchesUI(int value)
    {
        matchesUI.text = value.ToString();
    }
    private void UpdateTurnsUI(int value)
    {
        turnsUI.text = value.ToString();
    }
}
