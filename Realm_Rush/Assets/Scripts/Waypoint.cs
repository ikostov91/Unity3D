using UnityEngine;

public class Waypoint : MonoBehaviour
{
    // Cube is 10 * 10 * 10m in size;
    private const int GridSize = 10;

    public bool IsExplored = false;
    public Waypoint ExploredFrom = null;

    private Color blueColor = Color.blue;
    private Color greyColor = Color.grey;

    public int GetGridSize() => GridSize;

    private void Update()
    {
        // Can set top color to blue if IsExplored
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
