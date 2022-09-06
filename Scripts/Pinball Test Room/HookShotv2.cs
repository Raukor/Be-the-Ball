using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShotv2 : MonoBehaviour
{
    private LineRenderer ropeLR;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform hand, outerShere;
    
    private float maxDistanceRaycast = 10000f;
    private SpringJoint joint;

    private float armMovement = 0;
    private float armDistanceStart = 0;
    private GameObject handPositionSaved;
    public float retractVelocity;


    private bool isActive = false;
    private Vector3 currentGrapplePosition;

    public GameObject grappleHookPrefab;
    private GameObject grappleHook;

    public float spring = 0f, damper = 1000f, massScale = 1f, connectedMassScale = 5f, maxDistance = 1.1f, minDistance = .01f;
    public float distanceFromPoint;
    public float distanceFromPointMutable;
    public float jointDamper = 1000;
    private void Start()
    {
        ropeLR = GetComponent<LineRenderer>();
    }

    void LateUpdate()
    {
        if(isActive == true)
        {
            DrawRope();
        }

        
    }


    private void FixedUpdate()
    {
        if (isActive)
        {
            float currentDistance = Vector3.Distance(outerShere.position, grapplePoint);

            //armMovement = armDistanceStart / Vector3.Distance(outerShere.position, handPositionSaved.transform.position);

            
            
            
            //Debug.Log("111 armDistanceStart " + armDistanceStart);
            
            //Debug.Log("111 armMovement " + armMovement);
            
            //Debug.Log("111 current distance " + Vector3.Distance(outerShere.position, handPositionSaved.transform.position));
            
            if (distanceFromPoint >= currentDistance)
            {
                joint.damper = 0;                
            }

            if (distanceFromPoint < currentDistance)
            {
                joint.damper = jointDamper;
            }

            if (((armDistanceStart * .95f) - Vector3.Distance(outerShere.position, handPositionSaved.transform.position)) > 0)
            {
                distanceFromPointMutable -= retractVelocity * Time.fixedDeltaTime;
                joint.maxDistance = distanceFromPointMutable;
                joint.spring = jointDamper;
                joint.damper = jointDamper;
            }
            else
            {
                joint.spring = 0;
            }
            
        }
    }

    public void LaunchHookShot(GameObject handPosition)
    {
        RaycastHit hit;
        if (Physics.Raycast(hand.position, hand.forward, out hit, maxDistanceRaycast, whatIsGrappleable) && isActive == false)
        {
            grapplePoint = hit.point;

            armDistanceStart = Vector3.Distance(outerShere.position, handPosition.transform.position);
            handPositionSaved = handPosition;

            grappleHook = Instantiate(grappleHookPrefab, grapplePoint, Quaternion.Euler(Vector3.zero));
            joint = grappleHook.gameObject.AddComponent<SpringJoint>();
            //joint.autoConfigureConnectedAnchor = false;
            //joint.connectedAnchor = joint.anchor;
            joint.connectedBody = outerShere.GetComponent<Rigidbody>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = joint.anchor;
            distanceFromPoint = Vector3.Distance(outerShere.position, grapplePoint);
            distanceFromPointMutable = distanceFromPoint;
            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint;
            //joint.minDistance = distanceFromPoint;


            //Adjust these values to fit your game.
            joint.spring = spring;
            joint.damper = damper;
            joint.massScale = massScale;
            joint.connectedMassScale = connectedMassScale;
            ropeLR.positionCount = 2;
            currentGrapplePosition = hand.position;
            isActive = true;
        }
    }
    public void RetractHookShot()
    {
        ropeLR.positionCount = 0;
        armMovement = 0;
        armDistanceStart = 0;
        Destroy(grappleHook);
        isActive = false;
    }
    private void DrawRope()
    {
        //currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        //ropeLR.SetPosition(0, hand.position);
        //ropeLR.SetPosition(1, grapplePoint);
    }

}
