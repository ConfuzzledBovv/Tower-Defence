using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTextHealth : MonoBehaviour
{
    [SerializeField] Text enemyText;
    [SerializeField] EnemyDamage enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemyText.text = "Health: " + enemy.ReturnHealth().ToString();
    }
}
