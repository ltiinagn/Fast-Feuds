using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MuffinPinkController : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public GameObject character;
    public UnityEvent onEnemyDeath;
    private Transform spriteParent;
    private int health;
    private bool faceRight = true;
    List<SpriteRenderer> spriteDescendants = new List<SpriteRenderer> {};
    private Animator animator;
    // private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Character");
        health = enemyConstants.enemyHealth;
        spriteParent = transform.parent.gameObject.transform;
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
        // audioSource = GetComponent<AudioSource>();
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
        if (character.transform.position.x - transform.position.x > 0 != faceRight)
        {
            faceRight = !faceRight;
            spriteParent.Rotate(new Vector3(0, 0, 180));
        }
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
                StartCoroutine(fadeIntoOblivion(spriteDescendants, 0, 1));
                // audioSource.PlayOneShot(audioSource.clip);
                transform.parent.Find("StrawberryChipSpawner").gameObject.SetActive(false);
                gameObject.GetComponent<BoxCollider>().enabled = false;
                Destroy(transform.parent.gameObject, 1); // audioSource.clip.length);
            }
        }
    }
}
