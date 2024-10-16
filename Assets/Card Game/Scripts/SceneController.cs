using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// SceneController class that controls execution of scripts and UI order . 
/// WIll be helpful as more scripts get added later.
/// </summary>
public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameLevel;

    private void Start()
    {
        GoToMainMenu();
    }

    public void OpenGameScene()
    {
        mainMenu.SetActive(false);
        gameLevel.SetActive(true);
    }

    public void GoToMainMenu()
    {
        gameLevel.SetActive(false);
        mainMenu.SetActive(true);
    }
}
