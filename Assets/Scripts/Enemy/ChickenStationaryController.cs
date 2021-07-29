using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChickenStationaryController : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public UnityEvent onEnemyDeath;
    private int health;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        health = enemyConstants.chickenStationaryHealth;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Character"))
        {
            health -= 1;
            Debug.Log("damaged by character!");
            if (health == 0)
            {
                audioSource.PlayOneShot(audioSource.clip);
                onEnemyDeath.Invoke();
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }
}
