using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] float _enemyMovementSpeed = 1f;
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
            Vector3 startPosition = this.transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = Time.deltaTime;

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * this._enemyMovementSpeed;
                this.transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
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
