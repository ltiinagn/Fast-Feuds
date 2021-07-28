using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBigMacSauceSpawner : MonoBehaviour
{
    void spawnFromPooler(BulletType i){
        // static method access
        for (int j = 0; j < 8; j++) {
            GameObject item = BulletPooler.SharedInstance.GetPooledBullet(i);
            int angle = j * 45;
            if (item != null) {
                //set position, and other necessary states
                item.transform.position = this.transform.position;
                item.transform.Find("BoxCollider").GetComponent<ProjectileBigMacSauceController>().direction = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
                item.transform.rotation = Quaternion.AngleAxis(angle - 45, Vector3.up);
                item.SetActive(true);
            }
            else {
                Debug.Log("not enough items in the pool.");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnBulletPeriodically());
    }

    IEnumerator spawnBulletPeriodically() {
        yield return new WaitForSeconds(0.5f);
        while (true) {
            spawnFromPooler(BulletType.bigMacSauce);
            yield return new WaitForSeconds(2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
