using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRedBallSpawner : MonoBehaviour
{
    public EnemyConstants enemyConstants;
    public ProjectileRedBallSpawner otherPair;
    public Vector3 direction;
    public Vector3 endPosition;
    public bool shoot;
    public int spawnCount;

    void spawnFromPooler(BulletType i){
        // static method access
        GameObject item = BulletPooler.SharedInstance.GetPooledBullet(i);
        if (item != null) {
            //set position, and other necessary states
            item.transform.position = this.transform.position;
            var redBallController = item.transform.Find("BoxCollider").GetComponent<ProjectileRedBallController>();
            redBallController.direction = direction;
            redBallController.endPosition = endPosition;
            redBallController.spawner = this;
            item.SetActive(true);
        }
        else {
            Debug.Log("not enough items in the pool.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // direction = gameObject.transform.parent.parent.Find("Sprite/Body").GetComponent<SpriteRenderer>().flipX ? new Vector3(-1f, 0f, 0f) : new Vector3(1f, 0f, 0f);
        spawnCount = enemyConstants.redBallSpawnCount;
        StartCoroutine(spawnBulletPeriodically());
    }

    IEnumerator spawnBulletPeriodically() {
        yield return new WaitForSeconds(0.5f);
        while (true) {
            if (shoot) {
                for (int i = 0; i < 5; i++) {
                    spawnFromPooler(BulletType.redBall);
                    yield return new WaitForSeconds(0.2f);
                }
                shoot = false;
                // yield return new WaitForSeconds(2.0f);
            }
            else {
                if (spawnCount == 0) {
                    spawnCount = 5;
                    otherPair.shoot = true;
                }
                yield return null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
