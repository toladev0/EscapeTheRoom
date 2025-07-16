using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManagement : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }

    public void NewGames()
    {
        SceneManager.LoadScene("Level1");
    }

    public void SelectLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
