using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _hitPoints = 100f;

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        this._hitPoints -= damage;
        if (this._hitPoints <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
}