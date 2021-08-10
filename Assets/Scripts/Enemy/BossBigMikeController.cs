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
    Dictionary<string, Vector3> keyMap;
    Dictionary<string, string> keyRowMap;
    List<Vector3> keyList;

    private GameObject character;
    private bool raised;
    private int health;
    private int halfHealth;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Character");
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        keyRowMap = keyMapper.GetComponent<KeyMapping>().keyRowMap;
        raised = false;
        health = enemyConstants.bossBigMike_Health;
        halfHealth = health / 2;
        speed = 3.0f;

        StartCoroutine(loseHealthPeriodically());
        StartCoroutine(moveRandom());
    }

    IEnumerator moveRandom() {
        while (health > 1) {
            keyList = new List<Vector3>(keyMap.Values);
            keyList.Remove(character.transform.position);
            int index = Random.Range(0, keyList.Count);
            if (!raised && health <= halfHealth) {
                onBossHalfHealth.Invoke();
            }
            yield return move(transform.position, keyList[index]);
        }
    }

    IEnumerator move(Vector3 from, Vector3 to) {
        // bool moveRight = from.x - to.x < 0 ? true : false;
        // if (moveRight != faceRight)
        // {
        //     faceRight = !faceRight;
        //     sprite.Rotate(new Vector3(0, 180, 0));
        // }

        float startTime = Time.time;
        float fracDist = 0;
        float distance = Vector3.Distance(from, to);

        while (fracDist < 1) {
            float distCovered = (Time.time - startTime) * speed;
            fracDist = distCovered / distance;
            gameObject.transform.parent.gameObject.transform.position = Vector3.Lerp(from, to, fracDist);
            yield return null;
        }
    }

    IEnumerator loseHealthPeriodically() {
        while (health > 1) { // not a typo, don't want boss to actually die, but making use of onEnemyDeath
            yield return new WaitForSeconds(1.0f);
            health -=1;
        }
        Debug.Log("entering last hurrah");
        onBossDeath.Invoke();
        GetComponent<Collider>().enabled = false;
        speed = 5.0f;
        yield return move(transform.position, keyMap["q"]);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(health);
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.CompareTag("EnemyCollider") || col.gameObject.CompareTag("ChickenMoving")) {
            health += 2;
        }
    }
}