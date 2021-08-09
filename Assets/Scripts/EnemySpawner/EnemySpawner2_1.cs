using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnemySpawner2_1 : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public GameConstants gameConstants;
    public UnityEvent startNextDialogue;
    public UnityEvent SpawnPowerup;
    public GameObject keyMapper;
    Dictionary<string, Vector3> keyMap;
    List<Vector3> keyList;

    private GameObject character;
    private int[][] spawnSequence;
    private int enemyTotal;
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
        spawnSequence = enemyConstants.spawnSequence2_1;
        enemyTotal = spawnSequence[progress0][progress1];
    }

    void spawnEnemies() {
        for (int count = 0; count < spawnSequence[progress0][progress1]; count++) {
            spawnEnemy();
        }
    }

    void spawnEnemy() {
        int index = Random.Range(0, keyList.Count);
        if (progress0 == 0) {
            if (progress1 <= 4) {
                Instantiate(enemyConstants.muffinWhitePrefab, keyList[index], Quaternion.identity);
            }
            else if (progress1 <= 9) {
                Instantiate(enemyConstants.muffinBluePrefab, keyList[index], Quaternion.identity);
            }
            else {
                Instantiate(enemyConstants.muffinPinkPrefab, keyList[index], Quaternion.identity);
            }
            enemyCount += 1;
        }
        else if (progress0 == 1) {
            Instantiate(enemyConstants.chocolateCakePrefab, keyList[index], Quaternion.identity);
        }
        else if (progress0 == 2) {
            Instantiate(enemyConstants.donutPrefab, keyList[index], Quaternion.identity);
        }
        // else if (progress0 == 3) {
        //     Instantiate(enemyConstants.friesPrefab, keyList[index], Quaternion.identity);
        // }
        if (progress0 != 1) {
            keyList.RemoveAt(index);
        }
    }

    IEnumerator spawnEnemiesWithDelay() {
        int spawnAt = Random.Range(0, spawnSequence[progress0][progress1]);
        for (int count = 0; count < spawnSequence[progress0][progress1]; count++) {
            spawnEnemy();
            if (spawnAt == count && progress0 > 0) {
                SpawnPowerup.Invoke();
            }
            if (enemyCount < 4) {
                yield return new WaitForSeconds(0.5f);
            }
            else {
                while (enemyCount >= 4) {
                    yield return null;
                }
                yield return new WaitForSeconds(0.5f);
            }
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
            if (enemyTotal == 10) {
                StartCoroutine(spawnEnemiesWithDelay());
            }
            else {
                spawnEnemies();
            }
        }
    }

    public void startNextSpawn() {
        enemyTotal = spawnSequence[progress0][progress1];
        StartCoroutine(WaitForNextSpawn());
    }

    IEnumerator waitForStartNextDialogue() {
        yield return new WaitForSeconds(1);
        startNextDialogue.Invoke();
    }

    public void enemyDead() {
        if (enemyCount > 0) {
            enemyCount -= 1;
        }
        enemyTotal -= 1;
        if (enemyTotal == 0) {
            if (progress1 < spawnSequence[progress0].Length - 1) {
                progress1 += 1;
                enemyTotal = spawnSequence[progress0][progress1];
                StartCoroutine(WaitForNextSpawn());
            }
            else if (progress0 < spawnSequence.Length - 1) {
                StartCoroutine(waitForStartNextDialogue());
                progress0 += 1;
                progress1 = 0;
            }
            else if (progress0 == spawnSequence.Length - 1) { // Last dialogue after all enemies killed
                StartCoroutine(waitForStartNextDialogue());
            }
        }
    }
}
