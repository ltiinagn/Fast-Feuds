using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DonutController : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public UnityEvent onEnemyDeath;
    public GameObject keyMapper;
    Dictionary<string, Vector3> keyMap;
    List<Vector3> keyList;

    private Transform spriteParent;
    private int health;
    private bool dying;
    private Vector3 start;
    private Vector3 end;
    private float speed;
    private bool faceRight = true;
    List<SpriteRenderer> spriteDescendants = new List<SpriteRenderer> {};
    private Animator animator;
    // private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        keyList = new List<Vector3>(keyMap.Values);
        dying = false;
        health = enemyConstants.enemyHealth;
        start = transform.position;
        keyList.Remove(start);
        end = keyList[Random.Range(0, keyList.Count)];
        speed = 2.0f;
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
        StartCoroutine(moveEnemyLoop());
    }

    IEnumerator moveEnemyLoop()
    {
        while (!dying)
        {
            animator.SetBool("isMoving", true);
            yield return moveEnemy(start, end);
            transform.gameObject.tag = "EnemyCollider";
            animator.SetBool("isMoving", false);
            yield return new WaitForSeconds(2.0f);
            animator.SetBool("isMoving", true);
            transform.gameObject.tag = "MeleeCollider";
            yield return moveEnemy(end, start);
            transform.gameObject.tag = "EnemyCollider";
            animator.SetBool("isMoving", false);
            yield return new WaitForSeconds(2.0f);
            animator.SetBool("isMoving", true);
            transform.gameObject.tag = "MeleeCollider";
        }
    }

    IEnumerator moveEnemy(Vector3 from, Vector3 to)
    {
        float startTime;
        float distance;
        float count = 1.5f;
        startTime = Time.time;
        distance = Vector3.Distance(from, to);
        bool moveRight = from.x - to.x < 0 ? true : false;
        if (moveRight != faceRight && !dying)
        {
            faceRight = !faceRight;
            spriteParent.Rotate(new Vector3(0, 0, 180));
        }

        while (!dying)
        {
            if (Time.time - startTime > count)
            {
                count += 1.5f;
                startTime += 2.0f;
                transform.gameObject.tag = "EnemyCollider";
                animator.SetBool("isMoving", false);
                yield return new WaitForSeconds(2.0f);
                animator.SetBool("isMoving", true);
                transform.gameObject.tag = "MeleeCollider";
            }
            float distCovered = (Time.time - startTime) * speed;
            float fracDist = distCovered / distance;

            transform.parent.position = Vector3.Lerp(from, to, fracDist);

            if (fracDist >= 1)
                yield break;

            yield return null;
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

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Character") && transform.gameObject.tag != "MeleeCollider")
        {
            health -= 1;
            if (health == 0)
            {
                dying = true;
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
