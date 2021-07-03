using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    public string[] rowNames = {"Tiles/Row1", "Tiles/Row2", "Tiles/Row3", "Tiles/Row4"};
    public string[][] keySequence = new string[][] {
        new string[] {"l", "a", "l", "a"},
        new string[] {"n", "o", "a", "h"},
        new string[] {"s", "h", "a", "h", "u", "l"},
        new string[] {"l", "u", "n", "a"},
        new string[] {"w", "e", "i", "t", "i", "n", "g"}
    };
}