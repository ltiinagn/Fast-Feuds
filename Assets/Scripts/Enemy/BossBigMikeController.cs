using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossBigMikeController : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public GameConstants gameConstants;
    public GameObject keyMapper;
    public UnityEvent onEnemyDeath;
    public UnityEvent SpawnPowerup;
    Dictionary<string, Vector3> keyMap;
    Dictionary<string, string> keyRowMap;

    private int health;

    // Start is called before the first frame update
    void Start()
    {
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        keyRowMap = keyMapper.GetComponent<KeyMapping>().keyRowMap;
        health = enemyConstants.bossBigMike_Health;
        SpawnPowerup.Invoke();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col) {
        health -= 1;
        Debug.Log("damaged by character!");
        if (health == 1) { // not a typo, don't want boss to actually die, but making use of onEnemyDeath
            Debug.Log("entering last hurrah");
            onEnemyDeath.Invoke();
            GetComponent<Collider>().enabled = false;
        }
    }
}
