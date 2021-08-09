using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConstants", menuName = "ScriptableObjects/EnemyConstants", order = 3)]
public class EnemyConstants : ScriptableObject
{
    // Enemy Prefabs
    public GameObject chickenStationaryPrefab;
    public GameObject chickenMovingPrefab;
    public GameObject chickenThrowingPrefab;
    public GameObject chickenMovingExplodingPrefab;
    public GameObject clownMilkPrefab;
    public GameObject bigMacPrefab;
    public GameObject chocolateCakePrefab;
    public GameObject muffinPinkPrefab;
    public GameObject friesTutorialPrefab;
    public GameObject friesPrefab;
    public GameObject enemyTypeAPrefab;
    public GameObject boss1_BPrefab;
    public GameObject boss2_BPrefab;
    public GameObject bossTypeXPrefab;

    // Level 0-T Spawn
    public int[][] spawnSequence0_T = new int[][] {
        new int[] {5},
        new int[] {10}
    };

    // Level 1-1 Spawn
    public int[][] spawnSequence1_1 = new int[][] {
        new int[] {1, 2, 3, 4, 10, 1, 2, 3, 4, 10, 1, 2, 3, 4, 10},
        //new int[] {1, 1, 1, 1, 10, 1, 1, 1, 1, 10, 1, 1, 1, 1, 10},
        new int[] {1, 2, 3, 4, 10},
        new int[] {1, 2, 3, 4, 10},
        new int[] {1, 2, 3, 4, 10}
    };
    public int redBallSpawnCount = 5;

    // Level 1-2 Spawn
    // public int[][] spawnSequence1_2 = new int[][] {
    //     new int[] {10},
    //     new int[] {10}
    // };
    public int[][] spawnSequence1_2 = new int[][] {
        new int[] {50},
        new int[] {50}
    };


    // Level 1-3 Spawn
    public int[][] spawnSequence2_1 = new int[][] {
        new int[] {1, 2, 3, 4, 10, 1, 2, 3, 4, 10, 1, 2, 3, 4, 10},
        //new int[] {1, 1, 1, 1, 10, 1, 1, 1, 1, 10, 1, 1, 1, 1, 10},
        new int[] {1, 2, 3, 4, 10},
        new int[] {1, 2, 3, 4, 10},
        new int[] {1, 2, 3, 4, 10}
    };

    // Level 1-3 Spawn
    public int[] spawnSequence2_2 = {2, 3, 4};

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
    public string[][][] keySequence3_B_2 = new string[][][] {
        new string[][] {
            new string[] {"z", "x"}, new string[] {"x", "c"}, new string[] {"c", "v"}, new string[] {"v", "b"}, new string[] {"b", "n"}, new string[] {"n", "m"}, new string[] {"m", ","}, new string[] {",", "."},
        },
        new string[][] {
            new string[] {";", "l"}, new string[] {"l", "k"}, new string[] {"k", "j"}, new string[] {"j", "h"}, new string[] {"h", "g"}, new string[] {"g", "f"}, new string[] {"f", "d"}, new string[] {"d", "s"}, new string[] {"s", "a"}
        },
        new string[][] {
            new string[] {"q", "w"}, new string[] {"w", "e"}, new string[] {"e", "r"}, new string[] {"r", "t"}, new string[] {"t", "y"}, new string[] {"y", "i"}, new string[] {"i", "o"}, new string[] {"o", "p"}, new string[] {"p", "["}
        },
        new string[][] {
            new string[] {"1", "2"}, new string[] {"2", "3"}, new string[] {"3", "4"}, new string[] {"4", "5"}, new string[] {"5", "6"}, new string[] {"6", "7"}, new string[] {"7", "8"}, new string[] {"8", "9"}, new string[] {"9", "0"}, new string[] {"0", "-"}, new string[] {"-", "="}
        }
    };
    public string[][] keySequence3_B_L = new string[][] {
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