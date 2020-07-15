using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int _hitPoints = 40;
    [SerializeField] private ParticleSystem _hitParticlesPrefab;
    [SerializeField] private ParticleSystem _deathParticlesPrefab;
    [SerializeField] private AudioClip _enemyHitSfx;
    [SerializeField] private AudioClip _deathSfx;

    private AudioSource _myAudioSource;

    private void Start()
    {
        this._myAudioSource = GetComponent<AudioSource>();
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
        this._hitParticlesPrefab.Play();
        this._myAudioSource.PlayOneShot(this._enemyHitSfx);
    }

    private void DestroyEnemy()
    {
        var deathVfx = Instantiate(this._deathParticlesPrefab, this.transform.position, Quaternion.identity);
        deathVfx.Play();

        float destroyDelay = deathVfx.main.duration;
        Destroy(deathVfx.gameObject, destroyDelay);
        AudioSource.PlayClipAtPoint(this._deathSfx, Camera.main.transform.position);
        Destroy(this.gameObject);
    }    
}
