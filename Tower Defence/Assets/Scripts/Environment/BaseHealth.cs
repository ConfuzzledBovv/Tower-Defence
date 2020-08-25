using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BaseHealth : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] int healthDecrease = 1;
    [SerializeField] Text healthText;
    [SerializeField] AudioClip baseDamageSfx;
    public int startHealth;

    private void Start()
    {
        startHealth = health;
        healthText.text = health.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().PlayOneShot(baseDamageSfx);
        health -= healthDecrease;
        healthText.text = health.ToString();

        if(health <= 0)
        {
            EndGame();
        }
    }

    public int GetHealth()
    {
        return health;
    }

    private void EndGame()
    {
        SceneManager.LoadScene("EndScreen", LoadSceneMode.Single);
    }
}
