using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    public string[] levelNames = {"Level0-T", "Level1-1", "Level1-2", "Level1-3", "Level1-B"};
    public string[] rowNames = {"Tiles/Row1", "Tiles/Row2", "Tiles/Row3", "Tiles/Row4"};

    public int powerupVisibilityDuration = 5;
    public int powerupDisappearDuration = 3;

    public GameObject powerupWeaponPrefab;
    public GameObject powerupAddHealthPrefab;
    public GameObject powerupInvulnerablePrefab;
    public GameObject powerupDestroyAllEnemiesPrefab;
    public GameObject powerupDestroyAllProjectilesPrefab;

    // public Vector3[] offTileSpawnPoints = {}

    public int invulnerablePowerupDuration = 5;

    public string[] dialogueDummy = {};
    public string[][] dialogue0_T = new string[][] {
        new string[] {"Where am I?", "Oh! I can move to a tile by pressing its corresponding key!"},
        new string[] {"I should pick up that weapon!"},
        new string[] {"More fries?!"},
        new string[] {"This is bad.."}
    };

    public string[][] dialogue1_1 = new string[][] {
        new string[] {"Hello! Welcome to Fast Feuds! Have fun! Here are some chickens!"},
        new string[] {"ClownMilkk"},
        new string[] {"Burgers"},
        new string[] {"Fries"},
        new string[] {"Phew!"}
    };
    public string[][] dialogue1_2 = new string[][] {};
    public string[] dialogue1_3 = {};
    public string[] dialogue1_B = {};

    public string[] dialogue2_1 = {};
    public string[] dialogue2_2 = {};
    public string[] dialogue2_3 = {};
    public string[] dialogue2_B = {};

    public string[] dialogue3_1 = {};
    public string[] dialogue3_2 = {};
    public string[] dialogue3_3 = {};
    public string[] dialogue3_B = {};
}