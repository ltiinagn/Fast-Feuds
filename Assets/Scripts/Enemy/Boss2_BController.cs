using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss2_BController : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public GameConstants gameConstants;
    public UnityEvent onBossHalfHealth;
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
        initialHealth = enemyConstants.boss2_B_Health;
        health = initialHealth;
        phase = 1;
        speed = 8.0f;
        foreach (Transform sprite in gameObject.transform.parent.Find("Sprite")) {
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

    IEnumerator moveBossMain() {
        foreach (string[] name in enemyConstants.spawnKey2_B) {
            foreach (string c in name) {
                GetComponent<Collider>().enabled = false;
                yield return moveBoss(transform.position, keyMap[c]);
                GetComponent<Collider>().enabled = true;
                damaged = false;
                while (!damaged) {
                    yield return null;
                }
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(0.5f);
        }
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

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.CompareTag("ProjectileCollider")) {
            col.gameObject.SendMessage("SetInactive");
            damaged = true;
            health -= 1;
            if (phase == 1 && health <= initialHealth / 2) {
                phase = 2;
                onBossHalfHealth.Invoke();
            }
            if (health == 1) {
                Debug.Log("entering last hurrah");
                GetComponent<Collider>().enabled = false;
            }
        }
    }
}
