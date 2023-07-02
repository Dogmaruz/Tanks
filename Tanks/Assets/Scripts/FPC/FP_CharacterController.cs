using UnityEngine;

public class FP_CharacterController : Destructible
{
    [SerializeField] protected float m_moveSpeed; // Скорость движения.

    //[SerializeField] private Animator m_animator;

    [SerializeField] private int m_MaxAmmo;

    [SerializeField] protected Transform m_TowerTransform; // Transform для вращения персонажа

    [SerializeField] protected float m_tankRotationSpeed;

    [SerializeField] protected float m_towerRotationSpeed;

    [Header("Visual Settings")]
    [SerializeField] protected TankAsset m_tankAsset;

    [SerializeField] protected SpriteRenderer m_hull;

    [SerializeField] protected SpriteRenderer m_tower;

    [SerializeField] protected SpriteRenderer m_gun;

    [SerializeField] protected SpriteRenderer m_gunConnector;

    [SerializeField] protected SpriteRenderer m_aim; 

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

        Initialize();
    }

    private void Initialize()
    {
        m_hull.sprite = m_tankAsset.Hull;

        m_tower.sprite = m_tankAsset.Tower;

        m_gun.sprite = m_tankAsset.Gun;

        m_gunConnector.sprite = m_tankAsset.GunConnector;

        m_moveSpeed = m_tankAsset.moveSpeed;

        SetHitpoints(m_tankAsset.HitPoints);

        SetScoreValue(m_tankAsset.ScoreValue);
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
        Vector2 movement = transform.up * playerInputs.MoveAxisForward * m_moveSpeed;

        _rigibody.velocity = movement;

        // Поворот танка
        float rotation = playerInputs.MoveAxisRight * m_tankRotationSpeed * Time.deltaTime;

        _rigibody.rotation -= rotation;

        //Поворот башни
        Vector3 mousePosition = playerInputs.MousePosition;

        m_aim.transform.position = new Vector2(mousePosition.x, mousePosition.y);

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
