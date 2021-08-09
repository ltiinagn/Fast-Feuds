using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// TODO
public class EnemySpawner1_B : MonoBehaviour
{
    public EnemyConstants enemyConstants;
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
        spawnSequence = enemyConstants.spawnSequence1_B;
        enemyCount = spawnSequence[progress0][progress1];

        StartCoroutine(WaitForNextSpawn());
    }

    void spawnEnemies() {
        // Instantiate(enemyConstants.chickenStationaryPrefab, new Vector3(2,0,0), Quaternion.identity);
        for (int count = 0; count < spawnSequence[progress0][progress1]; count++) {
            spawnEnemy(count);
        }
    }

    void spawnEnemy(int count) {
        Vector3 position = keyMap[enemyConstants.spawnKey1_B[progress1-1][count]];
        GameObject chickMoves = Instantiate(enemyConstants.chickenMovesPrefab, position, Quaternion.identity);
        chickMoves.transform.Find("BoxCollider").GetComponent<ChickenMovesController>().Initialize(enemyConstants.spawnMovesAllowed1_B[progress1-1][count]);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawnBoss() {
        Instantiate(enemyConstants.bossBigMikePrefab, new Vector3(8,0,2), Quaternion.identity);
    }

    IEnumerator waitForStartNextDialogue() {
        yield return new WaitForSeconds(1);
        startNextDialogue.Invoke();
    }

    IEnumerator WaitForNextSpawn() {
        yield return new WaitForSeconds(1);
        if (character != null) {
            keyList = new List<Vector3>(keyMap.Values);
            keyList.Remove(character.transform.position);
            if (progress1 == 0) {
                spawnBoss();
            }
            else {
                spawnEnemies();
            }
        }
    }

    public void enemyDead() {
        enemyCount -= 1;
        if (enemyCount == 0) {
            if (progress0 == 0 && progress1 < spawnSequence[progress0].Length-1) {
                progress1 += 1;
                enemyCount = spawnSequence[progress0][progress1];
                StartCoroutine(WaitForNextSpawn());
            }
            else {
                StartCoroutine(waitForStartNextDialogue());
            }
        }
    }
}
