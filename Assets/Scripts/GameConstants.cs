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

    public int invulnerablePowerupDuration = 5;

    public string[] dialogueDummy = {};
    public string[][] dialogue0_T = new string[][] {
        new string[] {"Where am I?", "Oh! I can move to a tile by pressing its corresponding key!"},
        new string[] {"I should pick up that weapon!"},
        new string[] {"More fries?!"},
        new string[] {"This is bad.."}
    };

    public string[][] dialogue1_1 = new string[][] {
        new string[] {"Hello! Welcome to the tutorial! Press space to continue or K to skip!", "Press the corresponding key to move to the in-game tile.", "End of tutorial. Have fun!"},
        new string[] {"Good job! Next, clear some moving chickens!"},
        new string[] {"Good job! Next, clear some chickens that throw bones! Avoid getting hit by the bones!"},
        new string[] {"Beware of these chickens!"},
        new string[] {"Well done!"}
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