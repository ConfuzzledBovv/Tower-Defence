using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint start, end;
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
    bool isRunning = true;
    Waypoint searchCenter;
    List<Waypoint> path = new List<Waypoint>();

    public List<Waypoint> GetPath()
    {
        if(path.Count == 0)
        {
            LoadBlocks();
            SetColourStartEnd();
            BreadthFirstSearch();
            CreatePath();
        }
        return path;
    }
    
    private void CreatePath()
    {
        path.Add(end);

        Waypoint parent = end.parentWaypoint;

        while(parent != start)
        {
            // Add intermediate waypoints
            path.Add(parent);
            parent = parent.parentWaypoint;
        }

        // Reverse the list to go the right way
        path.Add(start);
        path.Reverse();
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(start);
        while(queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            StopIfEndFound();
            Explore();
            searchCenter.isExplored = true;
        }
    }

    private void StopIfEndFound()
    {
        if(searchCenter == end)
        {
            isRunning = false;
        }
    }

    private void LoadBlocks()
    {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        foreach(Waypoint wp in waypoints)
        {
            if(grid.ContainsKey(wp.GetPosition()))
            {
                Debug.LogWarning("Overlapping block" + wp);
            }
            else
            {
                grid.Add(wp.GetPosition(), wp);
            }
        }
        print("Loaded " + grid.Count + " Blocks");
    }

    private void SetColourStartEnd()
    {
        end.SetTopColour(Color.green);
        start.SetTopColour(Color.red);
    }

    private void Explore()
    {
        if(!isRunning)
        {
            return;
        }
        foreach(Vector2Int direction in directions)
        {
            Vector2Int ExplorationCoordinates = searchCenter.GetPosition() + direction;
            if(grid.ContainsKey(ExplorationCoordinates))
            {
                QueueNewNeighbours(ExplorationCoordinates);
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int ExplorationCoordinates)
    {
        Waypoint neighbour = grid[ExplorationCoordinates];
        if(neighbour.isExplored || queue.Contains(neighbour))
        {
            // Do Nothing!
        }
        else
        {
            queue.Enqueue(neighbour);
            neighbour.parentWaypoint = searchCenter;
        }

    }
}
