using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int hitPoints = 10;
    void Start()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        Hit();
        if(hitPoints <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        Destroy(gameObject);
    }

    void Hit()
    {
        hitPoints--;
    }
}
