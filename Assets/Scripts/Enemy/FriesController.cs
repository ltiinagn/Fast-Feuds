using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FriesController : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public UnityEvent onEnemyDeath;
    private int health;
    private int state; // 0 invulnerable, 1 attacking, 2 weakened
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        health = enemyConstants.enemyHealth;
        state = 0;
        // audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator attackPhase() {
        // to add animation
        yield return new WaitForSeconds(5);
        state = 2;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Character"))
        {
            if (state == 0) {
                state = 1;
                StartCoroutine(attackPhase());
            }
            else if (state == 2) {
                health -= 1;
                Debug.Log("damaged by character!");
                if (health == 0)
                {
                    // audioSource.PlayOneShot(audioSource.clip);
                    onEnemyDeath.Invoke();
                    Destroy(gameObject.transform.parent.gameObject);
                }
            }
        }
    }
}
