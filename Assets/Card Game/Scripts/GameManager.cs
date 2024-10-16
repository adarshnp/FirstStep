using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Game Manager class that handles score and game flow
/// </summary>
public class GameManager : MonoBehaviour
{
    private int matches = 0;
    private int turns = 0;
    private int totalPairs;

    public event Action<int> onTurnUpdate;
    public event Action<int> onMatchesUpdate;
    public event Action onMatchWin;
    public event Action onGameSessionStart;

    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    public void SetTotalPairsCount(int value)
    {
        totalPairs = value;
    }

    //track moves count
    public void IncrementTurns()
    {
        turns++;
        onTurnUpdate.Invoke(turns);
    }

    //track matches count
    public void IncrementMatches()
    {
        matches++;
        onMatchesUpdate.Invoke(matches);
        if (totalPairs <= matches)
        {
            WinGame();
        }
    }

    //handle end game
    public void WinGame()
    {
        onMatchWin.Invoke();
    }

    //handle restart game

    //handle new game
    public void NewGame()
    {
        onGameSessionStart.Invoke();
        matches = 0;
        turns = 0;
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
