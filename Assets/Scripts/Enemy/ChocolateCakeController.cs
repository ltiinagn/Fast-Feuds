using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChocolateCakeController : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public UnityEvent onEnemyDeath;
    private int health;
    private Animator animator;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        health = enemyConstants.enemyHealth;
        animator = gameObject.transform.parent.Find("Sprite").GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        int direction = Random.Range(0, 2);
        if (direction == 1)
        {
            gameObject.transform.parent.Find("Sprite/Body").GetComponent<SpriteRenderer>().flipX = true;
        }
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
                onEnemyDeath.Invoke();
                animator.SetTrigger("onDeath");
                audioSource.PlayOneShot(audioSource.clip);
                gameObject.transform.parent.Find("ProjectileBigMacSauceSpawner").gameObject.SetActive(false);
                gameObject.GetComponent<BoxCollider>().enabled = false;
                Destroy(gameObject.transform.parent.gameObject, audioSource.clip.length);
            }
        }
    }
}
