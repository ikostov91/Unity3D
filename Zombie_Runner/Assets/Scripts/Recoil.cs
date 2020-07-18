using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    [SerializeField] public float recoil = 0.0f;

    [SerializeField] private float _maxRecoil_x = -20f;
    [SerializeField] private float _maxRecoil_y = 20f;
    [SerializeField] private float _maxRecoil_z = 20f;
    [SerializeField] private float _recoilSpeed = 2f;
    [SerializeField] private float _maxTrans_x = 1.0f;
    [SerializeField] private float _maxTrans_z = -1.0f;

    void Update()
    {
        if (this.recoil > 0f)
        {
            var maxRecoil = Quaternion.Euler(
                this.transform.localRotation.x,
                -90f, // Y rotation of weapon in editor
                Random.Range(this.transform.localRotation.z, this._maxRecoil_z)
            );

            this.transform.localRotation = Quaternion.Slerp(
                this.transform.localRotation,
                maxRecoil,
                Time.deltaTime * this._recoilSpeed
            );

            this.recoil -= Time.deltaTime;
        }
        else
        {
            this.recoil = 0f;

            var minRecoil = Quaternion.Euler(
                this.transform.localRotation.x,
                -90f, // Y rotation of weapon in editor
                this.transform.localRotation.z
            );

            this.transform.localRotation = Quaternion.Slerp(
                this.transform.localRotation,
                minRecoil,
                Time.deltaTime * this._recoilSpeed / 2
            );
        }
    }
}
