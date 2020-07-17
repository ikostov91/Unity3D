using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _chaseRange = 5f;

    private NavMeshAgent _navMeshAgent;
    private float _distanceToTarget = Mathf.Infinity;
    private bool _isPlayerVisible = true;
    private bool _isEnemyProvoked = false;

    void Start()
    {
        this._navMeshAgent = GetComponent<NavMeshAgent>();
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
        Debug.Log("Target is attacked!");
    }

    private void ChaseTarget()
    {
        this._navMeshAgent.SetDestination(this._target.position);
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
