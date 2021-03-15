using System.Collections;
using UnityEngine;

namespace Assets.Scripts.EnemyScripts
{
    public class EnemyPlaceholder : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _enemySounds;
        [SerializeField] private float _minDelay = 2f;
        [SerializeField] private float _maxDelay = 5f;

        void Start()
        {
            StartCoroutine(this.PlayRandomSounds());
        }

        private IEnumerator PlayRandomSounds()
        {
            while (true)
            {
                int randomIndex = Random.Range(0, this._enemySounds.Length);
                float randomDelay = Random.Range(this._minDelay, this._maxDelay);
                AudioSource.PlayClipAtPoint(this._enemySounds[randomIndex], this.transform.position);
                yield return new WaitForSeconds(randomDelay);
            }
        }
    }
}
