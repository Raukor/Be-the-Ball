using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public List<GameObject> lcdDigits = new List<GameObject>();
    private int totalScore = 0;
    private int runningScoreBreakdown;
    private int digitHolder;
    void Start()
    {
        
    }

    
    void Update()
    {
        
        runningScoreBreakdown = totalScore;
        if (totalScore >= 10000000)
        {
            digitHolder = runningScoreBreakdown / 10000000;
            lcdDigits[0].GetComponent<ScoreDigitControl>().inputNumber = digitHolder;
            runningScoreBreakdown = runningScoreBreakdown - (digitHolder * 10000000);
        }
        else
        {
            lcdDigits[0].GetComponent<ScoreDigitControl>().inputNumber = 0;
        }

        if (totalScore >= 1000000)
        {
            digitHolder = runningScoreBreakdown / 1000000;
            lcdDigits[1].GetComponent<ScoreDigitControl>().inputNumber = digitHolder;
            runningScoreBreakdown = runningScoreBreakdown - (digitHolder * 1000000);
        }
        else
        {
            lcdDigits[1].GetComponent<ScoreDigitControl>().inputNumber = 0;
        }

        if (totalScore >= 100000)
        {
            digitHolder = runningScoreBreakdown / 100000;
            lcdDigits[2].GetComponent<ScoreDigitControl>().inputNumber = digitHolder;
            runningScoreBreakdown = runningScoreBreakdown - (digitHolder * 100000);
        }
        else
        {
            lcdDigits[2].GetComponent<ScoreDigitControl>().inputNumber = 0;
        }

        if (totalScore >= 10000)
        {
            digitHolder = runningScoreBreakdown / 10000;
            lcdDigits[3].GetComponent<ScoreDigitControl>().inputNumber = digitHolder;
            runningScoreBreakdown = runningScoreBreakdown - (digitHolder * 10000);
        }
        else
        {
            lcdDigits[3].GetComponent<ScoreDigitControl>().inputNumber = 0;
        }

        if (totalScore >= 1000)
        {
            digitHolder = runningScoreBreakdown / 1000;
            lcdDigits[4].GetComponent<ScoreDigitControl>().inputNumber = digitHolder;
            runningScoreBreakdown = runningScoreBreakdown - (digitHolder * 1000);
        }
        else
        {
            lcdDigits[4].GetComponent<ScoreDigitControl>().inputNumber = 0;
        }

        if (totalScore >= 100)
        {
            digitHolder = runningScoreBreakdown / 100;
            lcdDigits[5].GetComponent<ScoreDigitControl>().inputNumber = digitHolder;
            runningScoreBreakdown = runningScoreBreakdown - (digitHolder * 000);
        }
        else
        {
            lcdDigits[5].GetComponent<ScoreDigitControl>().inputNumber = 0;
        }

        if (totalScore >= 10)
        {
            digitHolder = runningScoreBreakdown / 10;
            lcdDigits[6].GetComponent<ScoreDigitControl>().inputNumber = digitHolder;
            runningScoreBreakdown = runningScoreBreakdown - (digitHolder * 10);
        }
        else
        {
            lcdDigits[6].GetComponent<ScoreDigitControl>().inputNumber = 0;
        }

        lcdDigits[7].GetComponent<ScoreDigitControl>().inputNumber = runningScoreBreakdown;
        


    }

    public void UpdateScore(int score)
    {
        totalScore += score;
    }

    public void ResetScore()
    {
        totalScore = 0;
    }
}
