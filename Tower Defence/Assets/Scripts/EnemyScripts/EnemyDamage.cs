using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDamage : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float hitPoints = 10.0f;
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] AudioClip damageSfx;
    [SerializeField] AudioClip deathSfx;
    bool isDying = false;
    AudioSource audioSource;
    bool CanSetScore = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnParticleCollision(GameObject other)
    {
        Tower attacker = other.GetComponentInParent<Tower>();
        Hit(attacker.ReturnDamageDealt());
        if (hitPoints <= 0.0f && isDying == false)
        {
            isDying = true;
            KillEnemy();
        }
    }

    public void KillEnemy()
    {
        ParticleSystem ps = Instantiate(deathParticles, transform.position, Quaternion.identity);
        Camera camera = FindObjectOfType<Camera>();
        AudioSource.PlayClipAtPoint(deathSfx, camera.transform.position);
        ps.Play();
        Destroy(gameObject);
        Score();
        float delay = ps.main.duration;
        Destroy(ps.gameObject, delay);

    }

    void Score()
    {
        if (CanSetScore == false)
        {
            // Score was exceeding the maximum due to enemy not destroying quick enough.
            return;             
        }
        if (SceneManager.GetActiveScene().name == "Endless Mode")
        {
            CanSetScore = false;
            FindObjectOfType<EndlessMode>().AddScore();
        }
        else
        {
            CanSetScore = false;
            FindObjectOfType<EnemiesSpawner>().AddScore();

        }

    }

    void Hit(float damage)
    {
        hitPoints -= damage;
        if(hitPoints > 0)
        {
            audioSource.PlayOneShot(damageSfx);
            hitParticles.Play();
        }
    }

    public float ReturnHealth()
    {
        return hitPoints;
    }

    public void SetCanGiveScore(bool boolean)
    {
        CanSetScore = boolean;
    }
}
