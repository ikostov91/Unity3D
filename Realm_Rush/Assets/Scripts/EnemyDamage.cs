using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int _hitPoints = 40;

    void Start()
    {
        
    }

    private void OnParticleCollision(GameObject otherCollider)
    {
        this.ProcessHit();
        if (this._hitPoints <= 0)
        {
            this.DestroyEnemy();
        }
    }

    private void ProcessHit()
    {
        this._hitPoints -= 1;
        Debug.Log($"Current hitpoints = {this._hitPoints}");
    }

    private void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }    
}
