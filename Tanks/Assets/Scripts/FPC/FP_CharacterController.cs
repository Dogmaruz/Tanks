using UnityEngine;

public class FP_CharacterController : Destructible
{
    [SerializeField] protected float m_forceSpeed; // �������� ��������.

    //[SerializeField] private Animator m_animator;

    [SerializeField] protected Transform m_TowerTransform; // Transform ��� �������� ���������

    [SerializeField] protected float m_tankRotationSpeed;

    [SerializeField] protected float m_towerRotationSpeed;

    protected Rigidbody2D _rigibody;

    private void Awake()
    {
        _rigibody = GetComponent<Rigidbody2D>();
    }

    public virtual void UpdateInputs(ref PlayerInputs playerInputs)
    {

        // �����.
        if (playerInputs.MouseButtonDown)
        {
            Debug.Log("�����");
        }

        // ����������� �����
        Vector2 movement = transform.up * playerInputs.MoveAxisForward * m_forceSpeed;

        _rigibody.velocity = movement;

        // ������� �����
        float rotation = playerInputs.MoveAxisRight * m_tankRotationSpeed * Time.deltaTime;

        _rigibody.rotation -= rotation;

        //������� �����

        Vector3 mousePosition = playerInputs.MousePosition;

        Vector2 direction = mousePosition - m_TowerTransform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        Quaternion target = Quaternion.AngleAxis(angle, Vector3.forward);

        m_TowerTransform.rotation = Quaternion.Slerp(m_TowerTransform.rotation, target, m_towerRotationSpeed * Time.deltaTime);
    }

    public virtual void FixedUpdateInputs(ref PlayerInputs playerInputs)
    {

        //����� ��������.
        ChangeAnimation();

    }

    // ����� ��������.
    private void ChangeAnimation()
    {


    }
}
