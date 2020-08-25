using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemiesSpawner : MonoBehaviour
{

    [Range(1.0f, 120f)]
    [SerializeField] List<float> secondsBetweenSpawns = new List<float>();
    [SerializeField] Transform enemyParent;
    [SerializeField] Text scoreText;
    [SerializeField] AudioClip spawnedEnemySfx;
    [SerializeField] AudioClip victorySfx;
    [SerializeField] List<EnemyMovement> enemiesToSpawn = new List<EnemyMovement>();
    [SerializeField] Text startDelay;
    int enemiesKilled = 0;
    List<bool> isWaveRunning = new List<bool>();
    bool enemiesAllDead = false;
    bool wavesHaveEnded = false;
    int enemiesSpawned = 0;
    int totalEnemiesToSpawn = 0;
    Coroutine delayStart;
    bool delayRoutineStopped = true;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && delayRoutineStopped == false)
        {
            SkipDelay();

        }
    }

    int AmountToSpawn(float spawnTime)
    {
        int maxEnemies = 100;
        if(spawnTime == 1)  
        {
            return maxEnemies;
        }
        return Mathf.RoundToInt(maxEnemies / spawnTime);
    }
    void Start()
    { 
        delayStart = StartCoroutine(DelayStart(10));
    }

    IEnumerator DelayStart(int delayTime)
    {
        delayRoutineStopped = false;
        for (int i = delayTime; i > 0; i--)
        {
            startDelay.text = "Wave \nstarts \n" + i.ToString();

            yield return new WaitForSeconds(1);
        }
        SkipDelay();
    }

    void BeginGame()
    {
        for (int i = 0; i < enemiesToSpawn.Capacity; i++)
        {
            StartCoroutine(SpawnEnemies(enemiesToSpawn[i], secondsBetweenSpawns[i], i));
        }
        scoreText.text = enemiesKilled.ToString();
    }

    IEnumerator SpawnEnemies(EnemyMovement enemyToSpawn, float timer, int routineNumber)
    {
        isWaveRunning.Add(true);
        int totalToSpawn = AmountToSpawn(timer);
        totalEnemiesToSpawn += totalToSpawn;
        print(totalEnemiesToSpawn);
        for (int i = 0; i < totalToSpawn; i++)
        {
            GetComponent<AudioSource>().PlayOneShot(spawnedEnemySfx);
            EnemyMovement newEnemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            newEnemy.transform.parent = enemyParent;
            yield return new WaitForSeconds(timer);
        }
        isWaveRunning[routineNumber] = false;

        if (isWaveRunning.Contains(true))
        {
            print("Routine #" + routineNumber + " has finished");
            yield return null;
        }
        else
        {
            print("All routines have finished");
            wavesHaveEnded = true;
            StartCoroutine(LevelFinished());
        }

    }

    public void AddScore()
    {
        enemiesKilled++;
        scoreText.text = enemiesKilled.ToString();
    }

    IEnumerator LevelFinished()
    {
        int levelFinished = 0;
        BaseHealth bh = FindObjectOfType<BaseHealth>();
        int healthDeduction = bh.startHealth - bh.GetHealth();
        int lastSpawnCount = 0;
        while (healthDeduction + enemiesKilled != totalEnemiesToSpawn && levelFinished != 6)
        {
            
            if(lastSpawnCount != healthDeduction + enemiesKilled)
            {
                lastSpawnCount = healthDeduction + enemiesKilled;
                levelFinished = 0;
            }
            else
            {   
                // Level was not ending on a rare occasion
                levelFinished++;
                Debug.Log(levelFinished);
            }
            healthDeduction = bh.startHealth - bh.GetHealth();
            yield return new WaitForSeconds(1);
        }
        AudioSource.PlayClipAtPoint(victorySfx, Camera.main.transform.position);
        for(int i = 5; i > 0; i--)
        {
            startDelay.text ="Returning\n to level select \n in " + i + " seconds";
            startDelay.fontSize = 32;
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene("LevelSelect");
    }

    void SkipDelay()
    {
        StopCoroutine(delayStart);
        startDelay.text = "";
        delayRoutineStopped = true;
        BeginGame();
    }

    public int ReturnScore()
    {
        return enemiesKilled;
    }

}
 