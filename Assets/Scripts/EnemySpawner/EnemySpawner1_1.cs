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
        spawnSequence = enemyConstants.spawnSequence1_1;
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
                Instantiate(enemyConstants.chickenStationaryPrefab, keyList[index], Quaternion.identity);
            }
            else if (progress1 <= 9) {
                Instantiate(enemyConstants.chickenMovingPrefab, keyList[index], Quaternion.identity);
            }
            else {
                Instantiate(enemyConstants.chickenThrowingPrefab, keyList[index], Quaternion.identity);
                enemyCount += 1;
            }
        }
        else if (progress0 == 1) {
            StartCoroutine(spawnClownMilkPair());
        }
        else if (progress0 == 2) {
            Instantiate(enemyConstants.bigMacPrefab, keyList[index], Quaternion.identity);
        }
        else if (progress0 == 3) {
            Instantiate(enemyConstants.friesPrefab, keyList[index], Quaternion.identity);
        }
        // else {
        //     Instantiate(enemyConstants.chickenMovingExplodingPrefab, keyList[index], Quaternion.identity);
        // }
        if (progress0 != 1) {
            keyList.RemoveAt(index);
        }
    }

    IEnumerator spawnClownMilkPair() {
        int index = Random.Range(0, keyList.Count);
        GameObject clownMilk1 = Instantiate(enemyConstants.clownMilkPrefab, keyList[index], Quaternion.identity);
        keyList.RemoveAt(index);

        index = Random.Range(0, keyList.Count);
        GameObject clownMilk2 = Instantiate(enemyConstants.clownMilkPrefab, keyList[index], Quaternion.identity);
        keyList.RemoveAt(index);

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
        yield return null;
    }

    IEnumerator spawnEnemiesWithDelay() {
        int spawnAt = Random.Range(0, spawnSequence[progress0][progress1]);
        for (int count = 0; count < spawnSequence[progress0][progress1]; count++) {
            spawnEnemy();
            if (spawnAt == count) {
                SpawnPowerup.Invoke();
            }
            if (progress0 == 0 && progress1 <= 9) {
                yield return new WaitForSeconds(0.2f);
            }
            else {
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
