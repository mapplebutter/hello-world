using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAssessment : MonoBehaviour
{
    public static ScoreAssessment instance = null;

    public Text TripTakenScore, CorrectUtensilScore, ProperHandlingScore, PositioningScore, TableContactScore, DurationScoreText, totalTime, totalScore;
    public int ContactScore = 0, DurationScore = 0;

    void Awake()
    {
        if(instance ==null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        TripTakenScore.text = GameManager.instance.TripCount.ToString() +"/2";
        CorrectUtensilScore.text = CorrectUtensilChecker.instance.Count().ToString() +"/4";
        ProperHandlingScore.text = ScoreCheck.instance.getPenalty().ToString() +"/8";
        PositioningScore.text = ScoreCheck.instance.getPositioning().ToString() + "/12";
        TableContactScore.text = TableCollideCheck.instance.getContactPenalty.ToString() + "/2";
        DurationScoreText.text = GameManager.instance.getDurationScore().ToString() + "/7";

        totalTime.text ="Time: " +  GameManager.instance.timeTaken.ToString();
        totalScore.text = "Score: " + GetTotalScore();
    }

    public int GetTotalScore()
    {
        int score = 0;
        score = GameManager.instance.TripCount + CorrectUtensilChecker.instance.Count() + ScoreCheck.instance.getPenalty() + ScoreCheck.instance.getPositioning() + TableCollideCheck.instance.getContactPenalty + GameManager.instance.getDurationScore();
        return score;
    }
}
