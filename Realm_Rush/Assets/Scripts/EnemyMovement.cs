using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _enemyMovementDelay = 1f;
    [SerializeField] private List<Waypoint> _path; // todo remove

    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        this._path = pathfinder.GetPath();
        this.StartCoroutine(this.FollowPath(this._path));
    }

    private IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            this.transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(this._enemyMovementDelay);
        }
    }
}
