using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    [Header("Wheels Collider")]
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider backLeftWheelCollider;

    public WheelCollider BackRightWheelCollider;
    [Header("Wheels Transform")]

    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform backLeftWheelTransform;
    public Transform backRightWheelTransform;


    // Car engine 
    public float accelerationForce = 300f;
    public float breakingForce = 3000f;
    private float presentBreakForce = 0f;
    private float presentAcceleration = 0f;

    //Steering wheel 
    public float wheelsTorque = 35f;
    public float presentTurnAngle = 0f;



    private void Update() {
        MoveCar();
        CarSteering();
        ApplyBreaks();
    }


    private void MoveCar() 
    {
        //AWD
        frontLeftWheelCollider.motorTorque = presentAcceleration;
        frontRightWheelCollider.motorTorque = presentAcceleration;
        backLeftWheelCollider.motorTorque = presentAcceleration;
        BackRightWheelCollider.motorTorque = presentAcceleration;

        presentAcceleration = accelerationForce * Input.GetAxis("Vertical");
    }

    private void CarSteering()
    {
        presentTurnAngle = wheelsTorque * Input.GetAxis("Horizontal"); 
        frontLeftWheelCollider.steerAngle = presentTurnAngle;
        frontRightWheelCollider.steerAngle = presentTurnAngle;
        SteeringWheels(frontLeftWheelCollider, frontLeftWheelTransform);
        SteeringWheels(frontRightWheelCollider, frontRightWheelTransform);
        SteeringWheels(backLeftWheelCollider, backLeftWheelTransform);
        SteeringWheels(BackRightWheelCollider, backRightWheelTransform);
    }

    void SteeringWheels(WheelCollider WC, Transform WT)
    {
        Vector3 position; 
        Quaternion rotation; 
        WC.GetWorldPose(out position, out rotation);
        WT.position = position;
        WT.rotation = rotation;
    }

    public void ApplyBreaks()
    {
        if(Input.GetKey(KeyCode.Space))
            presentBreakForce = breakingForce;
        else
            presentBreakForce = 0f; 
        
        frontLeftWheelCollider.brakeTorque = presentBreakForce;
        frontRightWheelCollider.brakeTorque = presentBreakForce;
        backLeftWheelCollider.brakeTorque = presentBreakForce;
        BackRightWheelCollider.brakeTorque = presentBreakForce;
    }

}
