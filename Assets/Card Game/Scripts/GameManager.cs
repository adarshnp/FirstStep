using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
/// <summary>
/// Game Manager class that handles score and game flow
/// </summary>
public class GameManager : MonoBehaviour
{
    private int highScore;
    private int matches = 0;
    private int turns = 0;
    private int totalPairs;
    private float currentLevelMultiplier=1;
    public int currentScore = 0;
    public float scoreMultiplierIncrementPerLevel = 0.25f;
    private int CompletedLevelsScore = 0;

    public event Action<int> onTurnUpdate;//update UI for turn counter
    public event Action<int> onMatchesUpdate;//update UI for match counter
    public event Action<int> onScoreUpdate;//update UI for score
    public event Action onMatchWin; // enable matchSuccess UI and disable game board UI 
    public event Action onGameSessionStart; //enable game level UI and disable main menu UI
    public event Action onNextLevel; // disable match complaetion UI and enable game board UI
    public event Action<int, int> onGridGeneration; // generate card layout for current level
    public event Action<int, int,int> onSaveGame;
    public event Action<int> onLoadComplete;

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
        currentScore = CompletedLevelsScore + CalculateCurrentLevelScore();
        onTurnUpdate.Invoke(turns);
    }

    //track matches count
    public void IncrementMatches()
    {
        matches++;
        currentScore = CompletedLevelsScore + CalculateCurrentLevelScore();
        onMatchesUpdate.Invoke(matches);
        if (totalPairs <= matches)
        {
            WinLevel();
        }
    }

    //calculate score
    private int CalculateCurrentLevelScore()
    {
        if (turns == 0) return 0;
        currentLevelMultiplier = 1 + currentLevelIndex* scoreMultiplierIncrementPerLevel;
        float score = (float)matches / turns * currentLevelMultiplier;
        return (int)(score * 100);
    }

    //handle end game
    public void WinLevel()
    {
        CompletedLevelsScore = currentScore;
        onScoreUpdate.Invoke(CompletedLevelsScore);
        onMatchWin.Invoke();
        onSaveGame.Invoke(currentLevelIndex, currentScore, highScore);
        CompletedLevelsScore = currentScore;
        if (highScore < CompletedLevelsScore)
        {
            highScore = CompletedLevelsScore;
        }
    }

    //handle Resume level from save data
    public void ApplySavedDataToGame(SaveData data)
    {
        if(data == null)
        {
            onLoadComplete.Invoke(0);
            return;
        }
        CompletedLevelsScore = data.score;
        currentLevelIndex = data.level;
        highScore = data.highScore;
        onLoadComplete.Invoke(highScore);
    }
    public void ResumeLevel()
    {
        onGameSessionStart.Invoke();
        currentLevelIndex++;
        currentScore = 0;
        LoadLevel(currentLevelIndex);
    }

    //handle new game
    public void NewGame()
    {
        onGameSessionStart.Invoke();
        currentLevelIndex = 0;
        CompletedLevelsScore = 0;
        currentScore = 0;
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
        currentScore = CompletedLevelsScore;

        onMatchesUpdate.Invoke(0);
        onTurnUpdate.Invoke(0);
        onScoreUpdate.Invoke(CompletedLevelsScore);

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
