using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConstants", menuName = "ScriptableObjects/EnemyConstants", order = 3)]
public class EnemyConstants : ScriptableObject
{
	// EnemyType0
    public int enemyType0Health = 1;

    // EnemyType1
    public int enemyType1Health = 10;

    // BossTypeX
    public int bossTypeXHealth = 15;
}