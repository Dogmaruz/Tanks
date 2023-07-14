using UnityEngine;
using Zenject;

public class Turret : MonoBehaviour
{
    [SerializeField] private TurretMode m_mode;
    public TurretMode Mode => m_mode;

    [SerializeField] private TurretProperties m_turretProperties; //Свойства турели.
    public TurretProperties TurretProperties => m_turretProperties;

    [SerializeField] private Transform m_bulletSpawnPoint;

    private float m_refireTimer;
    public bool CanFire => m_refireTimer <= 0;

    private FP_CharacterController m_character;

    private DiContainer _diContainer;

    [Inject]
    public void Construct(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    private void Start()
    {
        m_character = transform.root.GetComponent<FP_CharacterController>();
    }

    private void Update()
    {
        if (m_refireTimer > 0)
        {
            m_refireTimer -= Time.deltaTime;
        }
    }

    //Производит выстрел.
    public void Fire()
    {
        if (m_turretProperties == null) return;

        if (m_refireTimer > 0) return;

        if (m_turretProperties.Mode == TurretMode.Secondary)
        {
            if (m_character.DrawAmmo(m_turretProperties.AmoUsage) == false) return;
        }

        var projectile = _diContainer.InstantiatePrefab(m_turretProperties.ProjectilePrefab).GetComponent<Projectile>();

        projectile.transform.position = m_bulletSpawnPoint.position;

        projectile.transform.up = transform.up;

        if (m_character)
        {//Задает родителя сделавшего выстрел.
            projectile.SetParentShooter(m_character);
        }

        m_refireTimer = m_turretProperties.RateOfFire;

        Sound sound = m_turretProperties.ProjectileSound;

        sound.Play();
    }


    //Задает свойства турели.
    public void AssignTurretProperties(TurretProperties props)
    {
        if (m_mode != props.Mode) return;

        m_refireTimer = 0;

        m_turretProperties = props;
    }
}
