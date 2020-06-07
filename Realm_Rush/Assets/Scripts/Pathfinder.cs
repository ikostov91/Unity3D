using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private Dictionary<Vector2Int, Waypoint> _grid = new Dictionary<Vector2Int, Waypoint>();
    private Queue<Waypoint> _waypointsQueue = new Queue<Waypoint>();

    [SerializeField] private Waypoint _startWaypoint, _endWaypoint;
    private Waypoint _searchCenter;

    private bool _isRunning = true;

    private Vector2Int[] _directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    void Start()
    {
        this.LoadBlocks();
        this.ColorStartAndEndBlocks();
        this.FindPath();
    }

    private void LoadBlocks()
    {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            Vector2Int gridPosition = waypoint.GetGridPosition();
            if (this._grid.ContainsKey(gridPosition))
            {
                Debug.LogError($"Skipping overlapping block - {waypoint}");
                continue;
            }

            this._grid.Add(gridPosition, waypoint);
        }
    }

    private void ColorStartAndEndBlocks()
    {
        this._startWaypoint.SetTopColor(Color.green);
        this._endWaypoint.SetTopColor(Color.red);
    }

    private void FindPath()
    {
        this._waypointsQueue.Enqueue(this._startWaypoint);

        while (this._waypointsQueue.Count > 0 && this._isRunning)
        {
            this._searchCenter = this._waypointsQueue.Dequeue();
            this._searchCenter.IsExplored = true;

            this.StopIfEndpointFound();
 
            this.ExploreNeighbours();
        }

        // todo workout path
        Debug.Log("Finished pathfiding?");
    }

    private void StopIfEndpointFound()
    {
        if (this._searchCenter == this._endWaypoint)
        {
            Debug.Log("End waypoint found.");
            this._isRunning = false;
        }
    }

    private void ExploreNeighbours()
    {
        if (!this._isRunning)
        {
            return;
        }

        Debug.Log($"Searching from {this._searchCenter.GetGridPosition()}");
        foreach (Vector2Int direction in this._directions)
        {
            Vector2Int neightbourCoordinates = this._searchCenter.GetGridPosition() + direction;

            try
            {
                this.QueueNewNeighbour(neightbourCoordinates);
            }
            catch (Exception)
            {
                // do nothing
            }
        }
    }

    private void QueueNewNeighbour(Vector2Int coordinates)
    {
        Waypoint neighbour = this._grid[coordinates];

        if (neighbour.IsExplored || this._waypointsQueue.Contains(neighbour))
        {
            return;
        }

        // neighbour.SetTopColor(Color.blue);
        neighbour.ExploredFrom = this._searchCenter;
        Debug.Log($"Enqueuing nehghbour - {neighbour.GetGridPosition()}");
        this._waypointsQueue.Enqueue(neighbour);
    }
}
