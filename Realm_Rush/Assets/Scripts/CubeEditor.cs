using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    private Waypoint _waypoint;

    void Awake()
    {
        this._waypoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        this.SnapToNewPosition();
        this.UpdateLabel();
    }

    private void SnapToNewPosition()
    {
        int gridSize = this._waypoint.GetGridSize();
        Vector2 gridPosition = this._waypoint.GetGridPosition();
        this.transform.position =
            new Vector3(gridPosition.x * gridSize, 0f, gridPosition.y * gridSize);
    }

    private void UpdateLabel()
    {
        TextMesh textMesh = this.GetComponentInChildren<TextMesh>();
        string label = $"{this._waypoint.GetGridPosition().x},{this._waypoint.GetGridPosition().y}";
        textMesh.text = label;
        this.gameObject.name = label;
    }
}
