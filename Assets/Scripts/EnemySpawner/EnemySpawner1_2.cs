using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnemySpawner1_2 : MonoBehaviour
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
        removedKeyList = new List<Vector3> {};
        spawnSequence = enemyConstants.spawnSequence1_2;
        prefabsArray = new GameObject[] {enemyConstants.chickenStationaryPrefab, enemyConstants.chickenMovingPrefab, enemyConstants.chickenThrowingPrefab, enemyConstants.clownMilkPrefab, enemyConstants.bigMacPrefab, enemyConstants.friesPrefab};
        enemyTotal = spawnSequence[progress0][progress1];
        StartCoroutine(restoreKeyList());
    }

    IEnumerator restoreKeyList() {
        while (true) {
            for (int i = 0; i < removedKeyList.Count; i++) {
                int count = 0;
                Collider[] colliders = Physics.OverlapSphere(removedKeyList[i], 0.5f);
                foreach (Collider collider in colliders) {
                    if (collider.tag == "EnemyCollider" || collider.tag == "Character") {
                        count += 1;
                    }
                }
                if (count == 0) {
                    keyList.Add(keyList[i]);
                    removedKeyList.RemoveAt(i);
                    // Debug.Log(string.Format("Test: {0}, {1}", keyList.Count, removedKeyList.Count));
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
        if (indexPrefab != 3) {
            int index = Random.Range(0, keyList.Count);
            Instantiate(prefabsArray[indexPrefab], keyList[index], Quaternion.identity);
            removedKeyList.Add(keyList[index]);
            keyList.RemoveAt(index);
        }
        else if (indexPrefab == 3) {
            StartCoroutine(spawnClownMilkPair());
        }
    }

    IEnumerator spawnClownMilkPair() {
        int index = Random.Range(0, keyList.Count);
        GameObject clownMilk1 = Instantiate(enemyConstants.clownMilkPrefab, keyList[index], Quaternion.identity);
        keyList.RemoveAt(index);
        Debug.Log("spawn1");

        index = Random.Range(0, keyList.Count);
        GameObject clownMilk2 = Instantiate(enemyConstants.clownMilkPrefab, keyList[index], Quaternion.identity);
        keyList.RemoveAt(index);
        Debug.Log("spawn2");

        ClownMilkController clownMilk1Controller = clownMilk1.transform.Find("BoxCollider").GetComponent<ClownMilkController>();
        clownMilk1Controller.otherPair = clownMilk2;

        ClownMilkController clownMilk2Controller = clownMilk2.transform.Find("BoxCollider").GetComponent<ClownMilkController>();
        clownMilk2Controller.otherPair = clownMilk1;

        ProjectileRedBallSpawner clownMilk1RedBallSpawner = clownMilk1.transform.Find("ProjectileRedBallSpawner").GetComponent<ProjectileRedBallSpawner>();
        clownMilk1RedBallSpawner.direction = (clownMilk2.transform.position - clownMilk1.transform.position).normalized;
        clownMilk1RedBallSpawner.endPosition = clownMilk2.transform.position;
        clownMilk1RedBallSpawner.shoot = true;

        var clownMilk2RedBallSpawner = clownMilk2.transform.Find("ProjectileRedBallSpawner").GetComponent<ProjectileRedBallSpawner>();
        clownMilk2RedBallSpawner.direction = (clownMilk1.transform.position - clownMilk2.transform.position).normalized;
        clownMilk2RedBallSpawner.endPosition = clownMilk1.transform.position;
        clownMilk2RedBallSpawner.shoot = false;

        clownMilk1RedBallSpawner.otherPair = clownMilk2RedBallSpawner;
        clownMilk2RedBallSpawner.otherPair = clownMilk1RedBallSpawner;
        Debug.Log("spawndone");
        yield return null;
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
        // Debug.Log(enemyCount);
    }

    IEnumerator WaitForNextSpawn() {
        yield return new WaitForSeconds(1);
        if (character != null) {
            keyList = new List<Vector3>(keyMap.Values);
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
        Debug.Log("enemyDead");
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
