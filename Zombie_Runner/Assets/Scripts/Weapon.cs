using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera _fpsCamera;
    [SerializeField] private float _range = 100f;
    [SerializeField] private float _damage = 17f;
    [SerializeField] private float _shootingDelay = 0.15f;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private GameObject _hitEffect;

    private Coroutine _shootingCoroutine;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            this._shootingCoroutine = this.StartCoroutine(this.Shoot());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            this.StopCoroutine(this._shootingCoroutine);
        }
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            this.PlayMuzzleFlash();
            this.AddWeaponRecoil();
            this.ProcessRayCast();

            yield return new WaitForSeconds(this._shootingDelay);
        }
    }

    private void PlayMuzzleFlash()
    {
        this._muzzleFlash.Play();
    }

    private void AddWeaponRecoil()
    {
        // Visual only for now
        var recoilObject = GetComponent<Recoil>();
        recoilObject.recoil += 0.1f;
    }

    private void ProcessRayCast()
    {
        RaycastHit hit;
        Vector3 rayDirection = this._fpsCamera.transform.forward;
        bool hitSomething = Physics.Raycast(this._fpsCamera.transform.position, rayDirection, out hit, this._range);
        if (hitSomething)
        {
            this.CreateHitImpact(hit);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null)
            {
                return;
            }
            target.TakeDamage(this._damage);
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(this._hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.1f);
    }
}
