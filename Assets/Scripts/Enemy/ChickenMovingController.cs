using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChickenMovingController : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public UnityEvent onEnemyDeath;
    public GameObject keyMapper;
    Dictionary<string, Vector3> keyMap;
    List<Vector3> keyList;

    HashSet<string> spriteNames = new HashSet<string> {"Body"};
    List<SpriteRenderer> sprites = new List<SpriteRenderer> {};
    private int health;
    private Vector3 start;
    private Vector3 end;
    private float speed;
    private Animator animator;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        keyList = new List<Vector3>(keyMap.Values);
        foreach (Transform sprite in gameObject.transform.parent.Find("Sprite"))
        {
            if (spriteNames.Contains(sprite.name))
            {
                sprites.Add(sprite.GetComponent<SpriteRenderer>());
            }
        };
        health = enemyConstants.chickenMovingHealth;
        start = transform.position;
        keyList.Remove(start);
        end = keyList[Random.Range(0, keyList.Count)];
        speed = 2.0f;
        animator = gameObject.transform.parent.Find("Sprite").GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(moveEnemyLoop());
    }

    IEnumerator moveEnemyLoop()
    {
        while (true)
        {
            yield return moveEnemy(start, end);
            yield return moveEnemy(end, start);
        }
    }

    IEnumerator moveEnemy(Vector3 from, Vector3 to)
    {
        float startTime;
        float distance;
        startTime = Time.time;
        distance = Vector3.Distance(from, to);
        int direction = from.x - to.x > 0 ? 0 : 1;
        foreach (SpriteRenderer spriteRenderer in sprites)
        {
            spriteRenderer.flipX = direction == 1 ? true : false;
        }

        while (true)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracDist = distCovered / distance;

            gameObject.transform.parent.gameObject.transform.position = Vector3.Lerp(from, to, fracDist);

            if (fracDist >= 1)
                yield break;

            yield return null;
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
                gameObject.GetComponent<BoxCollider>().enabled = false;
                Destroy(gameObject.transform.parent.gameObject, audioSource.clip.length);
            }
        }
    }
}
