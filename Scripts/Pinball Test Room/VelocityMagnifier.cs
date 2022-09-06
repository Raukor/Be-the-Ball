using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityMagnifier : MonoBehaviour
{

    private Rigidbody thisRigidBody;
    private Vector3 velocityHolder;
    private float velocityHolderMagnitude;
    public float minSpeedBoost;
    public float maxSpeedThrottle;
    public GameObject audioSource;
    public Transform respawnPoint = null;

    //public float scalarBoost = 1;



    void Start()
    {
        thisRigidBody = gameObject.GetComponent<Rigidbody>();
        //thisRigidBody.maxAngularVelocity = 200;
    }

    private void FixedUpdate()
    {
        velocityHolder = thisRigidBody.velocity;
        velocityHolderMagnitude = Vector3.Magnitude(velocityHolder);

        audioSource.GetComponent<AudioSource>().volume = velocityHolderMagnitude / maxSpeedThrottle;

        if (velocityHolderMagnitude < minSpeedBoost)
        {
            thisRigidBody.velocity = (minSpeedBoost / velocityHolderMagnitude) * velocityHolder;
        }
        if (velocityHolderMagnitude > maxSpeedThrottle)
        {
            thisRigidBody.velocity = (maxSpeedThrottle / velocityHolderMagnitude) * velocityHolder;
        }
    }

    public void Boost()
    {
        thisRigidBody.velocity = Vector3.zero;
        thisRigidBody.isKinematic = true;
        gameObject.transform.position = respawnPoint.position;
        thisRigidBody.isKinematic = false;//2 * thisRigidBody.velocity;
    }
    
}
