using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChickenThrowingController : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public UnityEvent onEnemyDeath;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = enemyConstants.chickenStationaryHealth;
        int direction = Random.Range(0, 2);
        if (direction == 1) {
            gameObject.transform.parent.Find("Sprite/Body").GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col) {
        health -= 1;
        Debug.Log("damaged by character!");
        if (health == 0) {
            onEnemyDeath.Invoke();
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
