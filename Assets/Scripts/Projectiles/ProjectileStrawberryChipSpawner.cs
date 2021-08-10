using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStrawberryChipSpawner : MonoBehaviour
{
    public GameObject character;

    void spawnFromPooler(BulletType i){
        // static method access
        GameObject item = BulletPooler.SharedInstance.GetPooledBullet(i);
        if (item != null) {
            //set position, and other necessary states
            item.transform.position = this.transform.position;
            float randomX = Random.Range(-2.0f, 2.0f);
            float randomZ = Random.Range(-2.0f, 2.0f);
            Vector3 direction = (new Vector3(character.transform.position.x + randomX, 0.0f, character.transform.position.z + randomZ) - item.transform.position).normalized;
            item.transform.Find("BoxCollider").GetComponent<ProjectileStrawberryChipController>().direction = direction;
            direction = Quaternion.AngleAxis(-45, Vector3.up) * direction;
            item.transform.rotation = Quaternion.LookRotation(direction);
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
        yield return new WaitForSeconds(0.5f);
        while (true) {
            for (int i = 0; i < 3; i++) {
                spawnFromPooler(BulletType.strawberryChip);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(2.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
