using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStrawberryChipBlueSpawner : MonoBehaviour
{
    public GameObject character;
    private Animator muffinBlueAnimator;

    void spawnFromPooler(BulletType i)
    {
        // static method access
        GameObject item = BulletPooler.SharedInstance.GetPooledBullet(i);
        if (item != null)
        {
            // set position, and other necessary states
            item.transform.position = this.transform.position;
            float randomX = Random.Range(-0.4f, 0.4f);
            float randomZ = Random.Range(-0.4f, 0.4f);
            Vector3 direction = (new Vector3(character.transform.position.x, 0.0f, character.transform.position.z) - item.transform.position).normalized;
            direction.x += randomX;
            direction.z += randomZ;
            item.transform.Find("BoxCollider").GetComponent<ProjectileStrawberryChipBlueController>().direction = direction;
            direction = Quaternion.AngleAxis(-45, Vector3.up) * direction;
            item.transform.rotation = Quaternion.LookRotation(direction);
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
        character = GameObject.Find("Character");
        muffinBlueAnimator = transform.parent.Find("Sprite").GetComponent<Animator>();
        StartCoroutine(spawnBulletPeriodically());
    }

    IEnumerator spawnBulletPeriodically()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            muffinBlueAnimator.SetTrigger("onThrow");
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < 3; i++)
            {
                spawnFromPooler(BulletType.strawberryChipBlue);
                yield return new WaitForSeconds(0.3f);
            }
            yield return new WaitForSeconds(2.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
