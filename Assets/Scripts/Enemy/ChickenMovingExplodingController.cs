using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChickenMovingExplodingController : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public UnityEvent onEnemyDeath;
    public GameObject keyMapper;
    public GameObject projectileSpawner;
    Dictionary<string, Vector3> keyMap;
    List<Vector3> keyList;

    public GameObject character;

    HashSet<string> spriteNames = new HashSet<string> {"Body"};
    List<SpriteRenderer> sprites = new List<SpriteRenderer> {};
    private int health;
    private Vector3 start;
    private Vector3 end;
    private float speed;
    private bool explode;

    // Start is called before the first frame update
    void Start()
    {
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        keyList = new List<Vector3>(keyMap.Values);
        foreach (Transform sprite in transform.parent.Find("Sprite")) {
            if (spriteNames.Contains(sprite.name)) {
                sprites.Add(sprite.GetComponent<SpriteRenderer>());
            }
        };
        character = GameObject.Find("Character");
        projectileSpawner = transform.parent.Find("ProjectileBoneExplodeSpawner").gameObject;
        health = enemyConstants.chickenMovingHealth;
        start = transform.position;
        keyList.Remove(start);
        end = keyList[Random.Range(0, keyList.Count)];
        speed = 2.0f;
        explode = false;
        StartCoroutine(moveEnemyLoop());
    }

    IEnumerator moveEnemyLoop() {
        while (!explode) {
            start = transform.position;
            end = character.transform.position;
            moveEnemy(start, end);
            yield return null;
        }
    }

    void moveEnemy(Vector3 from, Vector3 to) {
        foreach (SpriteRenderer spriteRenderer in sprites) {
            spriteRenderer.flipX = from.x - to.x > 0 ? true : false;
        }
        Vector3 direction = (to - from).normalized;
        to = from + direction;
        float fracDist = Time.deltaTime * speed;
        transform.parent.position = Vector3.Lerp(from, to, fracDist);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Explode() {
        yield return new WaitForSeconds(1.0f);
    }

    void OnTriggerEnter(Collider col) {
        if (health > 0 && col.gameObject.CompareTag("Character")) {
            health -= 1;
            if (health == 0) {
                explode = true;
                // projectileSpawner.GetComponent<ProjectileBoneExplodeSpawner>().spawnProjectiles();
                // StartCoroutine(Explode());
                onEnemyDeath.Invoke();
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
