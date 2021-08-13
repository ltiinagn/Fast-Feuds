using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossBuffCakeController : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public GameConstants gameConstants;
    public UnityEvent onBossHalfHealth;
    public UnityEvent onBossLastHurrah;
    public GameObject keyMapper;
    Dictionary<string, Vector3> keyMap;
    Dictionary<string, string> keyRowMap;

    HashSet<string> spriteNames = new HashSet<string> {"Body"};
    List<SpriteRenderer> sprites = new List<SpriteRenderer> {};

    private int initialHealth;
    private int health;
    private int phase;
    private bool damaged;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        keyRowMap = keyMapper.GetComponent<KeyMapping>().keyRowMap;
        initialHealth = enemyConstants.bossBuffCake_Health;
        health = initialHealth;
        phase = 1;
        speed = 8.0f;
        foreach (Transform sprite in transform.parent.Find("Sprite")) {
            if (spriteNames.Contains(sprite.name)) {
                sprites.Add(sprite.GetComponent<SpriteRenderer>());
            }
        };

        StartCoroutine(moveBossMain());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator bossAtKey(string c) {
        yield return moveBoss(transform.position, keyMap[c]);
        GetComponent<Collider>().enabled = true;
        damaged = false;
        while (!damaged) {
            yield return null;
        }
    }

    IEnumerator moveBossMain() {
        foreach (string[] name in enemyConstants.spawnKey2_B) {
            for (int i = 0; i < name.Length - 1; i++) {
                yield return bossAtKey(name[i]);
                yield return new WaitForSeconds(0.2f);
            }
            yield return bossAtKey(name[name.Length-1]);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator moveBoss(Vector3 from, Vector3 to)
    {
        float startTime;
        float distance;
        float distCovered;
        float fracDist = 0;
        startTime = Time.time;
        distance = Vector3.Distance(from, to);
        // bool moveRight = from.x - to.x < 0 ? true : false;
        // if (moveRight != faceRight && !dying)
        // {
        //     faceRight = !faceRight;
        //     spriteParent.Rotate(new Vector3(0, 0, 180));
        // }
        if (distance > 0) {
            while (fracDist < 1)
            {
                distCovered = (Time.time - startTime) * speed;
                fracDist = distCovered / distance;
                transform.parent.position = Vector3.Lerp(from, to, fracDist);
                yield return null;
            }
        }
    }

    IEnumerator moveFinal(Vector3 from, Vector3 to) {
        float upSpeed = 10.0f;
        float startTime = Time.time;
        float fracDist = 0;
        Vector3 fromUp = new Vector3(from.x, from.y + 10, from.z);
        float distance = Vector3.Distance(from, fromUp);

        while (fracDist < 1) {
            float distCovered = (Time.time - startTime) * upSpeed;
            fracDist = distCovered / distance;
            transform.parent.position = Vector3.Lerp(from, fromUp, fracDist);
            yield return null;
        }

        startTime = Time.time;
        fracDist = 0;
        Vector3 toUp = new Vector3(to.x, to.y + 10, to.z);
        distance = Vector3.Distance(toUp, to);

        while (fracDist < 1) {
            float distCovered = (Time.time - startTime) * upSpeed;
            fracDist = distCovered / distance;
            transform.parent.position = Vector3.Lerp(toUp, to, fracDist);
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        onBossLastHurrah.Invoke();
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.CompareTag("ProjectileCollider")) {
            GetComponent<Collider>().enabled = false;
            col.gameObject.SendMessage("SetInactive");
            damaged = true;
            health -= 1;
            if (phase == 1 && health <= initialHealth / 2) {
                phase = 2;
                onBossHalfHealth.Invoke();
            }
            if (health == 0) {
                GetComponent<Collider>().enabled = false;
                StartCoroutine(moveFinal(transform.position, new Vector3(11,0,9)));
            }
        }
    }
}
