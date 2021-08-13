using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossBigMikeController : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public GameConstants gameConstants;
    public GameObject keyMapper;
    public UnityEvent onBossDeath;
    public UnityEvent onBossHalfHealth;
    public UnityEvent onBossMinusHealth;
    public UnityEvent onBossAddHealth2;
    Dictionary<string, Vector3> keyMap;
    Dictionary<string, string> keyRowMap;
    List<Vector3> keyList;

    private GameObject character;
    private bool phaseChanged;
    private bool eating;
    private int health;
    private int halfHealth;
    private int quarterHealth;
    private float initialSpeed;
    private float speed;
    private Animator animator;
    // private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Character");
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        keyRowMap = keyMapper.GetComponent<KeyMapping>().keyRowMap;
        phaseChanged = false;
        eating = false;
        health = enemyConstants.bossBigMike_Health;
        halfHealth = health / 2;
        quarterHealth = health / 4;
        speed = 3.0f;
        initialSpeed = speed;
        animator = transform.parent.Find("Sprite").GetComponent<Animator>();

        StartCoroutine(loseHealthPeriodically());
        StartCoroutine(moveRandom());
    }

    IEnumerator moveRandom()
    {
        yield return new WaitForSeconds(1.5f);
        while (health > 1)
        {
            keyList = new List<Vector3>(keyMap.Values);
            keyList.Remove(character.transform.position);
            int index = Random.Range(0, keyList.Count);
            if (!phaseChanged && health <= halfHealth)
            {
                phaseChanged = true;
                onBossHalfHealth.Invoke();
                animator.SetTrigger("onPhaseChange");
                yield return new WaitForSeconds(1);
            }
            if (health <= quarterHealth)
            {
                speed = initialSpeed + Random.Range(0.0f, 3.0f);
            }
            yield return move(transform.position, keyList[index]);
        }
    }

    IEnumerator move(Vector3 from, Vector3 to)
    {
        // bool moveRight = from.x - to.x < 0 ? true : false;
        // if (moveRight != faceRight)
        // {
        //     faceRight = !faceRight;
        //     sprite.Rotate(new Vector3(0, 180, 0));
        // }

        float startTime = Time.time;
        float fracDist = 0;
        float distance = Vector3.Distance(from, to);
        float distCovered;
        if (distance > 0)
        {
            while (fracDist < 1)
            {
                if (eating)
                {
                    yield return new WaitForSeconds(0.5f);
                    startTime += 0.5f;
                    eating = false;
                }
                distCovered = (Time.time - startTime) * speed;
                fracDist = distCovered / distance;
                transform.parent.position = Vector3.Lerp(from, to, fracDist);
                yield return null;
            }
        }
    }

    IEnumerator moveFinal(Vector3 from, Vector3 to)
    {
        float upSpeed = 10.0f;
        float startTime = Time.time;
        float fracDist = 0;
        Vector3 fromUp = new Vector3(from.x, from.y + 10, from.z);
        float distance = Vector3.Distance(from, fromUp);

        while (fracDist < 1)
        {
            float distCovered = (Time.time - startTime) * upSpeed;
            fracDist = distCovered / distance;
            transform.parent.position = Vector3.Lerp(from, fromUp, fracDist);
            yield return null;
        }

        startTime = Time.time;
        fracDist = 0;
        Vector3 toUp = new Vector3(to.x, to.y + 10, to.z);
        distance = Vector3.Distance(toUp, to);

        while (fracDist < 1)
        {
            float distCovered = (Time.time - startTime) * upSpeed;
            fracDist = distCovered / distance;
            transform.parent.position = Vector3.Lerp(toUp, to, fracDist);
            yield return null;
        }
    }

    IEnumerator loseHealthPeriodically()
    {
        while (health > 1) // not a typo, don't want boss to actually die, but making use of onEnemyDeath
        {
            yield return new WaitForSeconds(0.6f);
            health -= 1;
            onBossMinusHealth.Invoke();
        }
        onBossDeath.Invoke();
        GetComponent<Collider>().enabled = false;
        speed = 5.0f;
        yield return new WaitForSeconds(1.0f);
        yield return moveFinal(transform.position, new Vector3(11, 0, 9));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("EnemyCollider") || col.gameObject.CompareTag("ChickenMoving"))
        {
            eating = true;
            animator.SetTrigger("onEat");
            if (health < 99)
            {
                health += 2;
                onBossAddHealth2.Invoke();
            }
        }
    }
}
