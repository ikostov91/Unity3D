using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int _hitPoints = 40;
    [SerializeField] private ParticleSystem _hitParticlesPrefab;
    [SerializeField] private ParticleSystem _deathParticlesPrefab;

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
        this._hitParticlesPrefab.Play();
    }

    private void DestroyEnemy()
    {
        var deathVfx = Instantiate(this._deathParticlesPrefab, this.transform.position, Quaternion.identity);
        deathVfx.Play();
        Destroy(this.gameObject);
    }    
}
