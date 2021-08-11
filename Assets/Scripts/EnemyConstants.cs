using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConstants", menuName = "ScriptableObjects/EnemyConstants", order = 3)]
public class EnemyConstants : ScriptableObject
{
    // Enemy Prefabs
    public GameObject friesTutorialPrefab;
    public GameObject chickenStationaryPrefab;
    public GameObject chickenMovingPrefab;
    public GameObject chickenThrowingPrefab;
    public GameObject chickenMovingExplodingPrefab;
    public GameObject clownMilkPrefab;
    public GameObject bigMacPrefab;
    public GameObject friesPrefab;
    public GameObject muffinWhitePrefab;
    public GameObject muffinBluePrefab;
    public GameObject muffinPinkPrefab;
    public GameObject chocolateCakePrefab;
    public GameObject donutPrefab;
    public GameObject cupcakePrefab;
    public GameObject bossBigMikePrefab;
    public GameObject chickenMovesPrefab;
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
        new int[] {1, 2, 3, 3, 10},
        new int[] {1, 2, 2, 2, 10},
        new int[] {1, 2, 3, 4, 10}
    };
    public int redBallSpawnCount = 3;

    // Level 1-2 Spawn
    // public int[][] spawnSequence1_2 = new int[][] {
    //     new int[] {10},
    //     new int[] {10}
    // };
    public int[][] spawnSequence1_2 = new int[][] {
        new int[] {50},
        new int[] {50}
    };


    // Level 2-1 Spawn
    public int[][] spawnSequence2_1 = new int[][] {
        new int[] {1, 2, 3, 4, 10, 1, 2, 3, 4, 10, 1, 2, 3, 4, 10},
        //new int[] {1, 1, 1, 1, 10, 1, 1, 1, 1, 10, 1, 1, 1, 1, 10},
        new int[] {1, 2, 3, 4, 10},
        new int[] {1, 2, 3, 4, 10},
        new int[] {1, 2, 3, 4, 10}
    };

    // Level 2-2 Spawn
    public int[][] spawnSequence2_2 = new int[][] {
        new int[] {75},
        new int[] {100}
    };

    // Level 1-B
    public int bossBigMike_Health = 10;
    public string[][] spawnKey1_B = new string[][] {
        new string[] {"t", "h", "i", "s"},
        new string[] {"i", "s"},
        new string[] {"m", "y"},
        new string[] {"d", "e", "s", "t", "i", "n", "y"}
    };

    // Level 2-B
    public int boss2_B_Health = 31;
    public string[][] spawnKey2_B = new string[][] {
        new string[] {"n", "o", "t", "h", "i", "n", "g", "s"},
        new string[] {"g", "o", "n", "n", "a"},
        new string[] {"c", "h", "a", "n", "g", "e"},
        new string[] {"m", "y"},
        new string[] {"l", "o", "v", "e"},
        new string[] {"f", "o", "r"},
        new string[] {"y", "o", "u"},
    };
    // public int boss2_B_Health = 2;
    // public string[][] spawnKey2_B = new string[][] {
    //     new string[] {"n"},
    //     new string[] {"g"}
    // };

    // Level 3-B
    public string[][] keySequence3_B_1 = new string[][] {
        new string[] {"n", "o", "t", "h", "i", "n", "g"},
        new string[] {"c", "a", "n"},
        new string[] {"l", "i", "v", "e"},
        new string[] {"f", "o", "r", "e", "v", "e", "r"}
    };
    public string[][] keySequence3_B_1_2 = new string[][] {
        new string[] {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"},
        new string[] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0"},
        new string[] {"-", "=", "[", ";", ".", ","}
    };
    public string[][][] keySequence3_B_2 = new string[][][] {
        new string[][] {
            new string[] {"z", "x"}, new string[] {"x", "c"}, new string[] {"c", "v"}, new string[] {"v", "b"}, new string[] {"b", "n"}, new string[] {"n", "m"}, new string[] {"m", ","}, new string[] {",", "."},
        },
        new string[][] {
            new string[] {";", "l"}, new string[] {"l", "k"}, new string[] {"k", "j"}, new string[] {"j", "h"}, new string[] {"h", "g"}, new string[] {"g", "f"}, new string[] {"f", "d"}, new string[] {"d", "s"}, new string[] {"s", "a"}
        },
        new string[][] {
            new string[] {"q", "w"}, new string[] {"w", "e"}, new string[] {"e", "r"}, new string[] {"r", "t"}, new string[] {"t", "y"}, new string[] {"y", "u"}, new string[] {"u", "i"}, new string[] {"i", "o"}, new string[] {"o", "p"}, new string[] {"p", "["}
        },
        new string[][] {
            new string[] {"=", "-"}, new string[] {"-", "0"}, new string[] {"0", "9"}, new string[] {"9", "8"}, new string[] {"8", "7"}, new string[] {"7", "6"}, new string[] {"6", "5"}, new string[] {"5", "4"}, new string[] {"4", "3"}, new string[] {"3", "2"}, new string[] {"2", "1"}
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
}