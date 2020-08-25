using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }
    void Update()
    {
        SnapToGrid();

        UpdateLabel();
    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();

        transform.position = new Vector3(waypoint.GetPosition().x * gridSize, 0.0f, waypoint.GetPosition().y * gridSize);
    }

    private void UpdateLabel()
    {
        TextMesh textMeshCoords = GetComponentInChildren<TextMesh>();
        textMeshCoords.text = waypoint.GetPosition().x+ ", " + waypoint.GetPosition().y;
        gameObject.name = "Cube " + textMeshCoords.text;
    }
}
