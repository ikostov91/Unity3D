using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.EnemyScripts
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private AudioClip _deathSound;
        [SerializeField] private AudioClip[] _damageTakenSounds;
        [SerializeField] private float _soundsVolume = 1.5f;

        [SerializeField] private float _hitPoints = 100f;

        private bool _isDead = false;

        public bool IsDead => this._isDead;

        public void TakeDamage(float damage)
        {
            BroadcastMessage(MessageConstants.OnDamageTaken);
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
            AudioSource.PlayClipAtPoint(this._deathSound, this.transform.position, this._soundsVolume);
        }
    }
}
