using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncerv2 : MonoBehaviour
{
    private GameObject ball = null;
    public GameObject slingShotPaddle_0;
    

    public Transform resetPoint;
    private Rigidbody slingShotPaddleRigidbody_0;
    private bool timerOn = false;
    private float timer = 0f;
    public float resetTime = .1f;



    //private ConfigurableJoint slingShotPaddleJoint;


    void Start()
    {
        //set up joint
        slingShotPaddleRigidbody_0 = slingShotPaddle_0.GetComponent<Rigidbody>();
        ball = GameObject.Find("Outer Sphere");

        Debug.Log(ball.transform.name);
        //slingShotPaddleJoint = slingShotPaddle.GetComponent<ConfigurableJoint>();


    }

    private void OnTriggerEnter(Collider triggerObject)
    {
        //activate joint
        if (triggerObject.transform == ball.transform)
        {
            //rotate towards ball.postion
            Vector3 aimDirection = new Vector3();
            aimDirection = ball.transform.position;
            aimDirection.x = aimDirection.x * Random.Range(.95f, 1.05f);
            aimDirection.z = aimDirection.z * Random.Range(.95f, 1.05f);
            slingShotPaddle_0.transform.forward = aimDirection - resetPoint.position;
            slingShotPaddleRigidbody_0.isKinematic = false;
            timerOn = true;



            Debug.Log("trigger enter with ball");
        }

    }
    
    private void OnTriggerExit(Collider triggerObject)
    {
        //reset joint
        if (triggerObject.transform == ball.transform)
        {
            GameObject.Find("Score Keeper").GetComponent<ScoreManager>().Invoke(11);
            

        }

    }

    private void Update()
    {
        if (timerOn == true)
        {
            timer += Time.deltaTime;
            if (timer >= resetTime)
            {
                timer = 0;
                timerOn = false;
                slingShotPaddleRigidbody_0.isKinematic = true;

                slingShotPaddle_0.transform.position = resetPoint.position;
            }
        }
    }

}
