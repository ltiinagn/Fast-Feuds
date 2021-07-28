using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner1_3 : MonoBehaviour
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
        spawnSequence = enemyConstants.spawnSequence1_3;
        enemyCount = spawnSequence[progress];

        StartCoroutine(WaitForNextSpawn());
    }

    void spawnEnemies() {
        // Instantiate(enemyConstants.chickenStationaryPrefab, new Vector3(2,0,0), Quaternion.identity);
        for (int count = 0; count < spawnSequence[progress]; count++) {
            spawnEnemy();
        }
    }

    void spawnEnemy() {
        int index = Random.Range(0, keyList.Count);
        Instantiate(enemyConstants.bigMacPrefab, keyList[index], Quaternion.identity);
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
            // SpawnPowerup.Invoke();
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
