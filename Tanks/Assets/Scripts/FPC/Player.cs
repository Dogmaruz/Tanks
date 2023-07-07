using UnityEngine;

public class Player : SingletonBase<Player>, IDependency<FP_MovementController>, IDependency<LevelResultController>, IDependency<ShakeCamera>
{
    [SerializeField] private int m_HP;

    [SerializeField] private Transform m_respawnPoint;

    private FP_CharacterController m_characterController; //Ссылка на игрока.
    public FP_CharacterController CharacterController => m_characterController;

    private FP_MovementController _movementController;

    private LevelResultController _levelResultController;

    private ShakeCamera _shakeCamera;

    public void Construct(ShakeCamera obj)
    {
        _shakeCamera = obj;
    }

    public void Construct(FP_MovementController obj)
    {
        _movementController = obj;
    }

    public void Construct(LevelResultController obj)
    {
        _levelResultController = obj;
    }

    protected override void Awake()
    {
        base.Awake();

        Respawn();
    }

    private void OnDestroy()
    {
        m_characterController.EventOnDeath?.RemoveListener(OnPlayerDeath);
    }

    //Вызывается при уничтожении игрока.
    private void OnPlayerDeath()
    {
        ShowResultPanel(false);
    }

    public void ShowResultPanel(bool success)
    {
        _levelResultController.ShowResults(success);
    }

    /// <summary>
    /// Перерождает кораль игрока.
    /// </summary>
    private void Respawn()
    {
        if (LevelSequenceController.Instance.CharacterController != null)
        {
            var newPlayer = Instantiate(LevelSequenceController.Instance.CharacterController, m_respawnPoint.position, Quaternion.identity);

            m_characterController = newPlayer.GetComponent<FP_CharacterController>();

            m_characterController.EventOnDeath?.AddListener(OnPlayerDeath);

            m_characterController.SetHitpoints(m_HP);

            var turrets = m_characterController.GetComponentsInChildren<Turret>();

            foreach (var turret in turrets)
            {
                turret.SetShakeCamera(_shakeCamera);
            }

            _movementController.SetTargetCharacterController(m_characterController);
        }
    }

    #region Score

    public int Score { get; private set; } //Счет.

    //Увеличивает счет.
    public void AddScore(int num)
    {
        Score += num;
    }

    #endregion
}
