using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform _objectToMove;
    [SerializeField] private Transform _targetEnemy;
    [SerializeField] private ParticleSystem _weapon;

    [SerializeField] private float _attackRange = 10f;
    [SerializeField] private ParticleSystem _projectileParticle;

    // Update is called once per frame
    void Update()
    {
        if (this._targetEnemy)
        {
            this.LookAtEnemy();
            this.ShootAtEnemy();
        }
        else
        {
            this.Shoot(false);
        }
    }

    private void LookAtEnemy()
    {
        this._objectToMove.LookAt(this._targetEnemy);
    }

    private void ShootAtEnemy()
    {
        float distance = Vector3.Distance(this._targetEnemy.transform.position, this.gameObject.transform.position);
        if (distance <= this._attackRange)
        {
            this.Shoot(true);
        }
        else
        {
            this.Shoot(false);
        }
    }

    private void Shoot(bool isActive)
    {
        var emissionModule = this._projectileParticle.emission;
        emissionModule.enabled = isActive;
    }
}
