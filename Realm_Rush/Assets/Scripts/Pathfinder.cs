using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private Dictionary<Vector2Int, Waypoint> _grid = new Dictionary<Vector2Int, Waypoint>();
    private Queue<Waypoint> _waypointsQueue = new Queue<Waypoint>();

    private Queue<Vector2Int> queue = new Queue<Vector2Int>(); // to remove

    private Dictionary<Vector2Int, Waypoint> _explored = new Dictionary<Vector2Int, Waypoint>();

    [SerializeField] private Waypoint _startWaypoint, _endWaypoint;

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
            Waypoint currentWaypoint = this._waypointsQueue.Dequeue();
            currentWaypoint.IsExplored = true;

            this.StopIfEndpointFound(currentWaypoint);
 
            this.ExploreNeighbours(currentWaypoint);
        }

        // todo workout path
        Debug.Log("Finished pathfiding?");
    }

    private void StopIfEndpointFound(Waypoint waypoint)
    {
        if (waypoint == this._endWaypoint)
        {
            Debug.Log("End waypoint found.");
            this._isRunning = false;
        }
    }

    private void ExploreNeighbours(Waypoint centerWaypoint)
    {
        if (!this._isRunning)
        {
            return;
        }

        Debug.Log($"Searching from {centerWaypoint.GetGridPosition()}");
        foreach (Vector2Int direction in this._directions)
        {
            Vector2Int neightboutCoordinates = centerWaypoint.GetGridPosition() + direction;

            try
            {
                this.QueueNewNeighbour(neightboutCoordinates);
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

        if (neighbour.IsExplored)
        {
            return;
        }

        neighbour.SetTopColor(Color.blue);
        Debug.Log($"Enqueuing nehghbour - {neighbour.GetGridPosition()}");
        this._waypointsQueue.Enqueue(neighbour);
    }
}
