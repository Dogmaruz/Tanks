using UnityEngine;

[CreateAssetMenu(fileName = "TankAsset", menuName = "Tank/TankAsset")]
public sealed class TankAsset : ScriptableObject
{
    [Header("Tank Settings")]

    public float moveSpeed = 3f;

    public int HitPoints = 100;

    public int ScoreValue = 5;

    [Header("Visual settings")]
    public Sprite Hull;

    public Sprite Tower;

    public Sprite Gun;

    public Sprite GunConnector;
}
