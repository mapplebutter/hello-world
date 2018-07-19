using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject retryText;
    private bool isReloading = false;
    private bool isTestMode = false;

    public void ResumeGame()
    {
        if(isPaused && !isReloading)
        {
            isPaused = false;
            pauseMenu.SetActive(false);
        }
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(SceneManager.GetSceneByName("TitleScreen").buildIndex);
        CloseCanvas();
    }

    public void RetryLevel()
    {
        if (isPaused && !isReloading && !GameManager.instance.isInitializing)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("TitleScreen"));
            isReloading = true;
            retryText.GetComponent<Text>().text = "Loading...";
            isTestMode = (GameManager.instance.state == GameManager.GameState.Test) ? true : false;
            GameManager.instance.UpdateState(GameManager.GameState.Loading);
            Invoke("ReloadLevel", 2.0f);
        }
            CloseCanvas();
    }


    public void EndGame()
    {
        if (InitialObjectSpawning.instance != null)
        {
            if (TitleMenu.instance.gameMode == TitleMenu.Mode.Test)
                InitialObjectSpawning.instance.EndGame();
            else if (TitleMenu.instance.gameMode == TitleMenu.Mode.Guided)
                ExitGame();
        }
        CloseCanvas();
    }

    void CloseCanvas()
    {
        gameObject.SetActive(false);
    }

    private void ReloadLevel()
    {
        SceneManager.UnloadSceneAsync(TitleMenu.instance.GetGameScene());

        foreach(GameObject x in InitialObjectSpawning.instance.spawnObjects)
        {
            Destroy(x);
        }

        retryText.GetComponent<Text>().text = "Retry";
        pauseMenu.SetActive(false);
        isReloading = false;
        isPaused = false;
        if (isTestMode)
        {
            GameManager.instance.UpdateState(GameManager.GameState.Test);
        }
        else
        {
            GameManager.instance.UpdateState(GameManager.GameState.Guided);
        }
        
    }
}
