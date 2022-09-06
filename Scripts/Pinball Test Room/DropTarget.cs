using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTarget : MonoBehaviour
{
    float timer = 0;

    private void OnTriggerEnter(Collider collision)
    {
        gameObject.transform.position += 50 * Vector3.down;
        GameObject.Find("Score Keeper").GetComponent<ScoreManager>().Invoke(100);
     
    }
    /*
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5)
        {
            ResetDropTarget();
            timer = 0;
        }
    }
    */
    public void ResetDropTarget()
    {
        gameObject.transform.position -= 50 * Vector3.down;
    }

    
}
