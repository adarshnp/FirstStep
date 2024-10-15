using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootStrapper : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameLevel;

    private void Start()
    {
        gameLevel.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OpenGameScene()
    {
        mainMenu.SetActive(false);
        gameLevel.SetActive(true);
    }
}
