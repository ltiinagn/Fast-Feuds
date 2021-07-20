using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConstants", menuName = "ScriptableObjects/EnemyConstants", order = 3)]
public class EnemyConstants : ScriptableObject
{
    // Enemy Prefabs
    public GameObject enemyType0Prefab;
    public GameObject enemyType1Prefab;
    public GameObject enemyTypeAPrefab;
    public GameObject boss1_BPrefab;
    public GameObject boss2_BPrefab;
    public GameObject bossTypeXPrefab;

    // Level 1-1 Spawn
    public int[] spawnSequence1_1 = {1, 2, 3, 4, 10, 1, 2, 3, 4, 10};

    // Level 1-B
    public int boss1_B_Health = 10;
    public int[] spawnSequence1_B = {1, 5, 4, 4, 5, 5}; // include boss
    public int[][] spawnMovesAllowed1_B = new int[][] {
        new int[] {1, 2, 3, 4, 5}
    };
    public string[][] spawnKey1_B = new string[][] {
        new string[] {"w", "a", "t", "e", "r"}
    };

    // Level 2-B
    public GameObject bulletSpawns2_BPrefab;
    public int boss2_B_Health = 15;
    public int lastHurrahDuration2_B = 15;

    // Level 3-B
    public int bossTypeXHealth = 15;
    public string[][] keySequence3_B = new string[][] {
        new string[] {"l", "a", "l", "a"},
        new string[] {"n", "o", "a", "h"},
        new string[] {"s", "h", "a", "h", "u", "l"},
        new string[] {"l", "u", "n", "a"},
        new string[] {"w", "e", "i", "t", "i", "n", "g"}
    };

	// EnemyType0
    public int enemyType0Health = 1;

    // EnemyType1
    public int enemyType1Health = 1;

    // EnemyTypeA
    public int enemyTypeAHealth = 1;
}