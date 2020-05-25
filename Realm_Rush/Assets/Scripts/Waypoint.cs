using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private const int GridSize = 10;

    private Vector2Int _gridPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetGridSize()
    {
        return GridSize;
    }

    public Vector2 GetGridPosition()
    {
        Vector2Int position = new Vector2Int(
            Mathf.RoundToInt(this.transform.position.x / GridSize) * GridSize,
            Mathf.RoundToInt(this.transform.position.z / GridSize) * GridSize
        );
        return position;
    }
}
