using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.WeaponScripts
{
    public class WeaponSwitcher : MonoBehaviour
    {
        [SerializeField] private int _currentWeapon = 0;
        private Weapon[] _weapons;

        void Start()
        {
            this.GetWeapons();
            this.SetWeaponActive();
        }

        void Update()
        {
            if (!PauseHandler.GamePaused)
            {
                int previousWeapon = this._currentWeapon;

                this.ProcessKeyInput();
                this.ProcessScrollWheel();

                if (previousWeapon != this._currentWeapon)
                {
                    SetWeaponActive();
                }
            }
        }

        private void ProcessKeyInput()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                this._currentWeapon = 0;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                this._currentWeapon = 1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                this._currentWeapon = 2;
            }
        }

        private void ProcessScrollWheel()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                float sign = Mathf.Sign(scroll);
                this._currentWeapon += (int)(1 * sign);
                if (this._currentWeapon >= this._weapons.Length)
                {
                    this._currentWeapon = 0;
                }
                else if (this._currentWeapon < 0)
                {
                    this._currentWeapon = this._weapons.Length - 1;
                }
            }
        }

        private void GetWeapons()
        {
            this._weapons = this.GetComponentsInChildren<Weapon>();
        }

        private void SetWeaponActive()
        {
            int weaponIndex = 0;

            foreach (Weapon weapon in this._weapons)
            {
                bool isCurrentWeapon = weaponIndex == _currentWeapon;
                weapon.gameObject.SetActive(isCurrentWeapon);
                weaponIndex++;
            }
        }
    }
}
