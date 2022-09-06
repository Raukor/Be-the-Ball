using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour
{
    private GameObject ball;
    public GameObject slingShotPaddle;
    public Transform resetPoint;
    private Rigidbody slingShotPaddleRigidbody;
    //private ConfigurableJoint slingShotPaddleJoint;

    void Start()
    {
        //set up joint
        slingShotPaddleRigidbody = slingShotPaddle.GetComponent<Rigidbody>();
        //slingShotPaddleJoint = slingShotPaddle.GetComponent<ConfigurableJoint>();
        ball = GameObject.Find("Outer Sphere");
    }

    private void OnTriggerEnter(Collider triggerObject)
    {
        //activate joint
        if ( triggerObject.transform == ball.transform)
        {
            slingShotPaddleRigidbody.isKinematic = false;
            
            Debug.Log("trigger enter with ball");
        }
        
    }

    private void OnTriggerExit(Collider triggerObject)
    {
        //reset joint
        if (triggerObject.transform == ball.transform)
        {
            slingShotPaddleRigidbody.isKinematic = true;
            slingShotPaddle.transform.position = resetPoint.position;
            GameObject.Find("Score Keeper").GetComponent<ScoreManager>().Invoke(23);
        }
        
    }
}
