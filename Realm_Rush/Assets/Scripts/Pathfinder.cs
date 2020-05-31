using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private Dictionary<Vector2Int, Waypoint> _grid = new Dictionary<Vector2Int, Waypoint>();
    private Dictionary<Vector2Int, Waypoint> _explored = new Dictionary<Vector2Int, Waypoint>();
    private Queue<Vector2Int> _positionsToExplore = new Queue<Vector2Int>();

    [SerializeField] private Waypoint _startPoint, _endPoint;

    private Vector2Int[] _directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    // Start is called before the first frame update
    void Start()
    {
        this.LoadBlocks();
        this.ColorStartAndEndBlocks();
        this.StartCoroutine(this.ExploreNeighbours());
    }

    // Update is called once per frame
    void Update()
    {
        
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
        this._startPoint.SetTopColor(Color.green);
        this._endPoint.SetTopColor(Color.red);
    }

    private IEnumerator ExploreNeighbours()
    {
        Vector2Int startingPosition = this._startPoint.GetGridPosition();
        this._positionsToExplore.Enqueue(startingPosition);

        while (this._positionsToExplore.Count != 0)
        {
            Vector2Int currentPosition = this._positionsToExplore.Dequeue();
            if (currentPosition == this._endPoint.GetGridPosition())
            {
                Debug.Log("Found end position.");
                break;
            }

            foreach (Vector2Int direction in this._directions)
            {
                Vector2Int positionToExplore = currentPosition + direction;
                if (!this._grid.ContainsKey(positionToExplore))
                {
                    continue;
                }

                if (!this._explored.ContainsKey(positionToExplore))
                {
                    this._positionsToExplore.Enqueue(positionToExplore);
                }
            }

            if (currentPosition != this._startPoint.GetGridPosition() &&
                currentPosition != this._endPoint.GetGridPosition())
            {
                Waypoint waypoint = this._grid[currentPosition];
                waypoint.SetTopColor(Color.blue);
            }

            if (!this._explored.ContainsKey(currentPosition))
            {
                this._explored.Add(currentPosition, this._grid[currentPosition]);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
