using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class TESTButtonControls : MonoBehaviour
{
    // Start is called before the first frame update

    private ActionBasedController controller;
    
    //Variables for the plunger
    bool isActivatePressed;
    bool isSelectPressed;
    bool isHookAcvtive = false;
    bool isPlungerDrawn = false;

    public GameObject plungerHammer = null;
    private Rigidbody plungerHammerRigidbody = null;
    public GameObject plungerBase = null;

    public GameObject plungerHammer2 = null;
    private Rigidbody plungerHammerRigidbody2 = null;
    public GameObject plungerBase2 = null;

    public GameObject plungerHammer3 = null;
    private Rigidbody plungerHammerRigidbody3 = null;
    public GameObject plungerBase3 = null;

    public float plungerDrawSpeed = 4;
    private float plungerDelta = 0;

    
    //Variables for the flippers
    public GameObject rightFlipperPaddle = null;
    private HingeJoint rightFlipperHingeJoint = null;
    private JointMotor rightFlipperJointMotor;

    public GameObject leftFlipperPaddle = null;
    private JointMotor leftFlipperJointMotor;
    private HingeJoint leftFlipperHingeJoint = null;

    public GameObject rightFlipperPaddleSecondary = null;
    private HingeJoint rightFlipperHingeJointSecondary = null;
    private JointMotor rightFlipperJointMotorSecondary;

    public GameObject leftFlipperPaddleSecondary = null;
    private JointMotor leftFlipperJointMotorSecondary;
    private HingeJoint leftFlipperHingeJointSecondary = null;
    

    //Variables for the hook shots
    public GameObject rightHandHookShot;

    




    void Start()
    {
        //controller setup
        controller = GetComponent<ActionBasedController>();

        bool isPressed = controller.activateAction.action.ReadValue<bool>();

        //Activate Action - trigger
        controller.activateAction.action.started += Activate_Action_started;
        controller.activateAction.action.performed += Activate_Action_performed;
        controller.activateAction.action.canceled += Activate_Action_canceled;
        
        //Select option - touch pad click
        controller.selectAction.action.started += Select_Action_started;
        controller.selectAction.action.performed += Select_Action_performed;
        controller.selectAction.action.canceled += Select_Action_canceled;

        //Boost option - touch pad click
        //controller.uiPressAction.action.started += Select_Action_started;
        controller.uiPressAction.action.performed += UIPress_Action_performed;
        //controller.uiPressAction.action.canceled += Select_Action_canceled;

        //gameobject setup:
        //plunger set up
        plungerHammerRigidbody = plungerHammer.GetComponent<Rigidbody>();
        plungerHammerRigidbody2 = plungerHammer2.GetComponent<Rigidbody>();
        plungerHammerRigidbody3 = plungerHammer3.GetComponent<Rigidbody>();
        //flipper set up
        rightFlipperHingeJoint = rightFlipperPaddle.GetComponent<HingeJoint>();
        rightFlipperJointMotor = rightFlipperHingeJoint.motor;
        leftFlipperHingeJoint = leftFlipperPaddle.GetComponent<HingeJoint>();
        leftFlipperJointMotor = leftFlipperHingeJoint.motor;

        rightFlipperHingeJointSecondary = rightFlipperPaddleSecondary.GetComponent<HingeJoint>();
        rightFlipperJointMotorSecondary = rightFlipperHingeJointSecondary.motor;
        leftFlipperHingeJointSecondary = leftFlipperPaddleSecondary.GetComponent<HingeJoint>();
        leftFlipperJointMotorSecondary = leftFlipperHingeJointSecondary.motor;

    }

    //Bool Control for Activate Action
    private void Activate_Action_canceled(InputAction.CallbackContext obj)
    {
        //Debug.Log("canceled Test Script");
        isActivatePressed = false;
    }

    private void Activate_Action_started(InputAction.CallbackContext obj)
    {
        //Debug.Log("started Test Script");
    }

    private void Activate_Action_performed(InputAction.CallbackContext obj)
    {
        //Debug.Log("performed Test Script");
        isActivatePressed = true;
    }



    //Bool control for Select action
    private void Select_Action_canceled(InputAction.CallbackContext obj)
    {
        //Debug.Log("canceled Test Script");
        isSelectPressed = false;
    }

    private void Select_Action_started(InputAction.CallbackContext obj)
    {
        //Debug.Log("started Test Script");
    }

    private void Select_Action_performed(InputAction.CallbackContext obj)
    {
        //Debug.Log("performed Test Script");
        isSelectPressed = true;
    }



    //control for uipress action
    private void UIPress_Action_performed(InputAction.CallbackContext obj)
    {
        //boost is changed to zero velocity, set up for respawning
        GameObject.Find("Outer Sphere").GetComponent<VelocityMagnifier>().Boost();
        
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        
        
        if (isSelectPressed == true)
        {
            PlungerDrawBack();
            FlipperUp();            
        }
        if ((isPlungerDrawn == true) && (isSelectPressed == false))
        {
            PlungerRelease();
            FlipperDown();            
        }

        if (isActivatePressed == true)
        {
            LaunchHookShot();
        }
        if (isActivatePressed == false)
        {
            RetractHookShot();
        }

    }

    public void LaunchHookShot()
    {
        //rightHandHookShot.GetComponent<HookShotDrive>().LaunchHookShot();
        if (isHookAcvtive == false)
        {
            rightHandHookShot.GetComponent<HookShotv3>().StartGrapple();
            isHookAcvtive = true;
        }

    }
    public void RetractHookShot()
    {
        //rightHandHookShot.GetComponent<HookShotDrive>().RetractHookShot();
        if (isHookAcvtive == true)
        {
            rightHandHookShot.GetComponent<HookShotv3>().EndGrapple();
            isHookAcvtive = false;
        }
    }

    private void FlipperUp()
    {
        //rightFlipperJointMotor = rightFlipperHingeJoint.motor;
        rightFlipperJointMotor.targetVelocity = 1000;
        rightFlipperHingeJoint.motor = rightFlipperJointMotor;

        leftFlipperJointMotor.targetVelocity = 1000;
        leftFlipperHingeJoint.motor = rightFlipperJointMotor;

        rightFlipperJointMotorSecondary.targetVelocity = 1000;
        rightFlipperHingeJointSecondary.motor = rightFlipperJointMotorSecondary;

        leftFlipperJointMotorSecondary.targetVelocity = 1000;
        leftFlipperHingeJointSecondary.motor = rightFlipperJointMotorSecondary;
    }
    private void FlipperDown()
    {
        rightFlipperJointMotor.targetVelocity = -1000;
        rightFlipperHingeJoint.motor = rightFlipperJointMotor;

        leftFlipperJointMotor.targetVelocity = -1000;
        leftFlipperHingeJoint.motor = rightFlipperJointMotor;

        rightFlipperJointMotorSecondary.targetVelocity = -1000;
        rightFlipperHingeJointSecondary.motor = rightFlipperJointMotorSecondary;

        leftFlipperJointMotorSecondary.targetVelocity = -1000;
        leftFlipperHingeJointSecondary.motor = rightFlipperJointMotorSecondary;
    }
    private void PlungerDrawBack()
    {
        isPlungerDrawn = true;
        //plungerHammerRigidbody.constraints = RigidbodyConstraints.FreezePositionX;
        plungerHammerRigidbody.isKinematic = true;
        plungerHammerRigidbody2.isKinematic = true;
        plungerHammerRigidbody3.isKinematic = true;

        if (Vector3.Distance(plungerHammer.transform.position, plungerBase.transform.position) >= 0.05f)
        {
            plungerDelta = plungerDrawSpeed * Time.fixedDeltaTime;

            plungerHammer.transform.position = Vector3.MoveTowards(plungerHammer.transform.position, plungerBase.transform.position, plungerDelta);
            plungerHammer2.transform.position = Vector3.MoveTowards(plungerHammer2.transform.position, plungerBase2.transform.position, plungerDelta);
            plungerHammer3.transform.position = Vector3.MoveTowards(plungerHammer3.transform.position, plungerBase3.transform.position, plungerDelta);
        }
    }

    private void PlungerRelease()
    {
        isPlungerDrawn = false;
        plungerHammerRigidbody.isKinematic = false;
        plungerHammerRigidbody2.isKinematic = false;
        plungerHammerRigidbody3.isKinematic = false;
        //plungerHammerRigidbody.constraints = RigidbodyConstraints.None;
        //plungerHammerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }
}
