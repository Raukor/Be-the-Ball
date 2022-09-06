using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadballResetZone : MonoBehaviour
{
    public GameObject ball;
    public GameObject outerShell;
    private Rigidbody outerShellRigidbody;
    public float movementSpeed = 0;

    private bool isActive = false;

    public Transform moveToLocation;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        outerShellRigidbody = outerShell.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {

        if (isActive == true)
        {
            //ball.transform.position = Vector3.MoveTowards(ball.transform.position, moveToLocation.position, movementSpeed * Time.fixedDeltaTime);
            outerShellRigidbody.MovePosition(moveToLocation.position);
            if (moveToLocation.position == outerShell.transform.position)
            {
                isActive = false;
                outerShellRigidbody.isKinematic = false;
                outerShellRigidbody.useGravity = true;
            }
                
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        isActive = true;
        outerShellRigidbody.isKinematic = true;
        outerShellRigidbody.useGravity = false;
        outerShellRigidbody.velocity = Vector3.zero;
        outerShellRigidbody.angularVelocity = Vector3.zero;
    }
}
