//Control Logic for how the hook shot's hook and joint should behave based on trigger presses and collisions
//By Landon Rhoades





using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShotDrive : MonoBehaviour
{
    
    public GameObject hand;
    

    public float hookShotVelocity = 0;
    //public GameObject handDirection;
    private int layerMaskForTargeting;
    private RaycastHit raycastTarget;
    private Vector3 normalOfTargetForAddForce;
    public float raycastDistance = 400;
    private ConfigurableJoint theRopeJoint;
    //private SoftJointLimitSpring theRopeJointLimitSpring;
    //private SoftJointLimit hookLimitSpring;

    private const int ballLayer = 7;
    private bool targetingRaycastTrue = false;
    //-1 retract, 0 ready to fire, 1 launched, 2 anchored
    private int stateOfDrive = 0;
    private bool collidedWith = false;

    private Rigidbody thisRigidbody;

    public Transform ballLocation;
    //private float previousBallDistance = 0f;
    //private float deltaHookBallDistance = 0f;
    //private float maxHookDistance = 0f;

    //joint control

    private LineRenderer chain;
    private Vector3[] chainLinkPositionsArray = new Vector3[2];
    //private int chainLinks = 2;//will use for wrapping with the line renderer   switched to an array for now, wrapping solution needs a list for dynamic scaling of points, see vectorDrawing in project ikkyo
    //private List<Vector3> chainLinkPositions = new List<Vector3>(); 
    public float lrEndWidth = 0.15f;
    public float lrStartWidth = 0.15f;

    void Start()
    {
        layerMaskForTargeting = LayerMask.NameToLayer("Machine Parts");
        thisRigidbody = gameObject.GetComponent<Rigidbody>();
        theRopeJoint = thisRigidbody.GetComponent<ConfigurableJoint>();
        
        chain = gameObject.GetComponent<LineRenderer>();
        chain.endWidth = lrEndWidth;
        chain.startWidth = lrStartWidth;

    }

    // Update is called once per frame
    private void Update()
    {
        if((stateOfDrive == 2) || (stateOfDrive == 1))
        {         


            chain.enabled = true;
            //chain.positionCount = chainLinks;
            chainLinkPositionsArray[0] = gameObject.transform.position;
            chainLinkPositionsArray[1] = hand.transform.position;
            chain.SetPositions(chainLinkPositionsArray);           

        }
        
    }

    void FixedUpdate()
    {

       
        if ((collidedWith == false) && (stateOfDrive == 1))
        {
            thisRigidbody.MovePosition(raycastTarget.point);
            //MovePosition(Vector3.Normalize(raycastTarget.point - thisRigidbody.transform.position) * Time.deltaTime * hookShotVelocity);
            Debug.Log("no collide state 1");
        }
        
        if ((collidedWith == true) && (stateOfDrive == 1))
        {
            thisRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            thisRigidbody.isKinematic = false;
            stateOfDrive = 2;
            theRopeJoint.anchor = transform.InverseTransformPoint(hand.transform.position);

            SoftJointLimit hookLimitSpring = theRopeJoint.linearLimit;
            hookLimitSpring.limit = Vector3.Distance(raycastTarget.point, ballLocation.position);
            
            theRopeJoint.linearLimit = hookLimitSpring;

            Debug.Log("collide & state 1");
        }        
        if (stateOfDrive == 2)
        {
            //theRopeJoint.anchor = transform.InverseTransformPoint(hand.transform.position);


            /*
            //set up the rope joint limit spring based on direction towards axis                
            deltaHookBallDistance = Vector3.Distance(gameObject.transform.position, ballLocation.position) - previousBallDistance;
            previousBallDistance = Vector3.Distance(gameObject.transform.position, ballLocation.position);
            if (deltaHookBallDistance > 0)
            {
                theRopeJointLimitSpring.spring = 0f;
                theRopeJoint.linearLimitSpring = theRopeJointLimitSpring;
            }
            if ((deltaHookBallDistance < 0) && (previousBallDistance >= maxHookDistance))
            {
                theRopeJointLimitSpring.spring = 1000f;
                theRopeJoint.linearLimitSpring = theRopeJointLimitSpring;
            }
            */
            Debug.Log("state 2");
        }
        if (stateOfDrive == 0)
        {
            collidedWith = false;
            thisRigidbody.MovePosition(hand.transform.position);
            Debug.Log("state 0");
        }
        // when retracted:
        //turn on kinematic
        // turn off ball layer
        //move towards players hand


    }
    private void OnCollisionEnter(Collision collision)
    {
        //on collision switch to ball physics layer
        //lock position
        Debug.Log("ball collision");
        if (collision.gameObject.transform == raycastTarget.transform)
        {
            Debug.Log("ball collision correct");
            collidedWith = true;
        }

        //thisRigidbody.constraints = RigidbodyConstraints.FreezePosition;
        //gameObject.layer = ballLayer;
    }

    public void LaunchHookShot()
    {
        Debug.Log("launch");

        //when launched:
        //turn off kinematic
        //Raycast out to find target
        //apply force towards target

        if (stateOfDrive == 0)
        {
            targetingRaycastTrue = Physics.Raycast(hand.transform.position, hand.transform.TransformDirection(Vector3.forward), out raycastTarget, raycastDistance);
            //, layerMaskForTargeting
            Debug.Log("launch 2 ");

            if (targetingRaycastTrue == true)
            {
                //thisRigidbody.isKinematic = false;
                //normalOfTargetForAddForce = (raycastTarget.point - gameObject.transform.position);//.normalized;
                //thisRigidbody.AddForce(hookShootVelocity * normalOfTargetForAddForce, ForceMode.Impulse);
                Debug.Log("launch -  raycast true");
                stateOfDrive = 1;
            }
        }
        
        
    }
    public void RetractHookShot()
    {
        //thisRigidbody.isKinematic = true;
        Debug.Log("retract");
        if(stateOfDrive != 0)
        {
            thisRigidbody.isKinematic = true;
            thisRigidbody.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
            targetingRaycastTrue = false;
            chain.enabled = false;
            stateOfDrive = 0;
        }
       
    }
}
