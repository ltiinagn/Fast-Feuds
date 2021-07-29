using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConstants", menuName = "ScriptableObjects/CharacterConstants", order = 2)]
public class CharacterConstants : ScriptableObject
{
    // Character
    public int characterHealth = 10;
    public float characterSpeed = 50.0f;
}