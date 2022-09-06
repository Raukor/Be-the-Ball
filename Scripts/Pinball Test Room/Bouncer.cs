using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public Transform ball;
    public GameObject slingShotPaddle_0;
    public GameObject slingShotPaddle_1;
    public GameObject slingShotPaddle_2;
    public GameObject slingShotPaddle_3;
    public GameObject slingShotPaddle_4;
    public GameObject slingShotPaddle_5;
    public GameObject slingShotPaddle_6;
    public GameObject slingShotPaddle_7;
    public GameObject slingShotPaddle_8;
    public GameObject slingShotPaddle_9;
    public GameObject slingShotPaddle_10;
    public GameObject slingShotPaddle_11;
    public GameObject slingShotPaddle_12;
    public GameObject slingShotPaddle_13;
    public GameObject slingShotPaddle_14;

    public Transform resetPoint;
    private Rigidbody slingShotPaddleRigidbody_0;
    private Rigidbody slingShotPaddleRigidbody_1;
    private Rigidbody slingShotPaddleRigidbody_2;
    private Rigidbody slingShotPaddleRigidbody_3;
    private Rigidbody slingShotPaddleRigidbody_4;
    private Rigidbody slingShotPaddleRigidbody_5;
    private Rigidbody slingShotPaddleRigidbody_6;
    private Rigidbody slingShotPaddleRigidbody_7;
    private Rigidbody slingShotPaddleRigidbody_8;
    private Rigidbody slingShotPaddleRigidbody_9;
    private Rigidbody slingShotPaddleRigidbody_10;
    private Rigidbody slingShotPaddleRigidbody_11;
    private Rigidbody slingShotPaddleRigidbody_12;
    private Rigidbody slingShotPaddleRigidbody_13;
    private Rigidbody slingShotPaddleRigidbody_14;



    //private ConfigurableJoint slingShotPaddleJoint;


    void Start()
    {
        //set up joint
        slingShotPaddleRigidbody_0 = slingShotPaddle_0.GetComponent<Rigidbody>();
        slingShotPaddleRigidbody_1 = slingShotPaddle_1.GetComponent<Rigidbody>();
        slingShotPaddleRigidbody_2 = slingShotPaddle_2.GetComponent<Rigidbody>();
        slingShotPaddleRigidbody_3 = slingShotPaddle_3.GetComponent<Rigidbody>();
        slingShotPaddleRigidbody_4 = slingShotPaddle_4.GetComponent<Rigidbody>();
        slingShotPaddleRigidbody_5 = slingShotPaddle_5.GetComponent<Rigidbody>();
        slingShotPaddleRigidbody_6 = slingShotPaddle_6.GetComponent<Rigidbody>();
        slingShotPaddleRigidbody_7 = slingShotPaddle_7.GetComponent<Rigidbody>();
        slingShotPaddleRigidbody_8 = slingShotPaddle_8.GetComponent<Rigidbody>();
        slingShotPaddleRigidbody_9 = slingShotPaddle_9.GetComponent<Rigidbody>();
        slingShotPaddleRigidbody_10 = slingShotPaddle_10.GetComponent<Rigidbody>();
        slingShotPaddleRigidbody_11 = slingShotPaddle_11.GetComponent<Rigidbody>();
        slingShotPaddleRigidbody_12 = slingShotPaddle_12.GetComponent<Rigidbody>();
        slingShotPaddleRigidbody_13 = slingShotPaddle_13.GetComponent<Rigidbody>();
        slingShotPaddleRigidbody_14 = slingShotPaddle_14.GetComponent<Rigidbody>();
        

        //slingShotPaddleJoint = slingShotPaddle.GetComponent<ConfigurableJoint>();
    }

    private void OnTriggerEnter(Collider triggerObject)
    {
        //activate joint
        if (triggerObject.transform == ball)
        {
            slingShotPaddleRigidbody_0.isKinematic = false;
            slingShotPaddleRigidbody_1.isKinematic = false;
            slingShotPaddleRigidbody_2.isKinematic = false;
            slingShotPaddleRigidbody_3.isKinematic = false;
            slingShotPaddleRigidbody_4.isKinematic = false;
            slingShotPaddleRigidbody_5.isKinematic = false;
            slingShotPaddleRigidbody_6.isKinematic = false;
            slingShotPaddleRigidbody_7.isKinematic = false;
            slingShotPaddleRigidbody_8.isKinematic = false;
            slingShotPaddleRigidbody_9.isKinematic = false;
            slingShotPaddleRigidbody_10.isKinematic = false;
            slingShotPaddleRigidbody_11.isKinematic = false;
            slingShotPaddleRigidbody_12.isKinematic = false;
            slingShotPaddleRigidbody_13.isKinematic = false;
            slingShotPaddleRigidbody_14.isKinematic = false;

            Debug.Log("trigger enter with ball");
        }

    }

    private void OnTriggerExit(Collider triggerObject)
    {
        //reset joint
        if (triggerObject.transform == ball)
        {
            slingShotPaddleRigidbody_0.isKinematic = true;
            slingShotPaddleRigidbody_1.isKinematic = true;
            slingShotPaddleRigidbody_2.isKinematic = true;
            slingShotPaddleRigidbody_3.isKinematic = true;
            slingShotPaddleRigidbody_4.isKinematic = true;
            slingShotPaddleRigidbody_5.isKinematic = true;
            slingShotPaddleRigidbody_6.isKinematic = true;
            slingShotPaddleRigidbody_7.isKinematic = true;
            slingShotPaddleRigidbody_8.isKinematic = true;
            slingShotPaddleRigidbody_9.isKinematic = true;
            slingShotPaddleRigidbody_10.isKinematic = true;
            slingShotPaddleRigidbody_11.isKinematic = true;
            slingShotPaddleRigidbody_12.isKinematic = true;
            slingShotPaddleRigidbody_13.isKinematic = true;
            slingShotPaddleRigidbody_14.isKinematic = true;

            slingShotPaddle_0.transform.position = resetPoint.position;
            slingShotPaddle_1.transform.position = resetPoint.position;
            slingShotPaddle_2.transform.position = resetPoint.position;
            slingShotPaddle_3.transform.position = resetPoint.position;
            slingShotPaddle_4.transform.position = resetPoint.position;
            slingShotPaddle_5.transform.position = resetPoint.position;
            slingShotPaddle_6.transform.position = resetPoint.position;
            slingShotPaddle_7.transform.position = resetPoint.position;
            slingShotPaddle_8.transform.position = resetPoint.position;
            slingShotPaddle_9.transform.position = resetPoint.position;
            slingShotPaddle_10.transform.position = resetPoint.position;
            slingShotPaddle_11.transform.position = resetPoint.position;
            slingShotPaddle_12.transform.position = resetPoint.position;
            slingShotPaddle_13.transform.position = resetPoint.position;
            slingShotPaddle_14.transform.position = resetPoint.position;
            
        }

    }
}
