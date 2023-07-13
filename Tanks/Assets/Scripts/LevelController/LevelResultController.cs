using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelResultController : MonoBehaviour
{
    [SerializeField] private Text m_resultText;

    [SerializeField] private GameObject m_panel;

    public event Action OnShowPanel;

    private void Start()
    {
        m_panel.SetActive(false);
    }

    //Открывает окно победы - поражения.
    public void ShowResults(bool success)
    {
        m_panel.SetActive(true);

        m_resultText.text = success ? "Level Completed" : "You Lose";

        OnShowPanel?.Invoke();
    }
}
