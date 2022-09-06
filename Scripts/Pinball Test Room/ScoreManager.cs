using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lcdScreen;
    private int scoreIncrease = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreIncrease > 0)
            UpdateScore();
        //timer control
    }

    private void UpdateScore()
    {
        lcdScreen.GetComponent<ScoreDisplay>().UpdateScore(scoreIncrease);
        scoreIncrease = 0;
    }
    
    public void Invoke(int scoreInput)
    {
        scoreIncrease = scoreInput;
    }

}
