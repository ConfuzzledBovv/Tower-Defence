using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public bool isExplored = false;
    public Waypoint parentWaypoint;
    const int gridSize = 10;
    Vector2Int gridPosition;
    public bool isPlaceable = true;
    public bool isTowerPlaced = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0) && isPlaceable && !isTowerPlaced)
        {
            FindObjectOfType<TowerFactory>().AddTower(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetGridSize()
    {
        return gridSize;
    }

    // X & Y are being used for 3D Coordinate system. In this case Y always equals 0 but the result of Y here is used for the Z coordinate
    public Vector2Int GetPosition()
    {
        return new Vector2Int(
            (Mathf.RoundToInt(transform.position.x / 10.0f)),
            (Mathf.RoundToInt(transform.position.z / 10.0f))
        );
    }
}
