using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnemySpawner0_T : MonoBehaviour
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
        spawnSequence = enemyConstants.spawnSequence0_T;
        enemyCount = spawnSequence[progress0][progress1];
    }

    void spawnEnemy() {
        int index = Random.Range(0, keyList.Count);
        Instantiate(enemyConstants.friesTutorialPrefab, keyList[index], Quaternion.identity);
        keyList.RemoveAt(index);
    }

    IEnumerator spawnEnemiesWithDelay() {
        if (progress0 == 1) {
            StartCoroutine(waitForStartNextDialogue());
        }
        for (int count = 0; count < spawnSequence[progress0][progress1]; count++) {
            spawnEnemy();
            yield return new WaitForSeconds(0.2f);
        }
        if (progress0 == 0) {
            yield return new WaitForSeconds(0.2f);
            SpawnPowerup.Invoke();
            yield return waitForStartNextDialogue();
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
            StartCoroutine(spawnEnemiesWithDelay());
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
                if (progress0 != 0) {
                    StartCoroutine(waitForStartNextDialogue());
                }
                progress0 += 1;
                progress1 = 0;
                if (progress0 == 1) {
                    enemyCount = spawnSequence[progress0][progress1];
                    StartCoroutine(WaitForNextSpawn());
                }
            }
            else if (progress0 == spawnSequence.Length -1) { // Last dialogue after all enemies killed
                StartCoroutine(waitForStartNextDialogue());
            }
        }
    }
}
