using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] float secondsBetweenSpawns = 1.0f;
    [SerializeField] EnemyMovement enemyToSpawn;
    [SerializeField] int totalEnemies;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemies()
    {
        for(int i = 0; i < totalEnemies; i++)
        {
            Instantiate(enemyToSpawn, new Vector3(-20, 0, 0), Quaternion.identity);
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }

    }
}
 