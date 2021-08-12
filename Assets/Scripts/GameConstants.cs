using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    public string[] levelNames = {"Level0-T", "Level1-1", "Level1-2", "Level1-3", "Level1-B", "Level2-1", "Level2-2", "Level2-B", "Level3-B"};
    public string[] rowNames = {"Tiles/Row1", "Tiles/Row2", "Tiles/Row3", "Tiles/Row4"};

    public int powerupVisibilityDuration = 5;
    public int powerupVisibilityDuration2 = 3; // for invulnerable
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

    public string[][] dialogue1_2 = new string[][] {
        new string[] {"They're coming everywhere!"},
        new string[] {"Even more of them?!"},
        new string[] {"I think that's it for now.."}
    };
    public string[][] dialogue1_B = new string[][] {
        new string[] {"Boss"},
        new string[] {"Rest point."},
    };

    public string[][] dialogue2_1 = new string[][] {
        new string[] {"Dummy 2.1.1"},
        new string[] {"Dummy 2.1.2"},
        new string[] {"Dummy 2.1.3"},
        new string[] {"Dummy 2.1.4"},
        new string[] {"Dummy 2.1.5"}
    };
    public string[][] dialogue2_2 = new string[][] {
        new string[] {"They're coming everywhere! 2"},
        new string[] {"Even more of them?! 2"},
        new string[] {"I think that's it for now.. 2"}
    };
    public string[][] dialogue2_B = new string[][] {
        new string[] {"Boss2"},
        new string[] {"Rest point.2"},
    };

    public string[][] dialogue3_B = new string[][] {
        new string[] {"Final Boss"},
        new string[] {"I'm freed at last.."},
    };
}