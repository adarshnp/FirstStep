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

    public event Action<int> onTurnUpdate;//update UI for turn counter
    public event Action<int> onMatchesUpdate;//update UI for match counter
    public event Action onMatchWin; // enable matchSuccess UI and disable game board UI 
    public event Action onGameSessionStart; //enable game level UI and disable main menu UI
    public event Action onNextLevel; // disable match complaetion UI and enable game board UI
    public event Action<int,int> onGridGeneration; // generate card layout for current level

    public static GameManager instance;

    public CardLayoutData cardLayoutData;
    private int currentLevelIndex = 0;
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
        currentLevelIndex = 0;
        LoadLevel(currentLevelIndex);
    }
    
    //handle level progression
    public void NextLevel()
    {
        currentLevelIndex++;
        onNextLevel.Invoke();
        LoadLevel(currentLevelIndex);
    }
    public void LoadLevel(int levelIndex)
    {
        if (levelIndex >= cardLayoutData.layouts.Length)
        {
            Debug.Log("No more levels available.");
            return;
        }

        var layout = cardLayoutData.layouts[levelIndex];

        matches = 0;
        turns = 0;

        onGridGeneration.Invoke(layout.rows, layout.columns);
    }
    public bool IsLastLevel()
    {
        if (currentLevelIndex == cardLayoutData.layouts.Length - 1)
        {
            return true;
        }
        return false;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
