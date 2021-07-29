using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner1_2 : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public GameObject keyMapper;
    public UnityEvent SpawnPowerup;
    Dictionary<string, Vector3> keyMap;
    List<Vector3> keyList;
    
    private GameObject character;
    private int[] spawnSequence;
    private int enemyCount;
    private int progress = 0;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        character = GameObject.Find("Character");
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        spawnSequence = enemyConstants.spawnSequence1_2;
        enemyCount = spawnSequence[progress];

        StartCoroutine(WaitForNextSpawn());
    }

    void spawnEnemies() {
        // Instantiate(enemyConstants.chickenStationaryPrefab, new Vector3(2,0,0), Quaternion.identity);
        for (int count = 0; count < spawnSequence[progress]; count++) {
            spawnEnemy();
        }
    }

    IEnumerator spawnClownMilkPair() {
        int index = Random.Range(0, keyList.Count);
        GameObject clownMilk1 = Instantiate(enemyConstants.clownMilkPrefab, keyList[index], Quaternion.identity);
        keyList.RemoveAt(index);
        
        index = Random.Range(0, keyList.Count);
        GameObject clownMilk2 = Instantiate(enemyConstants.clownMilkPrefab, keyList[index], Quaternion.identity);
        keyList.RemoveAt(index);
        
        var clownMilk1RedBallSpawner = clownMilk1.transform.Find("ProjectileRedBallSpawner").GetComponent<ProjectileRedBallSpawner>();
        clownMilk1RedBallSpawner.direction = (clownMilk2.transform.position - clownMilk1.transform.position).normalized;
        clownMilk1RedBallSpawner.endPosition = clownMilk2.transform.position;
        clownMilk1RedBallSpawner.shoot = true;

        var clownMilk2RedBallSpawner = clownMilk2.transform.Find("ProjectileRedBallSpawner").GetComponent<ProjectileRedBallSpawner>();
        clownMilk2RedBallSpawner.direction = (clownMilk1.transform.position - clownMilk2.transform.position).normalized;
        clownMilk2RedBallSpawner.endPosition = clownMilk1.transform.position;
        clownMilk2RedBallSpawner.shoot = false;

        clownMilk1RedBallSpawner.otherPair = clownMilk2RedBallSpawner;
        clownMilk2RedBallSpawner.otherPair = clownMilk1RedBallSpawner;
        yield return null;
    }

    void spawnEnemy() {
        StartCoroutine(spawnClownMilkPair());
    }

    IEnumerator spawnEnemiesWithDelay() {
        for (int count = 0; count < spawnSequence[progress]; count++) {
            spawnEnemy();
            yield return new WaitForSeconds(0.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitForNextSpawn() {
        yield return new WaitForSeconds(1);
        if (character != null) {
            keyList = new List<Vector3>(keyMap.Values);
            keyList.Remove(character.transform.position);
            if (enemyCount == 10) {
                StartCoroutine(spawnEnemiesWithDelay());
            }
            else {
                spawnEnemies();
            }
            SpawnPowerup.Invoke();
        }
    }

    public void enemyDead() {
        enemyCount -= 1;
        if (enemyCount == 0) {
            if (progress < spawnSequence.Length-1) {
                progress += 1;
                enemyCount = spawnSequence[progress];
                StartCoroutine(WaitForNextSpawn());
            }
        }
    }
}
