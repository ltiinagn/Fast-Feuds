using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// TODO
public class EnemySpawner2_B : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public GameObject keyMapper;
    public GameObject projectileKnifeSpawner;
    public UnityEvent startNextDialogue;
    Dictionary<string, Vector3> keyMap;
    List<Vector3> keyList;

    private GameObject character;

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
        projectileKnifeSpawner.SetActive(true);
    }

    IEnumerator WaitForNextSpawn() {
        yield return new WaitForSeconds(1);
        if (character != null) {
            spawnBoss();
        }
    }

    IEnumerator waitForStartNextDialogue() {
        yield return new WaitForSeconds(5);
        startNextDialogue.Invoke();
    }

    public void enemyDead() {
        StartCoroutine(waitForStartNextDialogue());
    }
}
