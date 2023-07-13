using UnityEngine;


public class LevelConditionsEndOfAttacks : MonoBehaviour, ILevelCondition
{
    public bool IsCompleted
    {
        get
        {
            // ��� ���������� ������ �� ������� - ���� ������ �����
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
