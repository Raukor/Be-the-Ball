using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShotv3 : MonoBehaviour
{
    private LineRenderer ropeLR;
    private Vector3 grapplePoint;

    public LayerMask whatIsGrappleable;
    public LayerMask sphereForUnwrapping;
    public Transform hand, outerShere;
    private Rigidbody outerSphereRB;

    private float maxDistanceRaycast = 10000f;
    private SpringJoint joint;

    //private float armMovement = 0;
    private float armDistanceStart = 0;


    public float retractVelocity;
    public float retractTolerence;


    private bool isActive = false;
    //private bool isRetracting = false;

    private List<Vector3> grapplePositions = new List<Vector3>();

    public GameObject grappleHookPrefab;
    private GameObject grappleHook;

    public float spring = 0f, massScale = 1f, connectedMassScale = 5f, maxDistance = 1.1f, minDistance = .01f;
    public float distanceFromPointAtSpawn;
    public float distanceFromPointMutable;
    public float jointDamper;

    public GameObject coloredSphere;
    public GameObject placeHolderBreaker;

    private bool isSafeToUnwrap = false;

    public Light gunLight;

    public GameObject colliderForUnwrapping;
    

    private void Start()
    {
        ropeLR = GetComponent<LineRenderer>();
        outerSphereRB = outerShere.GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        if (isActive == true)
        {
            DrawRope();
        }
    }




    private void FixedUpdate()
    {
        if (isActive)
        {
            MaxDistanceCheck();


            RaycastHit currentGrappleScanHit;
            RaycastHit previousGrappleScanHit;

            //Unwrapping Check
            if (grapplePositions.Count >= 2)
            {
                Physics.Raycast(hand.position, colliderForUnwrapping.transform.position - hand.position, out previousGrappleScanHit, maxDistanceRaycast, whatIsGrappleable);
                Physics.Raycast(hand.position, grapplePositions[grapplePositions.Count - 1] - hand.position, out currentGrappleScanHit, maxDistanceRaycast, whatIsGrappleable);

                if (previousGrappleScanHit.collider.transform == colliderForUnwrapping.transform)
                {
                    DestroyJoint();
                    grapplePositions.RemoveAt(grapplePositions.Count - 1);
                    SpawnJoint();
                    Debug.Log("unwrap");
                }
                //Wrapping Check                
                else if ((currentGrappleScanHit.point != grapplePositions[grapplePositions.Count - 1]) && (currentGrappleScanHit.collider != colliderForUnwrapping.GetComponent<SphereCollider>()) && (Vector3.Distance(currentGrappleScanHit.point, hand.position) < distanceFromPointAtSpawn))
                {
                    placeHolderBreaker.transform.position = currentGrappleScanHit.point;
                    DestroyJoint();
                    grapplePositions.Add(currentGrappleScanHit.point);
                    SpawnJoint();
                    Debug.Log("second wrap");

                }
            }
            else
            {
                //Wrapping Check
                Physics.Raycast(hand.position, grapplePositions[grapplePositions.Count - 1] - hand.position, out currentGrappleScanHit, maxDistanceRaycast, whatIsGrappleable);

                if ((currentGrappleScanHit.point != grapplePositions[grapplePositions.Count - 1]) && (Vector3.Distance(currentGrappleScanHit.point, hand.position) < distanceFromPointAtSpawn))
                {
                    placeHolderBreaker.transform.position = currentGrappleScanHit.point;
                    DestroyJoint();
                    grapplePositions.Add(currentGrappleScanHit.point);
                    SpawnJoint();
                    Debug.Log("first wrap");
                }
            }
            

            RetractModeGestureCheck();
        }

        //Raycast for targetting recepticle
        RaycastHit targetingHit;
        Physics.Raycast(hand.position, hand.forward, out targetingHit, maxDistanceRaycast, whatIsGrappleable);
        coloredSphere.transform.position = targetingHit.point;


    }

    //Starts the grappling hook mode based on a trigger pull, accessed from the button control script
    public void StartGrapple()
    {
        RaycastHit hit;

        if (Physics.Raycast(hand.position, hand.forward, out hit, maxDistanceRaycast, whatIsGrappleable))//&& isActive == false
        {
            isActive = true;
            gunLight.color = Color.red;
            grapplePositions.Add(hit.point);
            SpawnJoint();
            //Saves the distance between the player hand and the center of the ball for gesture checking
            armDistanceStart = Vector3.Distance(outerShere.position, hand.position);
        }
    }

    //Ends the grappling hook mode based on a trigger release, accessed from the button control script
    public void EndGrapple()
    {
        grapplePositions = new List<Vector3>();
        //armMovement = 0;
        armDistanceStart = 0;
        Destroy(grappleHook);
        isActive = false;
        gunLight.color = Color.yellow;
        ropeLR.positionCount = 0;

        colliderForUnwrapping.transform.position = Vector3.zero;
    }

    //Checks to see if the distance the rope is total, is no more than the current max.  Either the starting distance, or modified by retract mode, number is for error room
    private void MaxDistanceCheck()
    {
        float currentDistance = Vector3.Distance(outerShere.position, grapplePositions[grapplePositions.Count - 1]);

        if (distanceFromPointAtSpawn >= (currentDistance - .5))
        {
            joint.damper = 0;
            joint.maxDistance = 0;


        }
        else if (distanceFromPointAtSpawn < (currentDistance + .5))
        {
            joint.damper = 1000;
            //joint.maxDistance = distanceFromPointAtSpawn;


        }
    }

    //Checks to see the hand motion for triggering retraction mode.  currently works by bringing your hands closer to the center of the ball
    private void RetractModeGestureCheck()
    {
        if (((armDistanceStart * retractTolerence) - Vector3.Distance(outerShere.position, hand.position)) > 0)
        {
            distanceFromPointAtSpawn -= retractVelocity * Time.deltaTime;
            //joint.maxDistance = distanceFromPointAtSpawn;
            joint.spring = 250;//only moving on this
            joint.damper = 1000;
            gunLight.color = Color.green;
        }
        else
        {
            joint.spring = 0;
            gunLight.color = Color.red;
        }
    }

    //creates new joints.  Used in the initial spawning and in wrapping
    private void SpawnJoint()
    {


        //Creates the game object with a joint that acts as the grappling hook
        grappleHook = Instantiate(grappleHookPrefab, grapplePositions[grapplePositions.Count - 1], Quaternion.Euler(Vector3.zero));
        joint = grappleHook.gameObject.AddComponent<SpringJoint>();

        //Sets the distance of the joint from the ball at joint spawn
        distanceFromPointAtSpawn = Vector3.Distance(outerShere.position, grapplePositions[grapplePositions.Count - 1]);


        //Joint set up section:        
        joint.connectedBody = outerShere.GetComponent<Rigidbody>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = joint.anchor;
        joint.maxDistance = 0;

        joint.spring = 00;
        joint.damper = 000;
        joint.massScale = massScale;
        joint.connectedMassScale = connectedMassScale;
        if (grapplePositions.Count >= 2)
        {
            colliderForUnwrapping.transform.position = (.1f * Vector3.Normalize(grapplePositions[grapplePositions.Count - 2] - grapplePositions[grapplePositions.Count - 1])) + grapplePositions[grapplePositions.Count - 1];            
        }
        else
        {
            colliderForUnwrapping.transform.position = Vector3.zero;
        }
    }

    //removes old joints.  Used in ending the grapple and in unwrapping
    private void DestroyJoint()
    {
        Destroy(grappleHook);
        colliderForUnwrapping.transform.position = Vector3.zero;

    }



    //updates the line renderer for the rope's graphic
    private void DrawRope()
    {
        grapplePositions.Add(hand.position);
        Vector3[] ropeLRArray;// = new Vector3[];
        ropeLRArray = grapplePositions.ToArray();
        ropeLR.positionCount = grapplePositions.Count;
        ropeLR.SetPositions(ropeLRArray);
        grapplePositions.RemoveAt(grapplePositions.Count - 1);
    }
}
    
