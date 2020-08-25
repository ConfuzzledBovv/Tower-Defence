using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("HowToLevel");
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void OptionsButton()
    {
        // TODO: Add Options to the game
    }

    public void QuitButton()
    {
        Application.Quit();
        print("Game is quitting");
    }
}
