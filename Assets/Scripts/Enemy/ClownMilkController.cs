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
    List<SpriteRenderer> spriteDescendants = new List<SpriteRenderer> {};
    private Animator animator;
    List<SpriteRenderer> otherPairSpriteDescendants = new List<SpriteRenderer> {};
    private Animator otherPairAnimator;
    // private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        health = enemyConstants.enemyHealth;
        foreach (Transform spriteChild in transform.parent.Find("Sprite"))
        {
            spriteDescendants.Add(spriteChild.GetComponent<SpriteRenderer>());
            foreach (Transform spriteGrandchild in spriteChild)
            {
                if (null == spriteGrandchild)
                {
                    continue;
                }
                spriteDescendants.Add(spriteGrandchild.GetComponent<SpriteRenderer>());
            }
        };
        animator = transform.parent.Find("Sprite").GetComponent<Animator>();
        foreach (Transform spriteChild in otherPair.transform.Find("Sprite"))
        {
            otherPairSpriteDescendants.Add(spriteChild.GetComponent<SpriteRenderer>());
            foreach (Transform spriteGrandchild in spriteChild)
            {
                if (null == spriteGrandchild)
                {
                    continue;
                }
                otherPairSpriteDescendants.Add(spriteGrandchild.GetComponent<SpriteRenderer>());
            }
        }
        otherPairAnimator = otherPair.transform.Find("Sprite").GetComponent<Animator>();
        // audioSource = GetComponent<AudioSource>();
        int direction = Random.Range(0, 2);
        if (direction == 1)
        {
            transform.parent.gameObject.transform.Rotate(new Vector3(0, 0, 180));
            transform.parent.Find("Sprite/Body").GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    IEnumerator fadeIntoOblivion(List<SpriteRenderer> sprites, float startTime, float totalDuration)
    {
        float counter = 0;
        float fadeDuration = totalDuration - startTime;

        yield return new WaitForSeconds(startTime);

        while (counter < fadeDuration)
        {
            counter += Time.deltaTime;
            foreach (SpriteRenderer spriteRenderer in sprites)
            {
                spriteRenderer.material.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, counter / fadeDuration));
            }
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.CompareTag("Character"))
        {
            health -= 1;
            Debug.Log("damaged by character!");
            if (health == 0)
            {
                onEnemyDeath.Invoke();
                animator.SetTrigger("onDeath");
                otherPairAnimator.SetTrigger("onDeath");
                StartCoroutine(fadeIntoOblivion(spriteDescendants, 0, 1));
                StartCoroutine(fadeIntoOblivion(otherPairSpriteDescendants, 0, 1));
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
