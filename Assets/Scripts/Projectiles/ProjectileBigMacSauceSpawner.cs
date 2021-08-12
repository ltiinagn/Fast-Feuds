using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBigMacSauceSpawner : MonoBehaviour
{
    void spawnFromPooler(BulletType i, bool diagonal) {
        int angleOffset = diagonal ? 45 : 0;
        for (int j = 0; j < 4; j++) {
            // static method access
            GameObject item = BulletPooler.SharedInstance.GetPooledBullet(i);
            int angle = j * 90 + angleOffset;
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
        bool diagonal = Random.Range(0,2) == 1 ? true : false;
        while (true) {
            spawnFromPooler(BulletType.bigMacSauce, diagonal);
            diagonal = !diagonal;
            yield return new WaitForSeconds(2);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
