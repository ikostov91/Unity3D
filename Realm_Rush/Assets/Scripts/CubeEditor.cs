using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    private TextMesh _textMesh;
    private Vector3 _gridPosition;
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
        // Cube is 10 * 10 * 10m in size;
        int gridSize = this._waypoint.GetGridSize();
        this._gridPosition.x = Mathf.RoundToInt(this.transform.position.x / gridSize) * gridSize;
        this._gridPosition.z = Mathf.RoundToInt(this.transform.position.z / gridSize) * gridSize;
        this._gridPosition.y = 0f;

        this.transform.position = new Vector3(this._gridPosition.x, this._gridPosition.y, this._gridPosition.z);
    }

    private void UpdateLabel()
    {
        int gridSize = this._waypoint.GetGridSize();
        string label = $"{this.transform.position.x / gridSize},{this.transform.position.z / gridSize}";

        this._textMesh = this.GetComponentInChildren<TextMesh>();
        this._textMesh.text = label;
        this.gameObject.name = label;
    }
}
