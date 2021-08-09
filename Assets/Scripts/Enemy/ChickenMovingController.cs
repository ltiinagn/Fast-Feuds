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

    private Transform spriteParent;
    private int health;
    private bool dying;
    private Vector3 start;
    private Vector3 end;
    private bool faceRight = true;
    private float speed;
    private Animator animator;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        keyList = new List<Vector3>(keyMap.Values);
        dying = false;
        health = enemyConstants.chickenMovingHealth;
        start = transform.position;
        keyList.Remove(start);
        end = keyList[Random.Range(0, keyList.Count)];
        spriteParent = gameObject.transform.parent.gameObject.transform;
        speed = 2.0f;
        animator = gameObject.transform.parent.Find("Sprite").GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(moveEnemyLoop());
    }

    IEnumerator moveEnemyLoop()
    {
        while (!dying)
        {
            yield return moveEnemy(start, end);
            if (!dying) {
                yield return moveEnemy(end, start);
            }
        }
    }

    IEnumerator moveEnemy(Vector3 from, Vector3 to)
    {
        float startTime;
        float distance;
        startTime = Time.time;
        distance = Vector3.Distance(from, to);
        bool moveRight = from.x - to.x < 0 ? true : false;
        if (moveRight != faceRight)
        {
            faceRight = !faceRight;
            spriteParent.Rotate(new Vector3(0, 0, 180));
        }

        while (!dying)
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
                dying = true;
                onEnemyDeath.Invoke();
                animator.SetTrigger("onDeath");
                audioSource.PlayOneShot(audioSource.clip);
                gameObject.GetComponent<BoxCollider>().enabled = false;
                Destroy(gameObject.transform.parent.gameObject, audioSource.clip.length);
            }
        }
    }
}
