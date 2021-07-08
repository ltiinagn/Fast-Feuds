using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConstants", menuName = "ScriptableObjects/CharacterConstants", order = 2)]
public class CharacterConstants : ScriptableObject
{
    // Character
    public int characterHealth = 2;
    public float characterSpeed = 100.0f;
}