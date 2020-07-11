using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] private Tower _towerPrefab;
    [SerializeField] private int _towerLimit = 5;
    [SerializeField] private Transform _towerParentTransform;

    private Queue<Tower> _towersQueue = new Queue<Tower>();
    
    public void AddTower(Waypoint baseWaypoint)
    {
        int towersCount = this._towersQueue.Count;
        if (towersCount < this._towerLimit)
        {
            this.InstantiateNewTower(baseWaypoint);
        }
        else
        {
            this.MoveExistingTower(baseWaypoint);
        }
    }

    private void InstantiateNewTower(Waypoint baseWaypoint)
    {
        Tower newTower = Instantiate(
            this._towerPrefab,
            baseWaypoint.transform.position,
            Quaternion.identity
        );
        newTower.BaseWaypoint = baseWaypoint;
        baseWaypoint.IsPlaceable = false;
        newTower.transform.parent = this._towerParentTransform.transform;

        this._towersQueue.Enqueue(newTower);
    }

    private void MoveExistingTower(Waypoint newBaseWaypoint)
    {
        Tower tower = this._towersQueue.Dequeue();

        tower.BaseWaypoint.IsPlaceable = true;
        tower.BaseWaypoint = newBaseWaypoint;
        newBaseWaypoint.IsPlaceable = false;

        tower.transform.position = newBaseWaypoint.transform.position;

        this._towersQueue.Enqueue(tower);
    }
}
