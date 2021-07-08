using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConstants", menuName = "ScriptableObjects/EnemyConstants", order = 3)]
public class EnemyConstants : ScriptableObject
{
    // Enemy Prefabs
    public GameObject enemyType0Prefab;
    public GameObject enemyType1Prefab;
    public GameObject bossTypeXPrefab;

    // Level 1-1 Spawn
    public int[] spawnSequence1_1 = {1, 2, 3, 4, 10, 1, 2, 3, 4, 10};

	// EnemyType0
    public int enemyType0Health = 1;

    // EnemyType1
    public int enemyType1Health = 1;

    // BossTypeX
    public int bossTypeXHealth = 15;
}