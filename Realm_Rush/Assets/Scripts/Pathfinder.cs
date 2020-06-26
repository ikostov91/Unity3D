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
    private List<Waypoint> _path = new List<Waypoint>();

    private bool _isRunning = true;

    private Vector2Int[] _directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    public List<Waypoint> GetPath()
    {
        if (this._path.Count == 0)
        {
            this.CalculatePath();
        }

        return this._path;
    }

    private void CalculatePath()
    {
        this.LoadBlocks();
        this.ColorStartAndEndBlocks();
        this.BreadthFirstSearch();
        this.CreatePath();
    }

    private void LoadBlocks()
    {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            Vector2Int gridPosition = waypoint.GetGridPosition();
            if (this._grid.ContainsKey(gridPosition))
            {
                continue;
            }

            this._grid.Add(gridPosition, waypoint);
        }
    }

    private void ColorStartAndEndBlocks()
    {
        // todo consider moving in the waypoint
        // this._startWaypoint.SetTopColor(Color.green);
        // this._endWaypoint.SetTopColor(Color.red);
    }

    private void BreadthFirstSearch()
    {
        this._waypointsQueue.Enqueue(this._startWaypoint);

        while (this._waypointsQueue.Count > 0 && this._isRunning)
        {
            this._searchCenter = this._waypointsQueue.Dequeue();
            this._searchCenter.IsExplored = true;

            this.StopIfEndpointFound();
 
            this.ExploreNeighbours();
        }
    }

    private void StopIfEndpointFound()
    {
        if (this._searchCenter == this._endWaypoint)
        {
            this._isRunning = false;
        }
    }

    private void ExploreNeighbours()
    {
        if (!this._isRunning)
        {
            return;
        }

        foreach (Vector2Int direction in this._directions)
        {
            Vector2Int neightbourCoordinates = this._searchCenter.GetGridPosition() + direction;

            if (this._grid.ContainsKey(neightbourCoordinates))
            {
                this.QueueNewNeighbour(neightbourCoordinates);
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
        this._waypointsQueue.Enqueue(neighbour);
    }

    private void CreatePath()
    {
        this.AddWaypointsToPath();
        this.ReversePath();
    }

    private void AddWaypointsToPath()
    {
        this._path.Add(this._endWaypoint);

        Waypoint previous = this._endWaypoint.ExploredFrom;
        while (previous != this._startWaypoint)
        {
            this._path.Add(previous);
            previous = previous.ExploredFrom;
        }

        this._path.Add(this._startWaypoint);
    }

    private void ReversePath()
    {
        List<Waypoint> reversedPath = new List<Waypoint>();

        int currentIndex = this._path.Count - 1;
        for (int i = currentIndex; i >= 0; i--)
        {
            reversedPath.Add(this._path[i]);
        }

        this._path = reversedPath;

        // or use _path.Reverse()
    }
}
