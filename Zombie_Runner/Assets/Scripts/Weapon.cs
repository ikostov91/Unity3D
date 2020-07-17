using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera _fpsCamera;
    [SerializeField] private float _range = 100f;
    [SerializeField] private float _damage = 17f;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private GameObject _hitEffect;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            this.Shoot();
        }
    }

    private void Shoot()
    {
        this.PlayMuzzleFlash();
        this.ProcessRayCast();
    }

    private void PlayMuzzleFlash()
    {
        this._muzzleFlash.Play();
    }

    private void ProcessRayCast()
    {
        RaycastHit hit;
        Vector3 rayDirection = this._fpsCamera.transform.forward;
        bool hitSomething = Physics.Raycast(this._fpsCamera.transform.position, rayDirection, out hit, this._range);
        if (hitSomething)
        {
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null)
            {
                return;
            }
            target.TakeDamage(this._damage);

            this.CreateHitImpact(hit);
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(this._hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1f);
    }
}
