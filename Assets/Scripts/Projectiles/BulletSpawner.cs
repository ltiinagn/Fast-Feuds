using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public Vector3 direction;

    void spawnFromPooler(BulletType i){
        // static method access
        GameObject item = BulletPooler.SharedInstance.GetPooledBullet(i);
        if (item != null) {
            //set position, and other necessary states
            item.transform.position = this.transform.position;
            item.transform.Find("BoxCollider").GetComponent<ProjectileBoneController>().direction = direction;
            item.SetActive(true);
        }
        else {
            Debug.Log("not enough items in the pool.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnBulletPeriodically());
    }

    IEnumerator spawnBulletPeriodically() {
        while (true) {
            yield return new WaitForSeconds(2);
            spawnFromPooler(BulletType.bullet1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
