using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
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
            yield return new WaitForSeconds(1f);
        }
    }
}
