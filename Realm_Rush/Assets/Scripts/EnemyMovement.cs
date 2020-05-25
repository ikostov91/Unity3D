using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private List<Waypoint> _path; 

    // Start is called before the first frame update
    void Start()
    {
        this.StartCoroutine(this.FollowPath());
    }

    private IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in this._path)
        {
            this.transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1f);
        }
    }
}
