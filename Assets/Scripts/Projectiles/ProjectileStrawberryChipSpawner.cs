using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStrawberryChipSpawner : MonoBehaviour
{
    public GameObject character;
    private Animator muffinPinkAnimator;

    void spawnFromPooler(BulletType i)
    {
        // static method access
        GameObject item = BulletPooler.SharedInstance.GetPooledBullet(i);
        if (item != null)
        {
            // set position, and other necessary states
            item.transform.position = this.transform.position;
            float randomX = Random.Range(-0.2f, 0.2f);
            float randomZ = Random.Range(-0.2f, 0.2f);
            Vector3 direction = (new Vector3(character.transform.position.x, 0.0f, character.transform.position.z) - item.transform.position).normalized;
            direction.x += randomX;
            direction.z += randomZ;
            item.transform.Find("BoxCollider").GetComponent<ProjectileStrawberryChipController>().direction = direction;
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
        muffinPinkAnimator = transform.parent.Find("Sprite").GetComponent<Animator>();
        StartCoroutine(spawnBulletPeriodically());
    }

    IEnumerator spawnBulletPeriodically()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                muffinPinkAnimator.SetTrigger("onThrow");
                yield return new WaitForSeconds(0.2f);
                spawnFromPooler(BulletType.strawberryChip);
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
