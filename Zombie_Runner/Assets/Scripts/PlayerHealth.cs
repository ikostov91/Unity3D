using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _hitPoints = 100f;

    public void TakeDamage(float damage)
    {
        this._hitPoints -= damage;
        if (this._hitPoints <= 0)
        {
            Debug.Log("DEAD!!!");
        }
    }
}
