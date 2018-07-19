using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreDisplay : MonoBehaviour
{

    public static HighscoreDisplay instance;

    GameObject highscoreUI;
    GameObject[] entries = new GameObject[5];


    List<Highscore> ListHighscores = new List<Highscore>();

    private void Awake()
    {
        instance = this;
    }

    void OnEnable()
    {
        HighscoreUI();
    }

    void OnDisable()
    {
        ListHighscores.Clear();
    }

    void InitHighscore() //Initiate the variable to assign to the UI
    {
        highscoreUI = this.gameObject;
        if (highscoreUI != null)
        {
            entries[0] = highscoreUI.transform.GetChild(1).transform.gameObject;
            entries[1] = highscoreUI.transform.GetChild(2).transform.gameObject;
            entries[2] = highscoreUI.transform.GetChild(3).transform.gameObject;
            entries[3] = highscoreUI.transform.GetChild(4).transform.gameObject;
            entries[4] = highscoreUI.transform.GetChild(5).transform.gameObject;
        }
    }

    void GetPreviousScores() //Get the data of the highscoreName, time Score from GameManager and put inside a List of Highscore
    {
        for (int i = 0; i < GameManager.instance.highscoreNames.Count; i++)
        {
            Highscore x = new Highscore(GameManager.instance.highscoreNames[i], GameManager.instance.highscoreTimes[i], GameManager.instance.highscoreScores[i]);
            ListHighscores.Add(x);
        }
    }

    static int SortByScore(Highscore p1, Highscore p2)
    {
        return p1.score.CompareTo(p2.score);
    }

    void Sort() //Sort the list based on the value of the score
    {
        ListHighscores.Sort(SortByScore);
        ListHighscores.Reverse();
    }

    void DisplayHighScore() //Assign the value of the list into the Highscore GameObject UI
    {
        for (int i = 0; i < 5; i++)
        {
            if (ListHighscores.Count > i)
            {
                entries[i].transform.GetChild(1).transform.GetChild(0).transform.GetComponent<Text>().text = ListHighscores[i].name;
                entries[i].transform.GetChild(2).transform.GetChild(0).transform.GetComponent<Text>().text = ListHighscores[i].time.ToString("F2").Replace('.', ':') + "s";
                entries[i].transform.GetChild(3).transform.GetChild(0).transform.GetComponent<Text>().text = ListHighscores[i].score.ToString();
            }
            else
            {
                entries[i].transform.GetChild(1).transform.GetChild(0).transform.GetComponent<Text>().text = "";
                entries[i].transform.GetChild(2).transform.GetChild(0).transform.GetComponent<Text>().text = "";
                entries[i].transform.GetChild(3).transform.GetChild(0).transform.GetComponent<Text>().text = "";
            }
        }
    }

    public void HighscoreUI() //Call this function for all to work
    {
        InitHighscore();
        GetPreviousScores();
        Sort();
        DisplayHighScore();
    }

    class Highscore //Custom class for Highscore to make easier for sorting
    {
        public string name;
        public float time;
        public int score;

        public Highscore(string name, float time, int score)
        {
            this.name = name;
            this.time = time;
            this.score = score;
        }
    }
}
