using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyType1Controller : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = enemyConstants.enemyType1_health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col) {
        health -= 1;
        Debug.Log("damaged by character!");
        if (health == 0) {
            Destroy(gameObject);
        }
    }
}
