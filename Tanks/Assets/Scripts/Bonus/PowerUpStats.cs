using UnityEngine;

public class PowerUpStats : PowerUp
{
    public enum EffectType
    {
        AddAmmo,

        Health
    }

    [SerializeField] private EffectType m_EffectType;

    [SerializeField] private float m_Value;


    //ƒобавл€ет снар€ды в зависимоти от их типа.
    protected override void OnPickedUp(FP_CharacterController character)
    {
        if (m_EffectType == EffectType.Health)
        {
            character.AddHitpoints((int)m_Value);
        }

        if (m_EffectType == EffectType.AddAmmo)
        {
            character.AddAmmo((int)m_Value);
        }
    }
}
