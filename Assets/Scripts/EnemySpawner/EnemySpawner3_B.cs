using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO
public class EnemySpawner3_B : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public GameObject keyMapper;
    Dictionary<string, Vector3> keyMap;
    List<Vector3> keyList;

    private GameObject character;
    private int[] spawnSequence;
    private int enemyCount;
    private int progress = 0;
    private bool movable = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        character = GameObject.Find("Character");
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        spawnSequence = enemyConstants.spawnSequence3_B;
        enemyCount = spawnSequence[progress];

        StartCoroutine(WaitForNextSpawn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawnBoss() {
        Instantiate(enemyConstants.bossTypeXPrefab, new Vector3(8,0,2), Quaternion.identity);
    }

    IEnumerator WaitForNextSpawn() {
        yield return new WaitForSeconds(1);
        if (character != null) {
            keyList = new List<Vector3>(keyMap.Values);
            keyList.Remove(character.transform.position);
            spawnBoss();
        }
    }

    public void enemyDead() {
        enemyCount -= 1;
        if (enemyCount == 0) {
            if (progress < spawnSequence.Length-1) {
                progress += 1;
                if (!movable && progress > 4) {
                    movable = true;
                }
                enemyCount = spawnSequence[progress];
                StartCoroutine(WaitForNextSpawn());
            }
        }
    }
}
