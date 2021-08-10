using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBoneExplodeSpawner : MonoBehaviour
{
    Vector3 direction;
    private Animator chickenThrowingAnimator;

    void spawnFromPooler(BulletType i){
        // static method access
        GameObject item = BulletPooler.SharedInstance.GetPooledBullet(i);
        for (int j = 0; j < 2; i++) {
            if (item != null) {
                direction = j == 0 ? new Vector3(-1f, 0f, 0f) : new Vector3(1f, 0f, 0f);
                //set position, and other necessary states
                item.transform.position = this.transform.position;
                item.transform.Find("BoxCollider").GetComponent<ProjectileBoneController>().direction = direction;
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
        // chickenThrowingAnimator = transform.parent.Find("Sprite").GetComponent<Animator>();
        // direction = transform.parent.Find("Sprite/Body").GetComponent<SpriteRenderer>().flipX ? new Vector3(-1f, 0f, 0f) : new Vector3(1f, 0f, 0f);Z
    }

    public void spawnProjectiles() {
        spawnFromPooler(BulletType.bone);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
