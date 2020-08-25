using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndlessMode : MonoBehaviour
{

    [Range(1.0f, 120f)]
    [SerializeField] List<float> secondsBetweenSpawns = new List<float>();
    [SerializeField] Transform enemyParent;
    [SerializeField] Text scoreText;
    [SerializeField] AudioClip spawnedEnemySfx;
    [SerializeField] List<EnemyMovement> enemiesToSpawn = new List<EnemyMovement>();
    [SerializeField] Text startDelay;
    [SerializeField] Text enemySpeedText;
    [SerializeField] Text enemySpawnedText;
    [SerializeField] Button enemySpawnedButton;
    [SerializeField] Button enemySpeedButton;
    [SerializeField] Button startWaveButton;
    int enemiesKilled = 0;
    int enemiesSpawned = 0;
    int totalEnemiesToSpawn = 0;
    EnemyMovement enemyPrefab;
    Queue<EnemyMovement> enemies = new Queue<EnemyMovement>();
    Queue<float> waveSpeeds = new Queue<float>();
    float currentWaveSpeed = 2.0f;
    bool delayRoutineStopped = true;
    Coroutine delayStart;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && delayRoutineStopped == false)
        {
            SkipDelay();
        }
    }

    void Start()
    {
        foreach(EnemyMovement eM in enemiesToSpawn)
        {
            enemies.Enqueue(eM);
        }
        waveSpeeds.Enqueue(1.0f);
        waveSpeeds.Enqueue(2.0f);
        waveSpeeds.Enqueue(3.0f);
        waveSpeeds.Enqueue(4.0f);
        waveSpeeds.Enqueue(5.0f);
        scoreText.text = enemiesKilled.ToString();
        delayStart = StartCoroutine(DelayStart(10));
    }

    IEnumerator SpawnEnemies(EnemyMovement prefab, float waveSpeed)
    {
        for (int i = 0; i < 20; i++)
        {
            GetComponent<AudioSource>().PlayOneShot(spawnedEnemySfx);
            EnemyMovement newEnemy = Instantiate(prefab, transform.position, Quaternion.identity);
            newEnemy.transform.parent = enemyParent;
            
            yield return new WaitForSeconds(waveSpeed);
        }
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
        ChangeEnemy();
        enemySpawnedButton.interactable = true;
        ChangeWaveSpeed();
        enemySpeedButton.interactable = true;
        startWaveButton.interactable = true;
    }

    public void AddScore()
    {
        enemiesKilled++;
        scoreText.text = enemiesKilled.ToString();
    }

    public void ChangeEnemy()
    {

        enemyPrefab = enemies.Dequeue();
        enemies.Enqueue(enemyPrefab);
        enemySpawnedText.text = enemyPrefab.name;
    }

    public void ChangeWaveSpeed()
    {
        currentWaveSpeed = waveSpeeds.Dequeue();
        waveSpeeds.Enqueue(currentWaveSpeed);
        enemySpeedText.text = currentWaveSpeed.ToString() + " x Slow";
    }

    public void SpawnWave()
    {
        StartCoroutine(SpawnEnemies(enemyPrefab, currentWaveSpeed));
    }

    public void ReturnToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    void SkipDelay()
    {
        StopCoroutine(delayStart);
        startDelay.text = "";
        delayRoutineStopped = true;
        BeginGame();
    }
}
