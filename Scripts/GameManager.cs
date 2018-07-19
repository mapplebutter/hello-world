using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using VRTK;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Loading,
        Title,
        Test,
        Guided,
        End,
        Reset
    }

    public bool loadOnce = true;
    public VRTK_BodyPhysics bodyReference; //the reference to the VRTK_BodyPhysics script in the game
    public static GameManager instance = null;
    public string playerName = "";
    public float prepTimeTaken = 0.0f;
    public float timeTaken = 0.0f;
    public Text timer;
    public GameState state;
    public bool isInitializing = false; //checks if the game is still initializing the objects for loading

    public bool isMakingATrip = false; //checks if the player is making a trip from the long table to the dinner table
    private int tripCount = 0;
    private bool prepTimerOn = false; //checks if the preparation timer has begun counting down
    private bool testTimerOn = false; //checks if the test timer has begun counting down

    public Transform[] objectRespawnZones = new Transform[6];
    public GameObject assessmentCanvas, highscoreCanvas;

    public int TripCount
    {
        get { return tripCount; }
        set { tripCount = value; }
    }

    public int getDurationScore()
    {
        int actualScore = 0;
        if(timeTaken<=180.0f)
        {
            actualScore = 7;
        }
        else if(timeTaken>=181.0f && timeTaken <=150.0f)
        {
            actualScore = 6;
        }
        else if(timeTaken>=151.0f && timeTaken <=240.0f)
        {
            actualScore = 5;
        }
        else if(timeTaken >= 241.0f && timeTaken <= 270.0f)
        {
            actualScore = 4;
        }
        else if(timeTaken >= 271.0f && timeTaken <= 300.0f)
        {
            actualScore = 3;
        }
        else if (timeTaken >= 301.0f && timeTaken <= 330.0f)
        {
            actualScore = 2;
        }
        else if (timeTaken >= 331.0f && timeTaken <= 360.0f)
        {
            actualScore = 1;
        }
        else
        {
            actualScore = 0;
        }
        return actualScore;
    }

    //For Spawning

    public List<string> highscoreNames = new List<string>();
    public List<float> highscoreTimes = new List<float>();
    public List<int> highscoreScores = new List<int>();
    // Use this for initialization
    void Awake()
    {
        if (instance != this)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        UpdateState(GameState.Loading);
    }

    // Update is called once per frame
    void Update()
    {
        if(prepTimerOn)
        {
            prepTimeTaken += Time.deltaTime;
        }

        if(testTimerOn)
        {
            timeTaken += Time.deltaTime;
        }
        if(Input.GetKeyDown("a") && loadOnce == true)
        {
            loadOnce = false;
            TitleMenu.instance.LoadSceneAdd();
            testTimerOn = true;
        }
    }

    //starts the preparation timer
    public void StartPrepTimer()
    {
        prepTimerOn = true;
    }

    //starts the test timer
    public void StartTestTimer()
    {
        prepTimerOn = false;
        testTimerOn = true;
    }

    void Reset()
    {
        playerName = "";
        timeTaken = 0.0f;
    }
    
    public void RunEndGame()
    {
        if(TitleMenu.instance.gameMode == TitleMenu.Mode.Guided)
        {
            SceneManager.LoadScene(SceneManager.GetSceneByName("TitleScreen").buildIndex);
        }
        else
        {
        ScoreCheck.instance.StartChecking();
        Invoke("EndGameCalculations", 1.0f);
        }
    }   

    void EndGameCalculations()
    {
        assessmentCanvas.SetActive(true);
        GameSaveLoad.SaveGame();
        highscoreNames.Add(playerName);
        highscoreTimes.Add(timeTaken);
        highscoreScores.Add(ScoreAssessment.instance.GetTotalScore());

        Invoke("DisplayHighScore", 10.0f);
    }

    void DisplayHighScore()
    {
        assessmentCanvas.SetActive(false);
        highscoreCanvas.SetActive(true);
        Invoke("ReloadScene", 15.0f);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void StartTestMode()
    {
        TitleMenu.instance.LoadSceneAdd();
        isInitializing = false;
        //Invoke("AssignListener", 2.0f);
    }

    public void StartGuidedMode()
    {
        TitleMenu.instance.LoadSceneAdd();
        //Invoke("AssignListener", 2.0f);
    }
    public void UpdateState(GameState newState)
    {
        state = newState;
        switch (state)
        {
            case GameState.Loading:
                GameSaveLoad.LoadScore();
                break;
            case GameState.Title:
                break;
            case GameState.Test:
                isInitializing = true;
                Invoke("StartTestMode", 2.0f);
                break;
            case GameState.Guided:
                Invoke("StartGuidedMode", 2.0f);
                break;
        }
    }
}
public enum ObjectLayer
{
    Ungrabbable = 9,
    Penalty = 8,
    Heavy = 10,
    Tray = 11,
    Utensil = 12,
    Room = 15
}

