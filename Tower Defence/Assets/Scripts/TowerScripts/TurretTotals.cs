using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurretTotals : MonoBehaviour
{

    [SerializeField] List<Text> towerLimitTxt = new List<Text>();
    [SerializeField] Text levelNumber;
    
    public void OnGameStart(List<int> limits)
    {
        for(int i = 0; i < towerLimitTxt.Capacity; i++)
        {
            towerLimitTxt[i].text = "0 / " + limits[i].ToString();
            levelNumber.text = SceneManager.GetActiveScene().name.ToString();
        }
    }

    public void SetText(int count, int limit, int currentTower)
    {
        towerLimitTxt[currentTower].text = count.ToString() + " / " + limit.ToString();
    }
}
