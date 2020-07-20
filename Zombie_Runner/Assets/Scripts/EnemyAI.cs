using UnityEngine;
using UnityEngine.AI;
using Constants;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _chaseRange = 5f;
    [SerializeField] private float _turnSpeed = 5f;

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private float _distanceToTarget = Mathf.Infinity;
    private bool _isPlayerVisible = true;
    private bool _isEnemyProvoked = false;

    void Start()
    {
        this._navMeshAgent = GetComponent<NavMeshAgent>();
        this._animator = GetComponent<Animator>();
    }

    void Update()
    {
        this.FollowPlayerIfInRange();
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

    private void EngageTarget()
    {
        this.FaceTarget();

        if (this._distanceToTarget >= this._navMeshAgent.stoppingDistance && this._isPlayerVisible)
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
        RaycastHit hit;
        Vector3 rayDirection = this._target.position - this.transform.position;
        if (Physics.Raycast(this.transform.position, rayDirection, out hit))
        {
            this._isPlayerVisible = hit.transform == this._target;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, this._chaseRange);
    }
}
