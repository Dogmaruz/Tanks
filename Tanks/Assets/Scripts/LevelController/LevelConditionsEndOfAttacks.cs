using UnityEngine;


public class LevelConditionsEndOfAttacks : MonoBehaviour, ILevelCondition
{
    [SerializeField] private LayerMask m_layermask;

    //private bool m_Reached; //Есть ли завершение.

    public bool IsCompleted
    {
        get
        {
            // Код завершения уровня по условию - всех врагов убили
            foreach (var destructible in Destructible.AllDestructible)
            {
                var ai = destructible.GetComponent<AI_CharacterController>();

                if (ai != null)
                {
                    return false;
                }
            }
           
            return true;
        }
    }
}
