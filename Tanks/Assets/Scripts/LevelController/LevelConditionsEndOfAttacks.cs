using UnityEngine;


public class LevelConditionsEndOfAttacks : MonoBehaviour, ILevelCondition
{
    [SerializeField] private LayerMask m_layermask;

    //private bool m_Reached; //���� �� ����������.

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
