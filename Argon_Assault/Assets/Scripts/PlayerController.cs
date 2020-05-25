using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In meters per second (ms^-1)")]
    [SerializeField] private float _controlSpeed = 8f;
    [SerializeField] private GameObject[] _guns;

    [Header("Maximum Movement Range")]
    [SerializeField] private float _maximumRangeX = 5f;
    [SerializeField] private float _maximumRangeY = 3.2f;

    [Header("Screen-position Based")]
    [SerializeField] private float _pitchFactor = -5f;
    [SerializeField] private float _yawFactor = 6f;

    [Header("Control-throw Based")]
    [SerializeField] private float _controlPitchFactor = -30f;
    [SerializeField] private float _controlRollFactor = -20f;

    private float _xThrow;
    private float _yThrow;

    private bool _isAlive = true;

    void Update()
    {
        if (this._isAlive)
        {
            this.ProcessTranslation();
            this.ProcessRotation();
            this.ProcessFiring();
        }  
    }

    private void ProcessTranslation()
    {
        this._xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        this._yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = this._xThrow * this._controlSpeed * Time.deltaTime;
        float rawXPos = this.gameObject.transform.localPosition.x + xOffset;

        float yOffset = this._yThrow * this._controlSpeed * Time.deltaTime;
        float rawYPos = this.gameObject.transform.localPosition.y + yOffset;

        float x = Mathf.Clamp(rawXPos, -this._maximumRangeX, this._maximumRangeX);
        float y = Mathf.Clamp(rawYPos, -this._maximumRangeY, this._maximumRangeY);
        float z = this.gameObject.transform.localPosition.z;
        Vector3 newShipPosition = new Vector3(x, y, z);

        this.gameObject.transform.localPosition = newShipPosition;
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = this.gameObject.transform.localPosition.y * this._pitchFactor;
        float pitchDueToControlThrow = this._yThrow * this._controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = this.gameObject.transform.localPosition.x * this._yawFactor;

        float rollDueToControlThrow = this._xThrow * this._controlRollFactor;
        float roll = this.gameObject.transform.localRotation.z + rollDueToControlThrow;

        this.gameObject.transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            this.ActivateGuns(true);
        }
        else
        {
            this.ActivateGuns(false);
        }
    }

    private void ActivateGuns(bool fire)
    {
        foreach (GameObject gun in this._guns)
        {
            var emission = gun.GetComponent<ParticleSystem>().emission;
            emission.enabled = fire;
        }
    }

    private void OnPlayerDeath() // Called by string reference
    {
        this._isAlive = false;
    }
}
