﻿using UnityEngine;

public enum TurretMode
{
    Primary,
    Secondary,
}

[CreateAssetMenu(fileName = "TurretProperties", menuName = "Turret/TurretProperties")]
public sealed class TurretProperties : ScriptableObject
{//Свойства турели.
    [SerializeField] private TurretMode m_Mode;
    public TurretMode Mode => m_Mode;

    [SerializeField] private Projectile m_ProjectilePrefab;
    public Projectile ProjectilePrefab => m_ProjectilePrefab;

    [SerializeField] private float m_RateOfFire;
    public float RateOfFire => m_RateOfFire;

    [SerializeField] private int m_AmoUsage;
    public int AmoUsage => m_AmoUsage;

    [SerializeField] private Sound m_ProjectileSound;
    public Sound ProjectileSound => m_ProjectileSound;
}
