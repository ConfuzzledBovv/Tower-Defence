using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] Transform target;
    [SerializeField] float attackRange = 30.0f;
    [SerializeField] ParticleSystem projectileParticle;
    // Update is called once per frame
    void Update()
    {
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
}
