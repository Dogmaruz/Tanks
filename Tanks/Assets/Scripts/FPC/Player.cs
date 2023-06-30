using UnityEngine;


public class Player : SingletonBase<Player>, IDependency<FP_MovementController>
{
    [SerializeField] private int m_HP;

    [SerializeField] private Transform m_respawnPoint;

    private FP_CharacterController m_characterController; //������ �� ������.
    public FP_CharacterController CharacterController => m_characterController;

    private FP_MovementController _movementController;

    public void Construct(FP_MovementController obj)
    {
        _movementController = obj;
    }

    protected override void Awake()
    {
        base.Awake();

        Respawn();
    }

    //���������� ��� ����������� ������.
    private void OnPlayerDeath()
    {
        Invoke(nameof(Respawn), 0.3f);
    }

    /// <summary>
    /// ����������� ������ ������.
    /// </summary>
    private void Respawn()
    {
        if (LevelSequenceController.Instance.CharacterController != null)
        {
            var newPlayer = Instantiate(LevelSequenceController.Instance.CharacterController, m_respawnPoint.position, Quaternion.identity);

            m_characterController = newPlayer.GetComponent<FP_CharacterController>();

            m_characterController.EventOnDeath?.AddListener(OnPlayerDeath);

            m_characterController.SetHitpoints(m_HP);

            _movementController.SetTargetCharacterController(m_characterController);
        }
    }

    #region Score

    public int Score { get; private set; } //����.
    public int NumKills { get; private set; } //����������� ������������ ��������.

    //��������� ����������� ������������ ��������.
    public void AddKill()
    {
        NumKills++;
    }

    //����������� ����.
    public void AddScore(int num)
    {
        Score += num;
    }

    #endregion
}
