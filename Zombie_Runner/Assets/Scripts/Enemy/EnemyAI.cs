using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.Constants;
using Assets.Scripts.PlayerScripts;

namespace Assets.Scripts.EnemyScripts
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private float _chaseRange = 5f;
        [SerializeField] private float _turnSpeed = 5f;

        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private float _distanceToTarget = Mathf.Infinity;
        private bool _isPlayerVisible = true;
        private bool _isEnemyProvoked = false;
        private EnemyHealth _health;
        private Transform _target;

        void Start()
        {
            this._navMeshAgent = GetComponent<NavMeshAgent>();
            this._animator = GetComponent<Animator>();
            this._health = GetComponent<EnemyHealth>();
            this._target = FindObjectOfType<PlayerHealth>().transform;
        }

        void Update()
        {
            if (this._health.IsDead)
            {
                this._navMeshAgent.enabled = false;
                this.enabled = false;
            }

            this.FollowPlayerIfInRange();
            this.MakeSounds();
        }

        void FixedUpdate()
        {
            this.CheckIfPlayerIsVisible();
        }

        private void FollowPlayerIfInRange()
        {
            this._distanceToTarget = Vector3.Distance(this._target.position, this.transform.position);
            if (this._isEnemyProvoked)
            {
                this.EngageTarget();
            }
            else if (this._distanceToTarget <= this._chaseRange && this._isPlayerVisible)
            {
                this._isEnemyProvoked = true;
            }
        }

        private void MakeSounds()
        {
            var state = this._animator.GetBool("Attack");
        }

        public void OnDamageTaken()
        {
            this._isEnemyProvoked = true;
        }

        private void EngageTarget()
        {
            this.FaceTarget();

            if (this._distanceToTarget > this._navMeshAgent.stoppingDistance && this._isPlayerVisible)
            {
                this.ChaseTarget();
            }

            if (this._distanceToTarget <= this._navMeshAgent.stoppingDistance && this._isPlayerVisible)
            {
                this.AttackTarget();
            }
        }

        private void AttackTarget()
        {
            this._animator.SetBool(AnimationConstants.Attack, true);
        }

        private void ChaseTarget()
        {
            this._animator.SetBool(AnimationConstants.Attack, false);
            this._animator.SetTrigger(AnimationConstants.Move);
            this._navMeshAgent.SetDestination(this._target.position);
        }

        private void FaceTarget()
        {
            Vector3 direction = (this._target.transform.position - this.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * this._turnSpeed);
        }

        private void CheckIfPlayerIsVisible()
        {
            //RaycastHit hit;
            //Vector3 rayDirection = this._target.position - this.transform.position;
            //if (Physics.Raycast(this.transform.position, rayDirection, out hit))
            //{
            //    this._isPlayerVisible = hit.transform == this._target;
            //}

            // Temporary solution because when in enslosed space in Asylum, hit.transform equals to the zombie transform for some reason
            this._isPlayerVisible = true;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, this._chaseRange);
        }
    }
}
