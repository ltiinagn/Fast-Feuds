using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

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
        StartCoroutine(Phase2());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Phase2() {
        float speedChange = 0.0f;
        foreach (string[][] name in enemyConstants.keySequence3_B_2) {
            float interval = 0.5f - speedChange * 0.02f;
            for (int j = 0; j < name.Length; j++) {
                foreach (string rowName in gameConstants.rowNames) {
                    foreach (Transform child in GameObject.Find(rowName).transform)
                    {
                        child.Find("Warn").gameObject.SetActive(true);
                    }
                }
                foreach (string n in name[j]) {
                    GameObject.Find(keyRowMap[n]+"/"+n+"/Warn").SetActive(false);
                }
                yield return new WaitForSeconds(interval);
                foreach (string rowName in gameConstants.rowNames) {
                    foreach (Transform child in GameObject.Find(rowName).transform)
                    {
                        if (Array.IndexOf(name[j], child.gameObject.name) == -1) {
                            child.Find("Warn").gameObject.SetActive(false);
                            child.Find("Danger").gameObject.SetActive(true);
                        }
                    }
                }
                yield return new WaitForSeconds(interval);
                foreach (string rowName in gameConstants.rowNames) {
                    foreach (Transform child in GameObject.Find(rowName).transform)
                    {
                        child.Find("Danger").gameObject.SetActive(false);
                    }
                }
                yield return new WaitForSeconds(interval);
            }
            yield return new WaitForSeconds(2.0f);
            speedChange += 1;
        }
        StartCoroutine(LastHurrah());
    }

    IEnumerator LastHurrah() {
        float speedChange = 0.0f;
        foreach (string[] name in enemyConstants.keySequence3_B_L) {
            float interval = 1.0f - speedChange * 0.02f;
            for (int j = 0; j < name.Length; j++) {
                foreach (string rowName in gameConstants.rowNames) {
                    foreach (Transform child in GameObject.Find(rowName).transform)
                    {
                        child.Find("Warn").gameObject.SetActive(true);
                    }
                }
                GameObject.Find(keyRowMap[name[j]]+"/"+name[j]+"/Warn").SetActive(false);
                yield return new WaitForSeconds(interval);
                foreach (string rowName in gameConstants.rowNames) {
                    foreach (Transform child in GameObject.Find(rowName).transform)
                    {
                        if (child.gameObject.name != name[j]) {
                            child.Find("Warn").gameObject.SetActive(false);
                            child.Find("Danger").gameObject.SetActive(true);
                        }
                    }
                }
                yield return new WaitForSeconds(interval);
                foreach (string rowName in gameConstants.rowNames) {
                    foreach (Transform child in GameObject.Find(rowName).transform)
                    {
                        child.Find("Danger").gameObject.SetActive(false);
                    }
                }
                yield return new WaitForSeconds(interval);
            }
            yield return new WaitForSeconds(2.0f);
            speedChange += 1;
        }
    }

    void OnTriggerEnter(Collider col) {
        // health -= 1;
        // Debug.Log("damaged by character!");
        // if (health == 5) {
        //     Debug.Log("entering last hurrah");
        //     GetComponent<Collider>().enabled = false;
        //     StartCoroutine(LastHurrah());
        // }
    }
}
