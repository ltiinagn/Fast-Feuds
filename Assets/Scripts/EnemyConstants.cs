using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConstants", menuName = "ScriptableObjects/EnemyConstants", order = 3)]
public class EnemyConstants : ScriptableObject
{
    // Enemy Prefabs
    public GameObject chickenStationaryPrefab;
    public GameObject chickenMovingPrefab;
    public GameObject chickenThrowingPrefab;
    public GameObject clownMilkPrefab;
    public GameObject bigMacPrefab;
    public GameObject shooterPrefab;
    public GameObject shooter2Prefab;
    public GameObject friesPrefab;
    public GameObject enemyTypeAPrefab;
    public GameObject boss1_BPrefab;
    public GameObject boss2_BPrefab;
    public GameObject bossTypeXPrefab;

    // Level 1-1 Spawn
    public int[][] spawnSequence1_1 = new int[][] {
        new int[] {1},
        new int[] {1},
        new int[] {1}
    };
    // public int[][] spawnSequence1_1 = new int[][] {
    //     new int[] {1, 2, 3, 4, 10},
    //     new int[] {1, 2, 3, 4, 10},
    //     new int[] {1, 2, 3, 4}
    // };

    // Level 1-2 Spawn
    public int redBallSpawnCount = 5;
    public int[] spawnSequence1_2 = {2, 3, 4};

    // Level 1-3 Spawn
    public int[] spawnSequence1_3 = {2, 2, 2};

    // Level 1-3 Spawn
    public int[] spawnSequence1_4 = {2, 3, 4};

    // Level 1-B
    public int boss1_B_Health = 10;
    public int[] spawnSequence1_B = {1, 5, 4, 4, 5, 5}; // include boss
    public int[][] spawnMovesAllowed1_B = new int[][] {
        new int[] {1, 2, 3, 4, 5},
        new int[] {1, 2, 3, 4}
    };
    public string[][] spawnKey1_B = new string[][] {
        new string[] {"w", "a", "t", "e", "r"},
        new string[] {"w", "i", "n", "e"}
    };

    // Level 2-B
    public GameObject bulletSpawns2_BPrefab;
    public int boss2_B_Health = 15;
    public int lastHurrahDuration2_B = 15;

    // Level 3-B
    public int bossTypeXHealth = 15;
    public int[] spawnSequence3_B = new int[] {1};
    public string[][] keySequence3_B = new string[][] {
        new string[] {"l", "a", "l", "a"},
        new string[] {"n", "o", "a", "h"},
        new string[] {"s", "h", "a", "h", "u", "l"},
        new string[] {"l", "u", "n", "a"},
        new string[] {"w", "e", "i", "t", "i", "n", "g"}
    };

    // EnemyHealth
    public int enemyHealth = 1;

	// ChickenStationary
    public int chickenStationaryHealth = 1;

    // ChickenMoving
    public int chickenMovingHealth = 1;

    // EnemyTypeA
    public int enemyTypeAHealth = 1;
}