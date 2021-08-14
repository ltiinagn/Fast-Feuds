using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnemySpawner2_2 : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public GameConstants gameConstants;
    public UnityEvent onWaveComplete;
    public UnityEvent SpawnPowerup;
    public GameObject keyMapper;
    Dictionary<string, Vector3> keyMap;
    GameObject[] prefabsArray;
    List<Vector3> keyList;
    List<Vector3> removedKeyList;

    private GameObject character;
    private int[][] spawnSequence;
    private int enemyTotal;
    private int enemyCount;
    private int spawned;
    private int progress0 = 0;
    private int progress1 = 0;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        character = GameObject.Find("Character");
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        keyList = new List<Vector3>(keyMap.Values);
        removedKeyList = new List<Vector3> {};
        spawnSequence = enemyConstants.spawnSequence2_2;
        prefabsArray = new GameObject[] {enemyConstants.muffinWhitePrefab, enemyConstants.muffinBluePrefab, enemyConstants.muffinPinkPrefab, enemyConstants.chocolateCakePrefab, enemyConstants.donutPrefab};
        enemyTotal = spawnSequence[progress0][progress1];
        StartCoroutine(restoreKeyList());
    }

    IEnumerator restoreKeyList() {
        while (true) {
            for (int i = 0; i < removedKeyList.Count; i++) {
                int count = 0;
                Collider[] colliders = Physics.OverlapSphere(removedKeyList[i], 1.0f, Physics.AllLayers, QueryTriggerInteraction.Collide);
                foreach (Collider collider in colliders) {
                    if (collider.tag == "EnemyCollider" || collider.tag == "Character") {
                        count += 1;
                    }
                }
                if (count == 0) {
                    keyList.Add(removedKeyList[i]);
                    removedKeyList.RemoveAt(i);
                    // Debug.Log(string.Format("Remove: {0}, {1}", keyList.Count, removedKeyList.Count));
                }
            }
            yield return null;
        }
    }

    void spawnEnemies() {
        for (int count = 0; count < spawnSequence[progress0][progress1]; count++) {
            spawnEnemy();
        }
    }

    void spawnEnemy() {
        enemyCount += 1;
        spawned += 1;
        int indexPrefab = Random.Range(0, prefabsArray.Length);
        int index = Random.Range(0, keyList.Count);
        Instantiate(prefabsArray[indexPrefab], keyList[index], Quaternion.identity);
        removedKeyList.Add(keyList[index]);
        keyList.RemoveAt(index);
        // Debug.Log(string.Format("Add: {0}, {1}", keyList.Count, removedKeyList.Count));
    }

    IEnumerator spawnEnemiesWithDelay() {
        int spawnAt = Random.Range(0, spawnSequence[progress0][progress1] / 2);
        int spawnAt2 = Random.Range(spawnSequence[progress0][progress1] / 2, spawnSequence[progress0][progress1]);
        for (int count = 0; count < spawnSequence[progress0][progress1]; count++) {
            spawnEnemy();
            if (spawnAt == count || spawnAt2 == count) {
                SpawnPowerup.Invoke();
            }
            if (progress0 == 0) {
                if (enemyCount < spawned / 10 + 2) {
                    yield return new WaitForSeconds(0.5f);
                }
                else {
                    while (enemyCount >= spawned / 10 + 2) {
                        yield return null;
                    }
                    yield return new WaitForSeconds(0.5f);
                }
            }
            else if (progress0 == 1) {
                if (enemyCount < 8) {
                    yield return new WaitForSeconds(0.2f * (8 - spawned / 10));
                }
                else {
                    while (enemyCount >= 8) {
                        yield return null;
                    }
                    yield return new WaitForSeconds(0.2f * (8 - spawned / 10));
                }
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
            keyList.Remove(character.transform.position);
            removedKeyList.Add(character.transform.position);
            StartCoroutine(spawnEnemiesWithDelay());
        }
    }

    public void startNextSpawn() {
        enemyTotal = spawnSequence[progress0][progress1];
        StartCoroutine(WaitForNextSpawn());
    }

    IEnumerator waitForStartNextDialogue() {
        yield return new WaitForSeconds(1);
        onWaveComplete.Invoke();
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
                spawned = 0;
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
