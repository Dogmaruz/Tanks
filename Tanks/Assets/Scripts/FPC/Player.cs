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

    [Inject]
    public void Construct(GamePause gamePause, ShakeCamera shakeCamera, LevelResultController levelResultController, FP_CharacterController characterController, LevelSequenceController levelSequenceController)
    {
        _levelResultController = levelResultController;

        _characterController = characterController;

        _gamePause = gamePause;

        _levelSequenceController = levelSequenceController;
    }

    protected void Awake()
    {
        _gamePause.UnPause();

        _levelSequenceController.OnResult += ShowResultPanel;
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
