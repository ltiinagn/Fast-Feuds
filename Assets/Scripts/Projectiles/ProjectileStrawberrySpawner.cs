using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStrawberrySpawner : MonoBehaviour
{
    Vector3 direction;
    private Animator cupcakeAnimator;

    void spawnFromPooler(BulletType i){
        // static method access
        GameObject item = BulletPooler.SharedInstance.GetPooledBullet(i);
        if (item != null) {
            //set position, and other necessary states
            item.transform.position = this.transform.position;
            item.transform.Find("BoxCollider").GetComponent<ProjectileStrawberryController>().direction = direction;
            item.SetActive(true);
        }
        else {
            Debug.Log("not enough items in the pool.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cupcakeAnimator = transform.parent.Find("Sprite").GetComponent<Animator>();
        direction = transform.parent.Find("Sprite/Body").GetComponent<SpriteRenderer>().flipX ? new Vector3(-1f, 0f, 0f) : new Vector3(1f, 0f, 0f);
        StartCoroutine(spawnBulletPeriodically());
    }

    IEnumerator spawnBulletPeriodically() {
        yield return new WaitForSeconds(0.5f);
        while (true) {
            cupcakeAnimator.SetTrigger("onThrow");
            yield return new WaitForSeconds(0.4f);
            spawnFromPooler(BulletType.strawberry);
            yield return new WaitForSeconds(5);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
