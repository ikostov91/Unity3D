using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _hitPoints = 100f;

    private bool _isDead = false;

    public bool IsDead => this._isDead;

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        this._hitPoints -= damage;
        if (this._hitPoints <= 0f)
        {
            this.Die();
        }
    }

    private void Die()
    {
        if (this._isDead)
        {
            return;
        }

        this._isDead = true;
        GetComponent<Animator>().SetTrigger(AnimationConstants.Die);
    }
}