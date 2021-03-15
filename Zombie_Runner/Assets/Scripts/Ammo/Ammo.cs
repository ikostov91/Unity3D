using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AmmoScripts
{
    public class Ammo : MonoBehaviour
    {
        [SerializeField] AmmoSlot[] _ammoSlots;

        public int GetCurrentAmmo(AmmoType ammoType)
        {
            return this.GetAmmoSlot(ammoType)._ammoAmount;
        }

        public void ReduceCurrentAmmo(AmmoType ammoType)
        {
            this.GetAmmoSlot(ammoType)._ammoAmount -= 1;
        }

        public void IncreaseCurrentAmmo(AmmoType ammoType, int ammoAmmount)
        {
            this.GetAmmoSlot(ammoType)._ammoAmount += ammoAmmount;
        }

        private AmmoSlot GetAmmoSlot(AmmoType ammoType)
        {
            foreach (AmmoSlot slot in this._ammoSlots)
            {
                if (slot._ammoType == ammoType)
                {
                    return slot;
                }
            }

            return null;
        }
    }
}

