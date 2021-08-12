using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStrawberryChipWhiteSpawner : MonoBehaviour
{
    private Animator muffinWhiteAnimator;

    void spawnFromPooler(BulletType i)
    {
        // static method access
        GameObject item = BulletPooler.SharedInstance.GetPooledBullet(i);
        int angle = Random.Range(0, 360);
        if (item != null)
        {
            // set position, and other necessary states
            item.transform.position = this.transform.position;
            item.transform.Find("BoxCollider").GetComponent<ProjectileStrawberryChipWhiteController>().direction = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
            item.transform.rotation = Quaternion.AngleAxis(angle - 45, Vector3.up);
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
        muffinWhiteAnimator = transform.parent.Find("Sprite").GetComponent<Animator>();
        StartCoroutine(spawnBulletPeriodically());
    }

    IEnumerator spawnBulletPeriodically()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            muffinWhiteAnimator.SetTrigger("onThrow");
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < 5; i++)
            {
                spawnFromPooler(BulletType.strawberryChipWhite);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(2.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
