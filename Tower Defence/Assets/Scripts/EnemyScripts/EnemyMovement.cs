using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{


    [SerializeField] float waitTime = 5f;
    [SerializeField] ParticleSystem goalParticle;

    // Start is called before the first frame update
    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        List<Waypoint> path = pathfinder.GetPath();
        StartCoroutine(MoveToWaypoints(path));
    }

    IEnumerator MoveToWaypoints(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            if(waypoint == path[path.Count - 1])
            {
                // Enemy wasn't destroying quick enough to avoid extra score being added  
                GetComponentInParent<EnemyDamage>().SetCanGiveScore(false);
            }
            yield return new WaitForSeconds(waitTime);
        }
        
        EnemyReachedTheEnd();
    }

    public void EnemyReachedTheEnd()
    {
        ParticleSystem ps = Instantiate(goalParticle, transform.position, Quaternion.identity);
        ps.Play();
        Destroy(gameObject);
        float delay = ps.main.duration;
        Destroy(ps.gameObject, delay);
    }

}
