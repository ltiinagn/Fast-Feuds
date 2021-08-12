using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileKnifeSpawner : MonoBehaviour
{
    private GameObject character;
    public EnemyConstants enemyConstants;
    public UnityEvent onBossDeath;
    public GameObject dialogueBox;
    public GameObject keyMapper;
    Dictionary<string, Vector3> keyMap;
    private int phase = 1;
    private bool lastHurrah = false;

    void spawnFromPooler(BulletType i, Vector3 position){
        // static method access
        GameObject item = BulletPooler.SharedInstance.GetPooledBullet(i);
        Vector3 knifeOffset = new Vector3(0,0,0);
        if (!lastHurrah) {
            knifeOffset = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)).normalized;
        }
        else if (lastHurrah) {
            knifeOffset = (position - character.transform.position).normalized;
        }
        float angle = Vector3.SignedAngle(knifeOffset, Vector3.forward, Vector3.up);
        if (item != null) {
            //set position, and other necessary states
            item.transform.position = knifeOffset * 3 + this.transform.position;
            ProjectileKnifeController projectileKnifeController = item.transform.Find("BoxCollider").GetComponent<ProjectileKnifeController>();
            projectileKnifeController.direction = -knifeOffset;
            projectileKnifeController.phase = phase;
            item.transform.rotation = Quaternion.AngleAxis(-angle, Vector3.up);
            item.SetActive(true);
        }
        else {
            Debug.Log("not enough items in the pool.");
        }
    }

    public void changePhase() {
        phase += 1;
    }

    public void SetInactive() {
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Character");
        keyMapper = GameObject.Find("KeyMapper");
        keyMap = keyMapper.GetComponent<KeyMapping>().keyMap;
        StartCoroutine(spawnBulletPeriodically());
    }

    public void StartLastHurrah() {
        lastHurrah = true;
    }

    IEnumerator spawnBulletPeriodically() {
        while ((!dialogueBox || dialogueBox.activeSelf)) {
            yield return null;
        }
        yield return new WaitForSeconds(3.0f);
        while (!lastHurrah) {
            for (int i = 0; i < 1; i++) {
                transform.position = character.transform.position;
                spawnFromPooler(BulletType.knife, transform.position);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(1.0f);
        }
        StartCoroutine(spawnLastHurrah());
    }

    IEnumerator spawnLastHurrah() {
        foreach (string[] name in enemyConstants.spawnKey2_B_L) {
            foreach (string c in name) {
                transform.position = keyMap[c];
                spawnFromPooler(BulletType.knife, keyMap[c]);
                yield return new WaitForSeconds(0.5f);
            }
        }
        onBossDeath.Invoke();
    }

    // Update is called once per frame
    void Update()
    {

    }
}