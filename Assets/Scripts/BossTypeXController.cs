using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossTypeXController : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public GameConstants gameConstants;
    public GameObject keyMapper;
    Dictionary<string, Vector3> keyMap;
    Dictionary<string, string> keyRowMap;

    private int health;
    private bool lastHits = true;

    // Start is called before the first frame update
    void Start()
    {
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        keyRowMap = keyMapper.GetComponent<KeyMapping>().keyRowMap;
        health = enemyConstants.bossTypeXHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastHits && Input.GetKeyDown("space")) {
            Debug.Log("HIT");
            lastHits = false;
            health -= 1;

            if (health == 0) {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }

    IEnumerator LastHurrah() {
        foreach (string rowName in gameConstants.rowNames) {
            foreach (Transform child in GameObject.Find(rowName).transform)
            {
                child.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        foreach (string[] name in gameConstants.keySequence) {
            GameObject.Find(keyRowMap[name[0]]+"/"+name[0]).GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(1);
            for (int j = 1; j < name.Length; j++) {
                GameObject.Find(keyRowMap[name[j-1]]+"/"+name[j-1]).GetComponent<SpriteRenderer>().enabled = false;
                GameObject.Find(keyRowMap[name[j]]+"/"+name[j]).GetComponent<SpriteRenderer>().enabled = true;
                yield return new WaitForSeconds(1);
            }
            lastHits = true;

            while (lastHits) {
                yield return null;
            }

            GameObject.Find(keyRowMap[name[name.Length-1]]+"/"+name[name.Length-1]).GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void OnTriggerEnter(Collider col) {
        health -= 1;
        Debug.Log("damaged by character!");
        if (health == 5) {
            Debug.Log("entering last hurrah");
            GetComponent<Collider>().enabled = false;
            StartCoroutine(LastHurrah());
        }
    }
}
