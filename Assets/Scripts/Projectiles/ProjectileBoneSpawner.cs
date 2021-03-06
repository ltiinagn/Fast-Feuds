using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBoneSpawner : MonoBehaviour
{
    Vector3 direction;
    private Animator chickenThrowingAnimator;

    void spawnFromPooler(BulletType i)
    {
        // static method access
        GameObject item = BulletPooler.SharedInstance.GetPooledBullet(i);
        if (item != null)
        {
            // set position, and other necessary states
            item.transform.position = this.transform.position;
            item.transform.Find("BoxCollider").GetComponent<ProjectileBoneController>().direction = direction;
            item.SetActive(true);
        }
        else
        {
            Debug.Log("not enough items in the pool.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        chickenThrowingAnimator = transform.parent.Find("Sprite").GetComponent<Animator>();
        if (transform.parent.gameObject.transform.rotation.eulerAngles.z > 179 && transform.parent.gameObject.transform.rotation.eulerAngles.z < 181)
        {
            direction = new Vector3(-1f, 0f, 0f);
        }
        else
        {
            direction = new Vector3(1f, 0f, 0f);
        }
        StartCoroutine(spawnBulletPeriodically());
    }

    IEnumerator spawnBulletPeriodically()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            chickenThrowingAnimator.SetTrigger("onThrow");
            yield return new WaitForSeconds(0.4f);
            spawnFromPooler(BulletType.bone);
            yield return new WaitForSeconds(2);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
