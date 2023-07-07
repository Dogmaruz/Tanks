using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private TurretMode m_Mode;
    public TurretMode Mode => m_Mode;

    [SerializeField] private TurretProperties m_TurretProperties; //Свойства турели.
    public TurretProperties TurretProperties => m_TurretProperties;

    [SerializeField] private Transform m_BulletSpawnPoint;

    private float m_RefireTimer;
    public bool CanFire => m_RefireTimer <= 0;

    private ShakeCamera _shakeCamera;

    private FP_CharacterController m_character;

    private void Start()
    {
        m_character = transform.root.GetComponent<FP_CharacterController>();
    }

    private void Update()
    {
        if (m_RefireTimer > 0)
        {
            m_RefireTimer -= Time.deltaTime;
        }
    }

    //Производит выстрел.
    public void Fire()
    {
        if (m_TurretProperties == null) return;

        if (m_RefireTimer > 0) return;

        if (m_TurretProperties.Mode == TurretMode.Secondary)
        {
            if (m_character.DrawAmmo(m_TurretProperties.AmoUsage) == false) return;
        }

        Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>();

        projectile.transform.position = m_BulletSpawnPoint.position;

        projectile.transform.up = transform.up;

        projectile.SetShakeCamera(_shakeCamera);

        if (m_character)
        {//Задает родителя сделавшего выстрел.
            projectile.SetParentShooter(m_character);
        }

        m_RefireTimer = m_TurretProperties.RateOfFire;

        Sound sound = m_TurretProperties.ProjectileSound;

        sound.Play();
    }


    //Задает свойства турели.
    public void AssignTurretProperties(TurretProperties props)
    {
        if (m_Mode != props.Mode) return;

        m_RefireTimer = 0;

        m_TurretProperties = props;
    }

    public void SetShakeCamera(ShakeCamera shakeCamera)
    {
        _shakeCamera = shakeCamera;
    }
}
