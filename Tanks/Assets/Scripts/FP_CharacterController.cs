using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FP_CharacterController : Destructible
{
    [SerializeField] private float m_forceSpeed; // Скорость движения.

    [SerializeField] private Animator m_animator;

    [SerializeField] private Transform m_TowerTransform; // Transform для вращения персонажа

    [SerializeField] private float m_tankRotationSpeed;

    [SerializeField] private float m_towerRotationSpeed;

    private Rigidbody2D _rigibody;

    Quaternion targetRotation;

    private void Awake()
    {
        _rigibody = GetComponent<Rigidbody2D>();
    }

    public void UpdateInputs(ref PlayerInputs playerInputs)
    {

        // Атака.
        if (playerInputs.MouseButtonDown)
        {
            Debug.Log("Огонь");
        }

        // Перемещение танка
        Vector2 movement = transform.up * playerInputs.MoveAxisForward * m_forceSpeed;

        _rigibody.velocity = movement;

        // Поворот танка
        float rotation = playerInputs.MoveAxisRight * m_tankRotationSpeed * Time.deltaTime;

        _rigibody.rotation -= rotation;

        //Поворот башни

        Vector3 mousePosition = playerInputs.MousePosition;

        Vector2 direction = mousePosition - m_TowerTransform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        Quaternion target = Quaternion.AngleAxis(angle, Vector3.forward);

        m_TowerTransform.rotation = Quaternion.Slerp(m_TowerTransform.rotation, target, m_towerRotationSpeed * Time.deltaTime);
    }

    public void FixedUpdateInputs(ref PlayerInputs playerInputs)
    {

        //Смена анимации.
        ChangeAnimation();

    }

    // Смена анимации.
    private void ChangeAnimation()
    {


    }
}
