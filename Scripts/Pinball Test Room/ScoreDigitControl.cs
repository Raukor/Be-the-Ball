using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDigitControl : MonoBehaviour
{
    // Start is called before the first frame update
    public int inputNumber = 0;
    public List<GameObject> lcdBlocks = new List<GameObject>();
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (inputNumber)
        {
            case 0:
                SetZero();
                break;
            case 1:
                SetOne();
                break;
            case 2:
                SetTwo();
                break;
            case 3:
                SetThree();
                break;
            case 4:
                SetFour();
                break;
            case 5:
                SetFive();
                break;
            case 6:
                SetSix();
                break;
            case 7:
                SetSeven();
                break;
            case 8:
                SetEight();
                break;
            case 9:
                SetNine();
                break;
            default:
                Debug.Log("LCD DIGIT OUT OF RANGE");
                break;

        }     

    }

    private void SetZero()
    {
        lcdBlocks[0].SetActive(true);
        lcdBlocks[1].SetActive(true);
        lcdBlocks[2].SetActive(true);

        lcdBlocks[3].SetActive(true);
        lcdBlocks[4].SetActive(true);

        lcdBlocks[5].SetActive(true);
        lcdBlocks[6].SetActive(false);
        lcdBlocks[7].SetActive(true);

        lcdBlocks[8].SetActive(true);
        lcdBlocks[9].SetActive(true);

        lcdBlocks[10].SetActive(true);
        lcdBlocks[11].SetActive(true);
        lcdBlocks[12].SetActive(true);
    }
    private void SetOne()
    {
        lcdBlocks[0].SetActive(false);
        lcdBlocks[1].SetActive(false);
        lcdBlocks[2].SetActive(true);

        lcdBlocks[3].SetActive(false);
        lcdBlocks[4].SetActive(true);

        lcdBlocks[5].SetActive(false);
        lcdBlocks[6].SetActive(false);
        lcdBlocks[7].SetActive(true);

        lcdBlocks[8].SetActive(false);
        lcdBlocks[9].SetActive(true);

        lcdBlocks[10].SetActive(false);
        lcdBlocks[11].SetActive(false);
        lcdBlocks[12].SetActive(true);
    }
    private void SetTwo()
    {
        lcdBlocks[0].SetActive(true);
        lcdBlocks[1].SetActive(true);
        lcdBlocks[2].SetActive(true);

        lcdBlocks[3].SetActive(false);
        lcdBlocks[4].SetActive(true);

        lcdBlocks[5].SetActive(true);
        lcdBlocks[6].SetActive(true);
        lcdBlocks[7].SetActive(true);

        lcdBlocks[8].SetActive(true);
        lcdBlocks[9].SetActive(false);

        lcdBlocks[10].SetActive(true);
        lcdBlocks[11].SetActive(true);
        lcdBlocks[12].SetActive(true);
    }
    private void SetThree()
    {
        lcdBlocks[0].SetActive(false);
        lcdBlocks[1].SetActive(true);
        lcdBlocks[2].SetActive(true);

        lcdBlocks[3].SetActive(false);
        lcdBlocks[4].SetActive(true);

        lcdBlocks[5].SetActive(false);
        lcdBlocks[6].SetActive(true);
        lcdBlocks[7].SetActive(true);

        lcdBlocks[8].SetActive(false);
        lcdBlocks[9].SetActive(true);

        lcdBlocks[10].SetActive(false);
        lcdBlocks[11].SetActive(true);
        lcdBlocks[12].SetActive(true);
    }
    private void SetFour()
    {
        lcdBlocks[0].SetActive(true);
        lcdBlocks[1].SetActive(false);
        lcdBlocks[2].SetActive(true);

        lcdBlocks[3].SetActive(true);
        lcdBlocks[4].SetActive(true);

        lcdBlocks[5].SetActive(true);
        lcdBlocks[6].SetActive(true);
        lcdBlocks[7].SetActive(true);

        lcdBlocks[8].SetActive(false);
        lcdBlocks[9].SetActive(true);

        lcdBlocks[10].SetActive(false);
        lcdBlocks[11].SetActive(false);
        lcdBlocks[12].SetActive(true);
    }
    private void SetFive()
    {
        lcdBlocks[0].SetActive(true);
        lcdBlocks[1].SetActive(true);
        lcdBlocks[2].SetActive(true);

        lcdBlocks[3].SetActive(true);
        lcdBlocks[4].SetActive(false);

        lcdBlocks[5].SetActive(true);
        lcdBlocks[6].SetActive(true);
        lcdBlocks[7].SetActive(true);

        lcdBlocks[8].SetActive(false);
        lcdBlocks[9].SetActive(true);

        lcdBlocks[10].SetActive(true);
        lcdBlocks[11].SetActive(true);
        lcdBlocks[12].SetActive(true);
    }
    private void SetSix()
    {
        lcdBlocks[0].SetActive(true);
        lcdBlocks[1].SetActive(true);
        lcdBlocks[2].SetActive(true);

        lcdBlocks[3].SetActive(true);
        lcdBlocks[4].SetActive(false);

        lcdBlocks[5].SetActive(true);
        lcdBlocks[6].SetActive(true);
        lcdBlocks[7].SetActive(true);

        lcdBlocks[8].SetActive(true);
        lcdBlocks[9].SetActive(true);

        lcdBlocks[10].SetActive(true);
        lcdBlocks[11].SetActive(true);
        lcdBlocks[12].SetActive(true);
    }
    private void SetSeven()
    {
        lcdBlocks[0].SetActive(true);
        lcdBlocks[1].SetActive(true);
        lcdBlocks[2].SetActive(true);

        lcdBlocks[3].SetActive(false);
        lcdBlocks[4].SetActive(true);

        lcdBlocks[5].SetActive(false);
        lcdBlocks[6].SetActive(false);
        lcdBlocks[7].SetActive(true);

        lcdBlocks[8].SetActive(false);
        lcdBlocks[9].SetActive(true);

        lcdBlocks[10].SetActive(false);
        lcdBlocks[11].SetActive(false);
        lcdBlocks[12].SetActive(true);
    }
    private void SetEight()
    {
        lcdBlocks[0].SetActive(true);
        lcdBlocks[1].SetActive(true);
        lcdBlocks[2].SetActive(true);

        lcdBlocks[3].SetActive(true);
        lcdBlocks[4].SetActive(true);

        lcdBlocks[5].SetActive(true);
        lcdBlocks[6].SetActive(true);
        lcdBlocks[7].SetActive(true);

        lcdBlocks[8].SetActive(true);
        lcdBlocks[9].SetActive(true);

        lcdBlocks[10].SetActive(true);
        lcdBlocks[11].SetActive(true);
        lcdBlocks[12].SetActive(true);
    }
    private void SetNine()
    {
        lcdBlocks[0].SetActive(true);
        lcdBlocks[1].SetActive(true);
        lcdBlocks[2].SetActive(true);

        lcdBlocks[3].SetActive(true);
        lcdBlocks[4].SetActive(true);

        lcdBlocks[5].SetActive(true);
        lcdBlocks[6].SetActive(true);
        lcdBlocks[7].SetActive(true);

        lcdBlocks[8].SetActive(false);
        lcdBlocks[9].SetActive(true);

        lcdBlocks[10].SetActive(false);
        lcdBlocks[11].SetActive(false);
        lcdBlocks[12].SetActive(true);
    }
}
