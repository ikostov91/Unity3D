using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] private Tower _towerPrefab;
    [SerializeField] private int _towerLimit = 5;

    private Queue<Tower> _towersQueue = new Queue<Tower>();
    
    public void AddTower(Waypoint baseWaypoint)
    {
        int towersCount = this._towersQueue.Count;
        if (towersCount < _towerLimit)
        {
            this.InstantiateNewTower(baseWaypoint);
        }
        else
        {
            MoveExistingTower(baseWaypoint);
        }
    }

    private void InstantiateNewTower(Waypoint baseWaypoint)
    {
        Tower newTower = Instantiate(
            this._towerPrefab,
            baseWaypoint.transform.position,
            Quaternion.identity
        );
        
        baseWaypoint.IsPlaceable = false;

        // set the basewaypoint

        this._towersQueue.Enqueue(newTower);
    }

    private void MoveExistingTower(Waypoint baseWaypoint)
    {
        Tower tower = this._towersQueue.Dequeue();

        // set placeable flags

        // set the basewaypoint

        // put the old tower on top of the queue
        this._towersQueue.Enqueue(tower);
    }
}
