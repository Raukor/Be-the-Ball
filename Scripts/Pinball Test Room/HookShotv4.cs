using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShotv4 : MonoBehaviour
{
    private LineRenderer ropeLR;
    private Vector3 grapplePoint;

    public LayerMask whatIsGrappleable;
    public Transform hand, outerShere;

    private float maxDistanceRaycast = 10000f;
    private SpringJoint joint;

    private float armMovement = 0;
    private float armDistanceStart = 0;
    private Vector3 handPositionSaved;

    public float retractVelocity;
    public float retractTolerence;


    private bool isActive = false;
    private bool isRetracting = false;

    private List<Vector3> grapplePositions = new List<Vector3>();

    public GameObject grappleHookPrefab;
    private GameObject grappleHook;

    public float spring = 0f, damper = 1000f, massScale = 1f, connectedMassScale = 5f, maxDistance = 1.1f, minDistance = .01f;
    public float distanceFromPoint;
    public float distanceFromPointMutable;
    public float jointDamper = 1000;

    public GameObject coloredSphere;
    public GameObject placeHolderBreaker;



    private void Start()
    {
        ropeLR = GetComponent<LineRenderer>();
        //set first position in grapple to hand for line renderer

    }

    void LateUpdate()
    {
        if (isActive == true)
        {
            DrawRope();
        }


    }

    private void Update()
    {

    }
    private void FixedUpdate()
    {
        if (isActive)
        {

            //Distance checking section
            float currentDistance = Vector3.Distance(outerShere.position, grapplePositions[grapplePositions.Count - 1]);
            //
            if (distanceFromPoint >= currentDistance)
            {
                joint.damper = 0;
            }
            if (distanceFromPoint < currentDistance)
            {
                joint.damper = jointDamper;
            }
            //End Distance checking

            //checks for hand motion to start retraction mode
            if (((armDistanceStart * retractTolerence) - Vector3.Distance(outerShere.position, hand.position)) > 0)
            {
                distanceFromPointMutable -= retractVelocity * Time.fixedDeltaTime;
                joint.maxDistance = distanceFromPointMutable;
                joint.spring = jointDamper;
                joint.damper = jointDamper;
                //added conditionals
                isRetracting = true;
            }
            else
            {
                joint.spring = 0;
            }

            RaycastHit currentGrappleScanHit;
            RaycastHit previousGrappleScanHit;
            //Section for the hook wrapping
            //raycast at the axis

            //replaced grapplePoint with grapplePositions[grapplePositions.Count - 1]



            //if it doesn't connect then make a new axis and save the old one in a list
            if (grapplePositions.Count > 1)
            {
                Physics.Raycast(hand.position, grapplePositions[grapplePositions.Count - 2] - hand.position, out previousGrappleScanHit, maxDistanceRaycast, whatIsGrappleable);

                if ((previousGrappleScanHit.point == grapplePositions[grapplePositions.Count - 2]))
                {
                    //placeHolderBreaker.transform.position = currentGrappleScanHit.point;

                    MoveingJointDeletion();
                    grapplePositions.RemoveAt(grapplePositions.Count - 1);

                    //added conditionals
                    if (isRetracting == true)
                        SpawnHookShot(jointDamper, jointDamper);
                    else
                        SpawnHookShot();

                }
            }
            Physics.Raycast(hand.position, grapplePositions[grapplePositions.Count - 1] - hand.position, out currentGrappleScanHit, maxDistanceRaycast, whatIsGrappleable);
            if ((currentGrappleScanHit.collider.transform.position != grapplePositions[grapplePositions.Count - 1]) && (Vector3.Distance(currentGrappleScanHit.collider.transform.position, hand.position) < distanceFromPoint))
            {
                placeHolderBreaker.transform.position = currentGrappleScanHit.point;

                MoveingJointDeletion();

                grapplePositions.Add(currentGrappleScanHit.point);

                //added conditionals
                if (isRetracting == true)
                    SpawnHookShot(jointDamper, jointDamper);
                else
                    SpawnHookShot();
            }



            //raycast at the last axis if it exists
            //if it connects then make a joint there and delete the previous one, remove the previous location from the list


        }
        //hookshot targeting
        RaycastHit targetingHit;
        Physics.Raycast(hand.position, hand.forward, out targetingHit, maxDistanceRaycast, whatIsGrappleable);
        coloredSphere.transform.position = targetingHit.point;




    }
    //added passables
    private void SpawnHookShot(float springL = 0f, float damperL = 1000f, float massScaleL = 1f, float connectedMassScaleL = 5f)
    {
        //grapplePoint = hit.point;

        armDistanceStart = Vector3.Distance(outerShere.position, hand.position);// the distance between the center of the ball and the players hand when the hook is spawned
        handPositionSaved = hand.position;

        grappleHook = Instantiate(grappleHookPrefab, grapplePositions[grapplePositions.Count - 1], Quaternion.Euler(Vector3.zero));
        joint = grappleHook.gameObject.AddComponent<SpringJoint>();

        //GrapplingHook Settings set up
        //joint.autoConfigureConnectedAnchor = false;
        //joint.connectedAnchor = joint.anchor;
        joint.connectedBody = outerShere.GetComponent<Rigidbody>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = joint.anchor;

        distanceFromPoint = Vector3.Distance(outerShere.position, grapplePositions[grapplePositions.Count - 1]);
        distanceFromPointMutable = distanceFromPoint;

        joint.maxDistance = distanceFromPoint;
        //joint.minDistance = distanceFromPoint;            
        joint.spring = spring;
        joint.damper = damper;
        joint.massScale = massScale;
        joint.connectedMassScale = connectedMassScale;
    }
    public void LaunchHookShot()
    {
        RaycastHit hit;

        if (Physics.Raycast(hand.position, hand.forward, out hit, maxDistanceRaycast, whatIsGrappleable))//&& isActive == false
        {

            isActive = true;
            grapplePositions.Add(hit.point);

            SpawnHookShot();

        }
    }
    private void MoveingJointDeletion()
    {
        armMovement = 0;
        armDistanceStart = 0;
        Destroy(grappleHook);

    }
    public void RetractHookShot()
    {

        grapplePositions = new List<Vector3>();
        armMovement = 0;
        armDistanceStart = 0;
        Destroy(grappleHook);
        isActive = false;
        ropeLR.positionCount = 0;
        isRetracting = false;
    }
    private void DrawRope()
    {
        //currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);


        grapplePositions.Add(hand.position);
        Vector3[] ropeLRArray;// = new Vector3[];
        ropeLRArray = grapplePositions.ToArray();

        ropeLR.positionCount = grapplePositions.Count;
        ropeLR.SetPositions(ropeLRArray);
        grapplePositions.RemoveAt(grapplePositions.Count - 1);
        //ropeLR.SetPosition(0, hand.position);
        //ropeLR.SetPosition(1, grapplePositions[grapplePositions.Count - 1]);
    }

}
