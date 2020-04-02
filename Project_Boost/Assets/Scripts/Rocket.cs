using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Parameters
    [SerializeField] private float _thrustPower = 1f;
    [SerializeField] private float _rotationSpeed = 1f;

    // Components
    private Rigidbody _myRigidBody;
    
    // Start is called before the first frame update
    void Start()
    {
        this._myRigidBody = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        this.ProcessInput();
    }

    private void ProcessInput()
    {
        // GetKey - continious press
        // GetKeyDown - one time press
        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            this._myRigidBody.AddRelativeForce(Vector3.up);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("rotate to the left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("rotate to the right");
        }
    }
}
