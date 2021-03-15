using System.Collections;
using TMPro;
using UnityEngine;
using Assets.Scripts.AmmoScripts;
using Assets.Scripts.EnemyScripts;
using Assets.Scripts.Player;

namespace Assets.Scripts.WeaponScripts
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private AmmoType _ammoType;

        [SerializeField] private Camera _fpsCamera;
        [SerializeField] private ParticleSystem _muzzleFlash;
        [SerializeField] private GameObject _hitEffect;
        [SerializeField] private Ammo _ammoSlot;
        [SerializeField] private TextMeshProUGUI _ammoText;

        [SerializeField] private float _range = 100f;
        [SerializeField] private float _damage = 17f;
        [SerializeField] private float _shootingDelay = 0.15f;

        [SerializeField] private float _recoil = 0.0f;

        [SerializeField] private float _maxRecoil_x = -20f;
        [SerializeField] private float _recoilSpeed = 7f;
        [SerializeField] private float _maxTrans_x = 1.0f;
        [SerializeField] private float _maxTrans_z = -1.0f;

        [SerializeField] private AudioClip _fireSound;
        private AudioSource _audioSource;

        private bool _canShoot = true;

        private void OnEnable()
        {
            this._canShoot = true;
        }

        private void Start()
        {
            this._audioSource = FindObjectOfType<Camera>().GetComponent<AudioSource>();
        }

        void Update()
        {
            if (!PauseHandler.GamePaused)
            {
                if (Input.GetMouseButton(0) && this._ammoSlot.GetCurrentAmmo(this._ammoType) > 0 && this._canShoot)
                {
                    this.StartCoroutine(this.Shoot());
                }

                this.VisualizeWeaponRecoil();
                this.DisplayAmmo();

            }
        }

        private IEnumerator Shoot()
        {
            this._canShoot = false;

            this._audioSource.PlayOneShot(this._fireSound);
            this.PlayMuzzleFlash();
            this.AddRecoilAfterShot();
            this.ProcessRayCast();
            this._ammoSlot.ReduceCurrentAmmo(this._ammoType);
            yield return new WaitForSeconds(this._shootingDelay);

            this._canShoot = true;
        }

        private void AddRecoilAfterShot()
        {
            this._recoil += 0.1f;
        }

        private void PlayMuzzleFlash()
        {
            this._muzzleFlash.Play();
        }

        private void VisualizeWeaponRecoil()
        {
            if (this._recoil > 0.0f)
            {
                var maxRecoil = Quaternion.Euler(
                   Random.Range(this._fpsCamera.transform.localRotation.x, this._maxRecoil_x),
                    this._fpsCamera.transform.localRotation.y,
                    this._fpsCamera.transform.localRotation.z
                );

                this._fpsCamera.transform.localRotation = Quaternion.Slerp(
                    this._fpsCamera.transform.localRotation,
                    maxRecoil,
                    Time.deltaTime * this._recoilSpeed
                );

                this._recoil -= Time.deltaTime;
            }
            else
            {
                this._recoil = 0f;

                var minRecoil = Quaternion.Euler(
                    this._fpsCamera.transform.localRotation.x,
                    this._fpsCamera.transform.localRotation.y,
                    this._fpsCamera.transform.localRotation.z
                );

                this._fpsCamera.transform.localRotation = Quaternion.Slerp(
                    this._fpsCamera.transform.localRotation,
                    minRecoil,
                    Time.deltaTime * this._recoilSpeed / 2
                );
            }
        }

        private void DisplayAmmo()
        {
            this._ammoText.text = this._ammoSlot.GetCurrentAmmo(this._ammoType).ToString();
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
}
