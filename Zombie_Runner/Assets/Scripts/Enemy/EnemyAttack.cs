using UnityEngine;
using Assets.Scripts.PlayerScripts;

namespace Assets.Scripts.EnemyScripts
{
    public class EnemyAttack : MonoBehaviour
    {
        private PlayerHealth _target;
        [SerializeField] private float _damage = 40f;

        void Start()
        {
            this._target = FindObjectOfType<PlayerHealth>();
        }

        public void AttackHitEvent()
        {
            if (this._target == null)
            {
                return;
            }

            this._target.TakeDamage(this._damage);
            this._target.GetComponent<DisplayDamage>().ShowDamageCanvas();
        }
    }
}
