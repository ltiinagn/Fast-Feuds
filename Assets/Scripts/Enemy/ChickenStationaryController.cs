using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChickenStationaryController : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public UnityEvent onEnemyDeath;
    private int health;
    private Transform sprite;
    List<SpriteRenderer> spriteDescendants = new List<SpriteRenderer> {};
    private Animator animator;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        health = enemyConstants.chickenStationaryHealth;
        sprite = gameObject.transform.parent.Find("Sprite").transform;
        foreach (Transform spriteChild in gameObject.transform.parent.Find("Sprite"))
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
        animator = gameObject.transform.parent.Find("Sprite").GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator eatenByBoss(Vector3 bossPosition, float duration)
    {
        Vector3 originalPosition = sprite.position;
        Vector3 finalPosition = bossPosition;

        Vector3 originalScale = sprite.localScale;
        Vector3 finalScale = new Vector3(0.0f, 0.0f, 0.0f);

        float counter = 0;
        float fracTime = 0;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            fracTime = counter / duration;
            sprite.position = Vector3.Lerp(originalPosition, finalPosition, fracTime);
            sprite.localScale = Vector3.Lerp(originalScale, finalScale, fracTime);
            foreach (SpriteRenderer spriteRenderer in spriteDescendants)
            {
                spriteRenderer.material.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, fracTime));
            }
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Character") || col.gameObject.CompareTag("BossBigMike"))
        {
            health -= 1;
            if (health == 0)
            {
                onEnemyDeath.Invoke();
                if (col.gameObject.CompareTag("Character"))
                {
                    animator.SetTrigger("onDeath");
                }
                else if (col.gameObject.CompareTag("BossBigMike"))
                {
                    StartCoroutine(eatenByBoss(col.gameObject.transform.parent.gameObject.transform.position, 0.6f));
                }
                audioSource.PlayOneShot(audioSource.clip);
                gameObject.GetComponent<BoxCollider>().enabled = false;
                Destroy(gameObject.transform.parent.gameObject, 1); // audioSource.clip.length);
            }
        }
    }
}
