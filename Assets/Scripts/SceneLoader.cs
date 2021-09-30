using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float deathDelayTimer = 0.5f;

    public void LoadMainGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void GameOverScreen()
    {
        StartCoroutine(GameOverScreenCountDown());
    }

    IEnumerator GameOverScreenCountDown()
    {
        yield return new WaitForSeconds(deathDelayTimer);
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
