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
    private BoxCollider swordCollider;
    List<SpriteRenderer> spriteDescendants = new List<SpriteRenderer> {};
    private Animator animator;
    // private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        health = enemyConstants.enemyHealth;
        state = 0;
        swordCollider = transform.parent.Find("Sprite/Body/LeftArm/Sword").GetComponent<BoxCollider>();
        swordCollider.enabled = false;
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
                foreach (Transform spriteGreatgrandchild in spriteGrandchild)
                {
                    if (null == spriteGreatgrandchild)
                    {
                        continue;
                    }
                    spriteDescendants.Add(spriteGreatgrandchild.GetComponent<SpriteRenderer>());
                }
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

    }

    IEnumerator attackPhase()
    {
        // to add animation
        yield return new WaitForSeconds(1.0f);
        swordCollider.enabled = true;
        yield return new WaitForSeconds(2.0f); // animator.GetCurrentAnimatorStateInfo(0).length);
        swordCollider.enabled = false;
        state = 2;
        animator.SetBool("isProvoked", false);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Character"))
        {
            if (state == 0)
            {
                state = 1;
                animator.SetBool("isProvoked", true);
                StartCoroutine(attackPhase());
            }
            else if (state == 2)
            {
                health -= 1;
                Debug.Log("damaged by character!");
                if (health == 0)
                {
                    onEnemyDeath.Invoke();
                    animator.SetTrigger("onDeath");
                    StartCoroutine(fadeIntoOblivion(spriteDescendants, 0, 1));
                    // audioSource.PlayOneShot(audioSource.clip);
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    Destroy(transform.parent.gameObject, 1); // audioSource.clip.length);
                }
            }
        }
    }
}
