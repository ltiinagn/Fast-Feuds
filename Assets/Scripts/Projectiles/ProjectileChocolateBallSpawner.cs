using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileChocolateBallSpawner : MonoBehaviour
{
    bool flipX;

    void spawnFromPooler(BulletType i){
        int center = flipX ? 270 : 90;
        for (int angle = center-10; angle <= center+10; angle += 10) {
            // static method access
            GameObject item = BulletPooler.SharedInstance.GetPooledBullet(i);
            if (item != null) {
                //set position, and other necessary states
                item.transform.position = this.transform.position;
                item.transform.Find("BoxCollider").GetComponent<ProjectileChocolateBallController>().direction = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
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
        // flipX = transform.parent.Find("Sprite/Body").GetComponent<SpriteRenderer>().flipX;
        if (transform.parent.gameObject.transform.rotation.eulerAngles.z > 179 && transform.parent.gameObject.transform.rotation.eulerAngles.z < 181)
        {
            flipX = true;
        }
        else
        {
            flipX = false;
        }
        StartCoroutine(spawnBulletPeriodically());
    }

    IEnumerator spawnBulletPeriodically() {
        yield return new WaitForSeconds(0.5f);
        while (true) {
            for (int i = 0; i < 5; i++) {
                spawnFromPooler(BulletType.chocolateBall);
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
