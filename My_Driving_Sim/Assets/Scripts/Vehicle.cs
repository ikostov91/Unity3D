using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField] private float _force = 15000f;
    [SerializeField] private float _torque = 15000f;

    private Rigidbody _myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        this._myRigidBody = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            float x = this._force;
            float y = 0f;
            float z = 0f;
            Vector3 forceToAdd = new Vector3(x, y, z);
            this._myRigidBody.AddRelativeForce(forceToAdd);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            float x = 0f;
            float y = this._torque;
            float z = this._torque;
            Vector3 torqueToAdd = new Vector3(x, y, z);
            this._myRigidBody.AddRelativeTorque(torqueToAdd);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            float x = 0f;
            float y = -1 * this._torque;
            float z = -1 * this._torque;
            Vector3 torqueToAdd = new Vector3(x, y, z);
            this._myRigidBody.AddRelativeTorque(torqueToAdd);
        }
    }
}
