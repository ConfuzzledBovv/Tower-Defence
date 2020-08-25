using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    int towerLimit = 3;
    [SerializeField] Transform towerParent;
    [SerializeField] List<Tower> towerTypes = new List<Tower>();
    Queue<Tower> totalStandardTowers = new Queue<Tower>();
    Queue<Tower> totalDoubleTowers = new Queue<Tower>();
    Queue<Tower> totalSlowShotTowers = new Queue<Tower>();
    List<Queue<Tower>> totalTowersList = new List<Queue<Tower>>();
    [SerializeField] List<int> towerLimits = new List<int>();
    List<int> totalTowersCount = new List<int>();
    int currentTower;
    int count = 0;

    private void Start()
    {
        totalTowersList.Add(totalStandardTowers);
        totalTowersList.Add(totalDoubleTowers);
        totalTowersList.Add(totalSlowShotTowers);

        for (int i = 0; i < totalTowersList.Capacity; i++)
        {
            totalTowersCount.Add(0);
        }
        towerLimit = towerLimits[0];
        FindObjectOfType<TurretTotals>().OnGameStart(towerLimits);
    }

    public void AddTower(Waypoint baseWaypoint)
    {
        if(towerPrefab == null || towerLimit == 0)
        {
            return;
        }
        if (count < towerLimit)
        {
            InstantiateNewTower(baseWaypoint);
            count = totalTowersList[currentTower].Count;
        }
        else
        {
            MoveExistingTower(baseWaypoint);
        }
        SetText();
        UpdateStats();
    }
    private void MoveExistingTower(Waypoint baseWaypoint)
    {
        print("You have reached your max number of towers! ");

        Tower old = totalTowersList[currentTower].Dequeue();
        totalTowersList[currentTower].Enqueue(old);
        old.transform.position = baseWaypoint.transform.position;
        old.currentPosition.isTowerPlaced = false;
        old.currentPosition = baseWaypoint;
        old.currentPosition.isTowerPlaced = true;
    }

    private void InstantiateNewTower(Waypoint baseWaypoint)
    {
        Tower newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        newTower.transform.parent = towerParent;
        totalTowersList[currentTower].Enqueue(newTower);
        baseWaypoint.isTowerPlaced = true;
        newTower.currentPosition = baseWaypoint;
    }

    void SetText()
    {
        FindObjectOfType<TurretTotals>().SetText(count, towerLimit, currentTower);
    }

    bool ChangeTower(int ChangeToTower)
    {
        if (currentTower == ChangeToTower)
        {
            return false;
        }
        else
        {
            UpdateStats();
            return true;
        }
    }

    void UpdateStats()
    {
        totalTowersCount[currentTower] = count;
        towerLimits[currentTower] = towerLimit;
    }

    public void SetTower(int newTowerType)
    {
        print(newTowerType);
        if (ChangeTower(newTowerType) == true)
        {
            towerPrefab = towerTypes[newTowerType];
            towerLimit = towerLimits[newTowerType];
            count = totalTowersCount[newTowerType];
            currentTower = newTowerType;
        }
    }
}
