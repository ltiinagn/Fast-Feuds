using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    public string[] rowNames = {"Tiles/Row1", "Tiles/Row2", "Tiles/Row3", "Tiles/Row4"};

    public int powerupVisibilityDuration = 5;
    public int powerupDisappearDuration = 3;

    public GameObject powerupAddHealthPrefab;
    public GameObject powerupInvulnerablePrefab;
    public GameObject powerupDestroyAllEnemiesPrefab;
    public GameObject powerupDestroyAllProjectilesPrefab;

    public int invulnerablePowerupDuration = 5;

    public string[] dialogue1_1 = {"Hello! Welcome to the tutorial! Press space to continue or K to skip!", "Press the corresponding key to move to the in game tile.", "End of tutorial. Have fun!"};
    public string[] dialogue1_2 = {};
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