using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _enemyMovementPeriod = 1f;
    [SerializeField] private ParticleSystem _goalParticle;

    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        List<Waypoint> path = pathfinder.GetPath();
        this.StartCoroutine(this.FollowPath(path));
    }

    private IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            this.transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(this._enemyMovementPeriod);
        }

        this.SelfDestruct();
    }

    private void SelfDestruct()
    {
        var goalVfx = Instantiate(this._goalParticle, this.transform.position, Quaternion.identity);
        goalVfx.Play();

        float destroyDelay = goalVfx.main.duration;
        Destroy(goalVfx.gameObject, destroyDelay);
        Destroy(this.gameObject);
    }
}
