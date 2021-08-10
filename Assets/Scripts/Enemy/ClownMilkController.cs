using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClownMilkController : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public GameObject otherPair;
    public UnityEvent onEnemyDeath;
    private int health;
    private Animator animator;
    private Animator otherPairAnimator;
    // private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        health = enemyConstants.enemyHealth;
        int direction = Random.Range(0, 2);
        if (direction == 1) {
            transform.parent.gameObject.transform.Rotate(new Vector3(0, 0, 180));
            transform.parent.Find("Sprite/Body").GetComponent<SpriteRenderer>().flipX = true;
        }
        animator = transform.parent.Find("Sprite").GetComponent<Animator>();
        otherPairAnimator = otherPair.transform.Find("Sprite").GetComponent<Animator>();
        // audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.CompareTag("Character")) {
            health -= 1;
            Debug.Log("damaged by character!");
            if (health == 0) {
                onEnemyDeath.Invoke();
                animator.SetTrigger("onDeath");
                otherPairAnimator.SetTrigger("onDeath");
                // audioSource.PlayOneShot(audioSource.clip);
                gameObject.GetComponent<BoxCollider>().enabled = false;
                otherPair.transform.Find("BoxCollider").GetComponent<BoxCollider>().enabled = false;
                transform.parent.Find("ProjectileRedBallSpawner").gameObject.SetActive(false);
                otherPair.transform.Find("ProjectileRedBallSpawner").gameObject.SetActive(false);
                Destroy(otherPair, 1); // audioSource.clip.length);
                Destroy(transform.parent.gameObject, 1); // audioSource.clip.length);
            }
        }
    }
}
