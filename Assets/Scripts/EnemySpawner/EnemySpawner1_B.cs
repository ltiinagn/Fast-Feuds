using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// TODO
public class EnemySpawner1_B : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public UnityEvent onWaveComplete;
    public GameObject keyMapper;
    Dictionary<string, Vector3> keyMap;
    List<Vector3> keyList;
    List<Vector3> removedKeyList;

    private GameObject character;
    private int enemyCount;
    private bool phaseChanged;
    private bool dead;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        character = GameObject.Find("Character");
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        removedKeyList = new List<Vector3> {};
        enemyCount = 0;
        phaseChanged = false;
        dead = false;
    }

    IEnumerator restoreKeyList() {
        for (int i = 0; i < removedKeyList.Count; i++) {
            int count = 0;
            Collider[] colliders = Physics.OverlapSphere(removedKeyList[i], 0.5f);
            foreach (Collider collider in colliders) {
                if (collider.tag == "EnemyCollider" || collider.tag == "Character") {
                    count += 1;
                }
            }
            if (count == 0) {
                keyList.Add(removedKeyList[i]);
                removedKeyList.RemoveAt(i);
            }
        }
        yield return null;
    }

    IEnumerator spawnEnemiesPeriodically() {
        while (!dead) {
            spawnEnemy();
            yield return restoreKeyList();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void spawnEnemy() {
        int index = Random.Range(0, keyList.Count);
        // Debug.Log(string.Format("Test: {0}, {1}", keyList.Count, index));
        if (!phaseChanged) {
            Instantiate(enemyConstants.chickenStationaryPrefab, keyList[index], Quaternion.identity);
        }
        else {
            Instantiate(enemyConstants.chickenMovingPrefab, keyList[index], Quaternion.identity);
        }
        enemyCount += 1;
        removedKeyList.Add(keyList[index]);
        keyList.RemoveAt(index);

        // Vector3 position = keyMap[enemyConstants.spawnKey1_B[progress1-1][count]];
        // GameObject chickMoves = Instantiate(enemyConstants.chickenStationaryPrefab, position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawnBoss() {
        Instantiate(enemyConstants.bossBigMikePrefab, new Vector3(8,0,2), Quaternion.identity);
    }

    public void startNextSpawn() {
        StartCoroutine(WaitForNextSpawn());
    }

    public void changePhase() {
        phaseChanged = true;
    }

    IEnumerator waitForStartNextDialogue() {
        yield return new WaitForSeconds(1);
        onWaveComplete.Invoke();
    }

    IEnumerator WaitForNextSpawn() {
        yield return new WaitForSeconds(1);
        if (character != null) {
            keyList = new List<Vector3>(keyMap.Values);
            keyList.Remove(character.transform.position);
            spawnBoss();
            yield return new WaitForSeconds(3.0f);
            StartCoroutine(spawnEnemiesPeriodically());
        }
    }

    IEnumerator FinalSequence() {
        foreach (string[] name in enemyConstants.spawnKey1_B) {
            foreach (string c in name) {
                Instantiate(enemyConstants.chickenStationaryPrefab, keyMap[c], Quaternion.identity);
                enemyCount += 1;
                while (enemyCount == 1) {
                    yield return null;
                }
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(waitForStartNextDialogue());
    }

    public void bossDead() {
        dead = true;
        StartCoroutine(waitForRemainingEnemies());
    }

    IEnumerator waitForRemainingEnemies() {
        while (enemyCount > 0) {
            yield return null;
        }
        StartCoroutine(FinalSequence());
    }

    public void enemyDead() {
        enemyCount -= 1;
    }
}
