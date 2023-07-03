using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelResultController : SingletonBase<LevelResultController>
{
    [SerializeField] private Text m_resultText;

    [SerializeField] private GameObject m_panel;

    public event Action OnShowPanel;

    private bool _success;

    private void Start()
    {
        m_panel.SetActive(false);
    }

    //Открывает окно победы - поражения.
    public void ShowResults(bool success)
    {
        m_panel.SetActive(true);

        _success = success;

        m_resultText.text = success ? "Level Completed" : "You Lose";

        OnShowPanel?.Invoke();
    }
}
