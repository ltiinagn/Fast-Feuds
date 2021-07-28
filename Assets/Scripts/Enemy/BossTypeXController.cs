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
    private bool lastHits = false;

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
            lastHits = false;
            health -= 1;

            if (health == 0) {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }

    IEnumerator LastHurrah() {
        foreach (string[] name in enemyConstants.keySequence3_B) {
            for (int j = 0; j < name.Length; j++) {
                foreach (string rowName in gameConstants.rowNames) {
                    foreach (Transform child in GameObject.Find(rowName).transform)
                    {
                        child.Find("Warn").gameObject.SetActive(true);
                    }
                }
                GameObject.Find(keyRowMap[name[j]]+"/"+name[j]+"/Warn").SetActive(false);
                yield return new WaitForSeconds(1.0f);
                foreach (string rowName in gameConstants.rowNames) {
                    foreach (Transform child in GameObject.Find(rowName).transform)
                    {
                        if (child.gameObject.name != name[j]) {
                            child.Find("Warn").gameObject.SetActive(false);
                            child.Find("Danger").gameObject.SetActive(true);
                        }
                    }
                }
                yield return new WaitForSeconds(1.0f);
                foreach (string rowName in gameConstants.rowNames) {
                    foreach (Transform child in GameObject.Find(rowName).transform)
                    {
                        child.Find("Danger").gameObject.SetActive(false);
                    }
                }
                yield return new WaitForSeconds(1.0f);
            }
            lastHits = true;

            while (lastHits) {
                yield return null;
            }
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
