using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestEnemy();
    }
    void FindClosestEnemy()
    {
        float distancetoClosestEnemy = Mathf.Infinity;
        EnemyHandler closestEnemy = null;
        EnemyHandler[] allEnemies = GameObject.FindObjectsOfType<EnemyHandler>();
        foreach( EnemyHandler currentEnemeny in allEnemies)
        {
            float distancetoEnemy = (currentEnemeny.transform.position - this.transform.position).sqrMagnitude;
            if(distancetoEnemy < distancetoClosestEnemy)
            {
                distancetoClosestEnemy = distancetoEnemy;
                closestEnemy = currentEnemeny;
            }
        }
        Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
    }
}
