using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Projectile : Entity
{
    [SerializeField] private LayerMask m_laeyerMask;

    [SerializeField] private float m_Velocity; //Скорость.
    public float Velocity => m_Velocity;

    [SerializeField] private float m_LifeTime; //Время жизни.

    [SerializeField] protected int m_Damage; //Величина урона.

    [SerializeField] private ImpactEffect m_ImpactEffectPrefab;

    private float m_Timer;

    [SerializeField] private UnityEvent m_EventOnDeath;
    public UnityEvent EventOnDeath => m_EventOnDeath;

    [SerializeField] private UnityEvent<Destructible> m_EventOnHit;
    public UnityEvent<Destructible> EventOnHit => m_EventOnHit;

    private ShakeCamera _shakeCamera;

    private Player _player;

    [Inject]
    public void Construct(ShakeCamera shakeCamera, Player player)
    {
        _player = player;

        _shakeCamera = shakeCamera;
    }

    private void Update()
    {
        float stepLenght = Time.deltaTime * m_Velocity;

        Vector2 step = transform.up * stepLenght;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLenght, m_laeyerMask);

        //Проверка на столкновение пули с объектом.

        if (hit)
        {
            OnHit(hit);

            OnProjectileLifeEnd(hit);
        }

        m_Timer += Time.deltaTime;

        if (m_Timer > m_LifeTime)
        {
            m_EventOnDeath?.Invoke();

            Destroy(gameObject);
        }

        transform.position += new Vector3(step.x, step.y, 0);
    }

    protected virtual void OnHit(RaycastHit2D hit)
    {
        Destructible destructible = DeterminingHit(hit);

        if (destructible != null && destructible != m_ParentDestructible)
        {
            destructible.ApplyDamage(m_Damage);

            EventOnHit?.Invoke(destructible);

            AddPoints(destructible);
        }
    }

    private Destructible DeterminingHit(RaycastHit2D hit)
    {
        Destructible destructible = hit.collider.transform.root.GetComponent<Destructible>();

        if (destructible == null) destructible = hit.collider.GetComponentInParent<Destructible>();
        return destructible;
    }

    //Уничтожение с вызовом эфекта после попадания.
    private void OnProjectileLifeEnd(RaycastHit2D hit)
    {
        if (m_ImpactEffectPrefab)
        {
            Instantiate(m_ImpactEffectPrefab, transform.position, Quaternion.identity);
        }

        if (m_ParentDestructible.Nickname == "Player")
        {
            _shakeCamera.Shake();
        }

        m_EventOnDeath?.Invoke();

        Destroy(gameObject);
    }

    private Destructible m_ParentDestructible;
    public Destructible ParentDestructible { get => m_ParentDestructible; set => m_ParentDestructible = value; }

    //Задает родителя в классе Turret при выстреле.
    public void SetParentShooter(Destructible parentDestructible)
    {
        m_ParentDestructible = parentDestructible;
    }

    //Добавляет очки и колличество уничтоженных объектов.
    public void AddPoints(Destructible destructible)
    {

        if (destructible.CurrentHitPoint <= 0)
        {
            _player.AddScore(destructible.ScoreValue);
        }
    }

    public void SetShakeCamera(ShakeCamera shakeCamera)
    {
        _shakeCamera = shakeCamera;
    }
}

