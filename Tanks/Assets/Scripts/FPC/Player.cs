using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [SerializeField] private int m_HP;

    [SerializeField] private Transform m_respawnPoint;

    private FP_CharacterController _characterController; //Ссылка на игрока.
    public FP_CharacterController CharacterController => _characterController;

    private LevelResultController _levelResultController;

    private GamePause _gamePause;

    private LevelSequenceController _levelSequenceController;

    private DiContainer _diContainer;

    [Inject]
    public void Construct(DiContainer diContainer, GamePause gamePause, ShakeCamera shakeCamera, LevelResultController levelResultController, LevelSequenceController levelSequenceController)
    {
        _diContainer = diContainer;

        _levelResultController = levelResultController;

        _gamePause = gamePause;

        _levelSequenceController = levelSequenceController;
    }

    protected void Awake()
    {
        _gamePause.UnPause();

        _levelSequenceController.OnResult += ShowResultPanel;

        Respawn();
    }

    private void Respawn()
    {
        if (_levelSequenceController.CharacterController != null)
        {
            var newPlayer = _diContainer.InstantiatePrefab(_levelSequenceController.CharacterController, m_respawnPoint.position, Quaternion.identity, null);

            _characterController = newPlayer.GetComponent<FP_CharacterController>();

            _characterController.EventOnDeath?.AddListener(OnPlayerDeath);

            GetComponent<FP_MovementController>().SetTargetCharacterController(newPlayer.GetComponent<FP_CharacterController>());
        }

    }

    private void OnDestroy()
    {
        _characterController.EventOnDeath?.RemoveListener(OnPlayerDeath);

        _levelSequenceController.OnResult -= ShowResultPanel;
    }

    //Вызывается при уничтожении игрока.
    public void OnPlayerDeath()
    {
        ShowResultPanel(false);
    }

    public void ShowResultPanel(bool success)
    {
        _levelResultController.ShowResults(success);
    }

    public int Score { get; private set; } //Счет.

    //Увеличивает счет.
    public void AddScore(int num)
    {
        Score += num;
    }
}
