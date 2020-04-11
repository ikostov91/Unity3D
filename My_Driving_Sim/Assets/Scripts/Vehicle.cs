using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField] private float _force = 15000f;
    [SerializeField] private float _torque = 15000f;
    [SerializeField] private float _wheelMotorTorque = 5000f;
    [SerializeField] private float _wheelBrakingTorque = 1500f;
    [SerializeField] private float _handbrakeTorque = 10000000f;
    [SerializeField] private float _steeringAngle = 25f;
    [SerializeField] private float _frontBrakeBias = 0.75f;
    [SerializeField] private float _rearBrakeBias = 0.25f;

    private Rigidbody _myRigidBody;

    [SerializeField] private WheelCollider _frontRightWheel;
    [SerializeField] private WheelCollider _frontLeftWheel;
    [SerializeField] private WheelCollider _rearRightWheel;
    [SerializeField] private WheelCollider _rearLeftWheel;

    //[SerializeField] private GameObject _frontRightVisualWheel;
    //[SerializeField] private GameObject _frontLeftVisualWheel;
    //[SerializeField] private GameObject _rearRightVisualWheel;
    //[SerializeField] private GameObject _rearLeftVisualWheel;

    // Start is called before the first frame update
    void Start()
    {
        this._myRigidBody = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        this.Accelerate();
        this.Brake();
        this.Steer();
        this.AnimateWheels();
        // this.ShowStats();
    }

    private void Accelerate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this._frontLeftWheel.motorTorque = this._wheelMotorTorque;
            this._frontRightWheel.motorTorque = this._wheelMotorTorque;
        }
        else
        {
            this._frontLeftWheel.motorTorque = 0f;
            this._frontRightWheel.motorTorque = 0f;
        }
    }

    private void Brake()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this._frontLeftWheel.brakeTorque = this._wheelBrakingTorque * this._frontBrakeBias;
            this._frontRightWheel.brakeTorque = this._wheelBrakingTorque * this._frontBrakeBias;
            this._rearLeftWheel.brakeTorque = this._wheelBrakingTorque * this._rearBrakeBias;
            this._rearRightWheel.brakeTorque = this._wheelBrakingTorque * this._rearBrakeBias;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            this._rearLeftWheel.brakeTorque = this._handbrakeTorque;
            this._rearRightWheel.brakeTorque = this._handbrakeTorque;
        }
        else
        {
            this._frontLeftWheel.brakeTorque = 0f;
            this._frontRightWheel.brakeTorque = 0f;
            this._rearLeftWheel.brakeTorque = 0f;
            this._rearRightWheel.brakeTorque = 0f;
        }
    }

    private void Steer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this._frontLeftWheel.steerAngle = -1 * this._steeringAngle;
            this._frontRightWheel.steerAngle = -1 * this._steeringAngle;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this._frontLeftWheel.steerAngle = this._steeringAngle;
            this._frontRightWheel.steerAngle = this._steeringAngle;
        }
        else
        {
            this._frontLeftWheel.steerAngle = 0f;
            this._frontRightWheel.steerAngle = 0f;
        }
    }

    private void AnimateWheels()
    {
        this.AnimateSingleWheel(this._frontLeftWheel);
        this.AnimateSingleWheel(this._frontRightWheel);
        this.AnimateSingleWheel(this._rearLeftWheel);
        this.AnimateSingleWheel(this._rearRightWheel);
    }

    private void AnimateSingleWheel(WheelCollider wheel)
    {
        if (wheel.transform.childCount == 0)
            return;

        Transform visualWheel = wheel.transform.GetChild(0);
        Vector3 position;
        Quaternion rotation;
        wheel.GetWorldPose(out position, out rotation);

        Quaternion newRotation = rotation * Quaternion.Euler(new Vector3(90, 90, 0));
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = newRotation;
    }

    private void ShowStats()
    {
        Debug.Log($"Wheel RPM = {this._rearLeftWheel.rpm}");
        Debug.Log($"Wheel brake torque = {this._rearLeftWheel.brakeTorque}");
    }
}
