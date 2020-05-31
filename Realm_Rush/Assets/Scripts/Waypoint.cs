using UnityEngine;

public class Waypoint : MonoBehaviour
{
    // Cube is 10 * 10 * 10m in size;
    private const int GridSize = 10;

    private Vector2Int _gridPosition;
    private bool _isExplored = false;

    public int GetGridSize() => GridSize;

    public bool IsExplored
    {
        get => this._isExplored;
        set => this._isExplored = value;
    }

    public Vector2Int GetGridPosition()
    {
        Vector2Int position = new Vector2Int(
            Mathf.RoundToInt(this.transform.position.x / GridSize),
            Mathf.RoundToInt(this.transform.position.z / GridSize)
        );
        return position;
    }

    public void SetTopColor(Color color)
    {
        MeshRenderer topMeshRenderer = this.transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }
}
