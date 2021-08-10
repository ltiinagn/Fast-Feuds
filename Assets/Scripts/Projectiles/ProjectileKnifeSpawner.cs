using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileKnifeSpawner : MonoBehaviour
{
    private GameObject character;
    public GameObject dialogueBox;

    void spawnFromPooler(BulletType i){
        // static method access
        GameObject item = BulletPooler.SharedInstance.GetPooledBullet(i);
        Vector3 knifeOffset = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)).normalized * 3;
        float angle = -Vector3.SignedAngle(knifeOffset, Vector3.forward, Vector3.up);
        if (item != null) {
            //set position, and other necessary states
            item.transform.position = knifeOffset + this.transform.position;
            // item.transform.Find("BoxCollider").GetComponent<ProjectileKnifeController>().direction = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
            item.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            Debug.Log(string.Format("Test: {0}, {1}", angle, item.transform.rotation));
            item.SetActive(true);
        }
        else {
            Debug.Log("not enough items in the pool.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Character");
        StartCoroutine(spawnBulletPeriodically());
    }

    IEnumerator spawnBulletPeriodically() {
        while ((!dialogueBox || dialogueBox.activeSelf)) {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        while (true) {
            for (int i = 0; i < 5; i++) {
                spawnFromPooler(BulletType.knife);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(1.0f);
            transform.position = character.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}