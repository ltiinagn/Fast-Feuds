using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class ProjectileRedBubbleSpawner : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public GameConstants gameConstants;
    public UnityEvent onBossDeath;
    public GameObject keyMapper;
    public GameObject dialogueBox;
    Dictionary<string, Vector3> keyMap;
    Dictionary<string, string> keyRowMap;
    List<Vector3> keyList;

    void spawnFromPooler(BulletType i, string n, float margin1, float margin2){
        // static method access
        GameObject item = BulletPooler.SharedInstance.GetPooledBullet(i);
        if (item != null) {
            //set position, and other necessary states
            item.transform.position = keyMap[n];
            item.SetActive(true);
            ProjectileRedBubbleController projectileRedBubbleController = item.transform.Find("BoxCollider").GetComponent<ProjectileRedBubbleController>();
            projectileRedBubbleController.margin1 = margin1;
            projectileRedBubbleController.margin2 = margin2;
        }
        else {
            Debug.Log("not enough items in the pool.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        keyRowMap = keyMapper.GetComponent<KeyMapping>().keyRowMap;
        keyList = new List<Vector3>(keyMap.Values);
        StartCoroutine(Phase1());
    }

    IEnumerator Phase1() {
        while ((!dialogueBox || dialogueBox.activeSelf)) {
            yield return null;
        }
        yield return new WaitForSeconds(3.0f);
        float margin1 = 0.7f;
        float margin2 = 1.0f;
        foreach (string[] name in enemyConstants.keySequence3_B_1) {
            foreach (string n in name) {
                spawnFromPooler(BulletType.redBubble, n, margin1, margin2);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(2.0f);
        }
        foreach (string[] name in enemyConstants.keySequence3_B_1_2) {
            foreach (string n in name) {
                spawnFromPooler(BulletType.redBubble, n, margin1, margin2);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(2.0f);
        }
        yield return StartCoroutine(PhaseIntermediate(30.0f));
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(Phase2());
    }

    IEnumerator PhaseIntermediate(float duration) {
        float margin1 = 0.7f;
        float margin2 = 1.0f;
        float interval = duration == 30.0f ? 0.1f : 0.08f;
        string[] keyMapKeys = keyMap.Keys.ToArray();
        int keyMapKeysLength = keyMapKeys.Length;
        for (int i = 0; i < (int) duration * 10; i ++) {
            string spawnLetter = keyMapKeys[Random.Range(0, keyMapKeysLength)];
            spawnFromPooler(BulletType.redBubble, spawnLetter, margin1, margin2);
            yield return new WaitForSeconds(interval);
        }
    }

    IEnumerator Phase2() {
        float margin1 = 0.5f;
        float margin2 = 0.2f;
        yield return new WaitForSeconds(0.5f);
        float speedChange = 0.0f;
        foreach (string[][] name in enemyConstants.keySequence3_B_2) {
            float interval = 1.0f - speedChange * 0.2f;
            for (int j = 0; j < name.Length; j++) {
                foreach (string c in keyMap.Keys) {
                    if (!name[j].Contains(c)) {
                        spawnFromPooler(BulletType.redBubble, c, margin1, margin2);
                    }
                }
                yield return new WaitForSeconds(interval);
            }
            yield return new WaitForSeconds(2.0f);
            speedChange += 1;
        }
        yield return StartCoroutine(PhaseIntermediate(60.0f));
        StartCoroutine(LastHurrah());
    }

    IEnumerator LastHurrah() {
        float margin1 = 1.0f;
        float margin2 = 0.4f;
        float speedChange = 0.0f;
        foreach (string[] name in enemyConstants.keySequence3_B_L) {
            float interval = 1.4f - speedChange * 0.1f;
            for (int j = 0; j < name.Length; j++) {
                foreach (string c in keyMap.Keys) {
                    if (!name[j].Contains(c)) {
                        spawnFromPooler(BulletType.redBubble, c, margin1, margin2);
                    }
                }
                yield return new WaitForSeconds(interval);
            }
            yield return new WaitForSeconds(2.0f);
            speedChange += 1;
        }
        onBossDeath.Invoke();
    }

    public void SetInactive() {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
