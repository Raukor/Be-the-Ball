using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFollower : MonoBehaviour
{
    public Transform hand;
    public float followSpeed = 5f;
    public float dragScaling = .1f;
    public float dragBase = 1f;
    private Rigidbody thisRigidBody;

    

    void Start()
    {
        thisRigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //if(thisRigidBody.isKinematic == true)
           // thisRigidBody.MovePosition(hand.position);
        //else
        //{
            float magnitudeOfHandAndBall = (hand.position - gameObject.transform.position).magnitude;
            thisRigidBody.AddForce(followSpeed * (hand.position - gameObject.transform.position));
            thisRigidBody.drag = dragBase + (dragScaling / magnitudeOfHandAndBall);
        //}
       
    }
}
