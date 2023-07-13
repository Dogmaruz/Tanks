using UnityEngine;
using Zenject;

public class UIPausePanel : MonoBehaviour
{
    [SerializeField] private GameObject m_pausePanel;

    [SerializeField] private GameObject m_resultPanel;

    private GamePause _gamePause;

    [Inject]
    public void Construct(GamePause gamePause)
    {
        _gamePause = gamePause;
    }

    private void Start()
    {
        m_pausePanel?.SetActive(false);

        _gamePause.PauseStateChange += OnPauseStateChange;
    }

    private void OnDestroy()
    {
        _gamePause.PauseStateChange -= OnPauseStateChange;
    }

    private void Update()
    {
        if (m_resultPanel.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            _gamePause.ChangePauseState();
        }
    }

    private void OnPauseStateChange(bool isPause)
    {
        m_pausePanel?.SetActive(isPause);
    }

    public void UnPause()
    {
        _gamePause.UnPause();
    }
}
