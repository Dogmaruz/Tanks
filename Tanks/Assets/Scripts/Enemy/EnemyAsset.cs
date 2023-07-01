using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAsset", menuName = "Enemy/EnemyAsset")]
public sealed class EnemyAsset : ScriptableObject
{
    [Header("Игровые параметры")]

    public float moveSpeed = 3f;

    public int HitPoints = 100;

    public int ScoreValue = 5;

    public int Damage = 10;
}
