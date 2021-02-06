using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int _ammoAmount = 10;

    [Serializable]
    private class AmmoSlot
    {
        public AmmoType _ammoType;
        public int _ammoAmount;
    }

    private void Update()
    {
        this.ProcessWeaponReload();
    }

    private void ProcessWeaponReload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            this._ammoAmount += 10;
        }
    }

    public int GetCurrentAmmo()
    {
        return this._ammoAmount;
    }

    public void ReduceCurrentAmmo()
    {
        this._ammoAmount -= 1;
    }    
}
