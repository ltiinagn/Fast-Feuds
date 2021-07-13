using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss2_BController : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public GameConstants gameConstants;
    public GameObject keyMapper;
    public GameObject bulletSpawns2_BPrefab;
    Dictionary<string, Vector3> keyMap;
    Dictionary<string, string> keyRowMap;

    private int health;

    // Start is called before the first frame update
    void Start()
    {
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        keyRowMap = keyMapper.GetComponent<KeyMapping>().keyRowMap;
        bulletSpawns2_BPrefab = enemyConstants.bulletSpawns2_BPrefab;
        health = enemyConstants.boss2_B_Health;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LastHurrah() {
        GameObject bulletSpawns2_B = Instantiate(bulletSpawns2_BPrefab, new Vector3(0,0,0), Quaternion.identity);
        yield return new WaitForSeconds(enemyConstants.lastHurrahDuration2_B);
        Destroy(bulletSpawns2_B);
    }

    void OnTriggerEnter(Collider col) {
        health -= 1;
        Debug.Log("damaged by character!");
        if (health == 1) {
            Debug.Log("entering last hurrah");
            GetComponent<Collider>().enabled = false;
            StartCoroutine(LastHurrah());
        }
    }
}
