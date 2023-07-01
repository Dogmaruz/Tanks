using UnityEngine;

public class FP_CharacterController : Destructible
{
    [SerializeField] protected float m_forceSpeed; // Скорость движения.

    //[SerializeField] private Animator m_animator;

    [SerializeField] private int m_MaxAmmo;

    [SerializeField] protected Transform m_TowerTransform; // Transform для вращения персонажа

    [SerializeField] protected float m_tankRotationSpeed;

    [SerializeField] protected float m_towerRotationSpeed;

    private int m_SecondaryAmmo; //Вторичная турель.
    public int SecondaryAmmo => m_SecondaryAmmo;

    protected Rigidbody2D _rigibody;

    private Turret[] m_Turrets;

    protected TurretMode _mode;

    private void Awake()
    {
        _rigibody = GetComponent<Rigidbody2D>();

        m_Turrets = GetComponentsInChildren<Turret>();

        m_SecondaryAmmo = m_MaxAmmo;
    }

    public virtual void UpdateInputs(ref PlayerInputs playerInputs)
    {

        // Атака.
        if (playerInputs.MouseButtonPrimaryDown)
        {
            _mode = TurretMode.Primary;

            Fire();
        }

        if (playerInputs.MouseButtonSecondatyDown)
        {
            _mode = TurretMode.Secondary;

            Fire();
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

    public void AddAmmo(int ammo)
    {
        m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
    }

    //Использование снарядов.
    public bool DrawAmmo(int count)
    {
        if (count == 0)
            return true;

        if (m_SecondaryAmmo >= count)
        {
            m_SecondaryAmmo -= count;

            return true;
        }

        return false;
    }

    protected void Fire()
    {
        foreach (var turret in m_Turrets)
        {
            if (turret.Mode == _mode)
            {
                turret.Fire();
            }
        }
    }

    public virtual void FixedUpdateInputs(ref PlayerInputs playerInputs)
    {

        //Смена анимации.
        ChangeAnimation();

    }

    // Смена анимации.
    private void ChangeAnimation()
    {


    }

    public float GetVelosity()
    {
        return _rigibody.velocity.magnitude;
    }
}
