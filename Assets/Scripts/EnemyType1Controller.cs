using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyType1Controller : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public UnityEvent onEnemyDeath;
    public GameObject keyMapper;
    Dictionary<string, Vector3> keyMap;
    List<Vector3> keyList;

    private int health;
    private Vector3 start;
    private Vector3 end;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        keyList = new List<Vector3>(keyMap.Values);        

        health = enemyConstants.enemyType1Health;
        start = transform.position;
        keyList.Remove(start);
        end = keyList[Random.Range(0, keyList.Count)];
        speed = 2.0f;
        StartCoroutine(moveEnemyLoop());
    }

    IEnumerator moveEnemyLoop() {
        while (true) {
            yield return moveEnemy(start, end);
            yield return moveEnemy(end, start);
        }
    }

    IEnumerator moveEnemy(Vector3 from, Vector3 to) {
        float startTime;
        float distance;
        startTime = Time.time;
        distance = Vector3.Distance(from, to);

        while (true) {
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

    void OnTriggerEnter(Collider col) {
        health -= 1;
        Debug.Log("damaged by character!");
        if (health == 0) {
            onEnemyDeath.Invoke();
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
