﻿using UnityEngine;


[CreateAssetMenu(fileName = "EnemyAsset", menuName = "Enemy/EnemyAsset")]
public sealed class EnemyAsset : ScriptableObject
{
    [Header("Внешний вид")]

    public Color color = Color.white;

    public Vector2 spriteScale = new Vector2(3, 3);

    public RuntimeAnimatorController controller;

    [Header("Игровые параметры")]

    public float moveSpeed = 1f;

    public int HitPoints = 1;

    public int Armor = 0;

    public Enemy.ArmorType ArmorType = Enemy.ArmorType.Base;

    public int ScoreValue = 1;

    public int KillValue = 1;

    public float ColliderRadius = 0.22f;

    public int Damage = 1;

    public int Gold = 1;
}