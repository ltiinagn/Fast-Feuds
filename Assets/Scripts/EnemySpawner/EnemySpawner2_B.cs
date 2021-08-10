using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// TODO
public class EnemySpawner2_B : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public GameObject keyMapper;
    public UnityEvent startNextDialogue;
    Dictionary<string, Vector3> keyMap;
    List<Vector3> keyList;

    private GameObject character;
    private int[] spawnSequence;
    private int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        character = GameObject.Find("Character");
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        keyList = new List<Vector3>(keyMap.Values);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startNextSpawn() {
        StartCoroutine(WaitForNextSpawn());
    }

    void spawnBoss() {
        Instantiate(enemyConstants.boss2_BPrefab, new Vector3(8,0,2), Quaternion.identity);
    }

    IEnumerator WaitForNextSpawn() {
        yield return new WaitForSeconds(1);
        if (character != null) {
            spawnBoss();
        }
    }

    IEnumerator waitForStartNextDialogue() {
        yield return new WaitForSeconds(1);
        startNextDialogue.Invoke();
    }

    public void enemyDead() {
        enemyCount -= 1;
        if (enemyCount == 0) {
            StartCoroutine(waitForStartNextDialogue());
        }
    }
}
