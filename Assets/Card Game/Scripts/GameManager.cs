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

    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        matches = 0;
        turns = 0;
    }

    //track moves count
    public void IncrementTurns()
    {
        turns++;
    }

    //track matches count
    public void IncrementMatches()
    {
        matches++;
    }

    //handle end game

    //handle restart game
}
