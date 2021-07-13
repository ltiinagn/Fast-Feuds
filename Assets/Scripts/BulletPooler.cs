using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType {
	bullet1 =  0
}

[System.Serializable]
public class BulletPoolItem
{
	public int amount;
	public GameObject prefab;
	public bool expandPool;
	public BulletType type;
}

public class ExistingPoolItem
{
	public GameObject gameObject;
	public BulletType type;

	// constructor
	public ExistingPoolItem(GameObject gameObject, BulletType type) {
		// reference input
		this.gameObject = gameObject;
		this.type = type;
	}
}

public class BulletPooler : MonoBehaviour
{
    public static BulletPooler SharedInstance;

    public List<BulletPoolItem> bulletsToPool; // types of different object to pool
    public List<ExistingPoolItem> pooledBullets; // a list of all objects in the pool, of all types

    public GameObject GetPooledBullet(BulletType type)
    {
        // return inactive pooled object if it matches the type
        for (int i = 0; i < pooledBullets.Count; i++)
        {
            if (!pooledBullets[i].gameObject.activeInHierarchy && pooledBullets[i].type == type)
            {
                return pooledBullets[i].gameObject;
            }
        }
        
        // this will be called when no more active object is present, item to expand pool if required
        foreach (BulletPoolItem item in bulletsToPool)
        {
            if (item.type == type)
            {
                if (item.expandPool)
                {
                    GameObject bullet = (GameObject) Instantiate(item.prefab);
                    bullet.SetActive(false);
                    bullet.transform.parent = this.transform;
                    pooledBullets.Add(new ExistingPoolItem(bullet, item.type));
                    return bullet;
                }
            }
        }
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        SharedInstance = this;

        pooledBullets = new List<ExistingPoolItem>();
        foreach (BulletPoolItem item in bulletsToPool)
        {
            for (int i = 0; i < item.amount; i++)
            {
                // this 'pickup' a local variable, but Unity will not remove it since it exists in the scene
                GameObject bullet = (GameObject) Instantiate(item.prefab);
                bullet.SetActive(false);
                bullet.transform.parent = this.transform;
                ExistingPoolItem e = new ExistingPoolItem(bullet, item.type);
                pooledBullets.Add(e);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
