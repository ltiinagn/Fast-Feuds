using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnemySpawner1_1 : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public GameConstants gameConstants;
    public UnityEvent startNextDialogue;
    public GameObject keyMapper;
    Dictionary<string, Vector3> keyMap;
    List<Vector3> keyList;
    
    private GameObject character;
    private int[][] spawnSequence;
    private int enemyCount;
    private int progress0 = 0;
    private int progress1 = 0;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        character = GameObject.Find("Character");
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        spawnSequence = enemyConstants.spawnSequence1_1;
        enemyCount = spawnSequence[progress0][progress1];
    }

    void spawnEnemies() {
        // Instantiate(enemyConstants.chickenStationaryPrefab, new Vector3(2,0,0), Quaternion.identity);
        for (int count = 0; count < spawnSequence[progress0][progress1]; count++) {
            spawnEnemy();
        }
    }

    void spawnEnemy() {
        int index = Random.Range(0, keyList.Count);
        if (progress0 == 0) {
            Instantiate(enemyConstants.chickenStationaryPrefab, keyList[index], Quaternion.identity);
        }
        else if (progress0 == 1) {
            Instantiate(enemyConstants.chickenMovingPrefab, keyList[index], Quaternion.identity);
        }
        else {
            Instantiate(enemyConstants.chickenThrowingPrefab, keyList[index], Quaternion.identity);
        }
        keyList.RemoveAt(index);
    }

    IEnumerator spawnEnemiesWithDelay() {
        for (int count = 0; count < spawnSequence[progress0][progress1]; count++) {
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
        }
    }

    public void startNextSpawn() {
        enemyCount = spawnSequence[progress0][progress1];
        StartCoroutine(WaitForNextSpawn());
    }

    IEnumerator waitForStartNextDialogue() {
        yield return new WaitForSeconds(1);
        startNextDialogue.Invoke();
    }

    public void enemyDead() {
        enemyCount -= 1;
        if (enemyCount == 0) {
            if (progress1 < spawnSequence[progress0].Length - 1) {
                progress1 += 1;
                enemyCount = spawnSequence[progress0][progress1];
                StartCoroutine(WaitForNextSpawn());
            }
            else if (progress0 < spawnSequence.Length - 1) {
                StartCoroutine(waitForStartNextDialogue());
                progress0 += 1;
                progress1 = 0;
            }
        }
    }
}
