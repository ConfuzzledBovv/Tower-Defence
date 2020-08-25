using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    // Paramaters of each tower
    [SerializeField] Transform objectToPan;
    [SerializeField] float attackRange = 30.0f;
    [SerializeField] ParticleSystem projectileParticle;
    [SerializeField] AudioClip towerShootSfx;
    [SerializeField] float damage = 1.0f;

    // State of each tower
    Transform target;

    public Waypoint currentPosition;

    // Update is called once per frame
    void Update()
    {
        SetTargetEnemy();
        if(target)
        {
            Lookat();
            Shoot();
        }
        else
        {
            InRange(false);
        }
    }

    private void SetTargetEnemy()
    {
        EnemyDamage[] sceneEnemies = FindObjectsOfType<EnemyDamage>();

        if(sceneEnemies.Length == 0)
        {
            return;
        }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach(EnemyDamage enemies in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, enemies.transform);
        }

        target = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        if(Vector3.Distance(gameObject.transform.position, transformA.transform.position) < Vector3.Distance(gameObject.transform.position, transformB.position))
        {
            return transformA;
        }
        else
        {
            return transformB;
        }
    }

    public void Lookat()
    {
        objectToPan.LookAt(target);
    }

    private void Shoot()
    {
        if(Vector3.Distance(target.transform.position, objectToPan.transform.position) > attackRange)
        {
            InRange(false);
            return;
        }
        InRange(true);
        // Shoot!
    }

    private void InRange(bool isActive)
    {
        ParticleSystem.EmissionModule emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;
    }

    public float ReturnDamageDealt()
    {
        return damage;
    }
}
