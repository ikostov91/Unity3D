using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Transform _objectToMove;
    [SerializeField] private float _attackRange = 10f;
    [SerializeField] private ParticleSystem _projectileParticle;

    [Header("State")]
    private Transform _targetEnemy;

    void Update()
    {
        this.SetTargetEnemy();

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

    private void SetTargetEnemy()
    {
        EnemyDamage[] sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if (sceneEnemies.Length == 0)
        {
            return;
        }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach (EnemyDamage testEnemy in sceneEnemies)
        {
            closestEnemy = this.GetClosestEnemy(closestEnemy, testEnemy.transform);
        }

        this._targetEnemy = closestEnemy;
    }

    private void LookAtEnemy()
    {
        this._objectToMove.LookAt(this._targetEnemy);
    }

    private void ShootAtEnemy()
    {
        float distance = this.GetDistance();
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

    private float GetDistance()
    {
        float distance = Vector3.Distance(this._targetEnemy.transform.position, this.gameObject.transform.position);
        return distance;
    }

    private Transform GetClosestEnemy(Transform closest, Transform test)
    {
        float distanceToClosest = Vector3.Distance(this.transform.position, closest.position);
        float distanceToTest = Vector3.Distance(this.transform.position, test.position);

        if (distanceToClosest < distanceToTest)
        {
            return closest;
        }

        return test;
    }
}
