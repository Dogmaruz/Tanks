﻿using UnityEngine;
using UnityEngine.Events;
using Zenject;

public interface ILevelCondition
{
    bool IsCompleted { get; }
}

public class LevelController : MonoBehaviour
{
    [SerializeField] protected float m_ReferenceTime; //Время для выполнения условий.
    public float ReferenceTime => m_ReferenceTime;

    [SerializeField] protected UnityEvent m_EventLevelCompleted;

    private ILevelCondition[] m_Conditions; //Массив всех уровней.

    private bool m_IsLevelCompleted;

    private float m_LevelTime; //Время затраченное на прохождение уровня.
    public float LevelTime => m_LevelTime;

    private LevelSequenceController _levelSequenceController;

    [Inject]
    public void Construct(LevelSequenceController levelSequenceController)
    {
        _levelSequenceController = levelSequenceController;
    }

    protected void Start()
    {
        m_Conditions = GetComponentsInChildren<ILevelCondition>();
    }

    void Update()
    {
        if (m_ReferenceTime > 0)
        {
            m_ReferenceTime -= Time.deltaTime;
        }

        if (!m_IsLevelCompleted)
        {
            m_LevelTime += Time.deltaTime;

            if (m_ReferenceTime <= 0)
            {
                CheckLevelConditions();
            }
        }
    }

    //Проверка на завершение уровней.
    private void CheckLevelConditions()
    {
        if (m_Conditions == null || m_Conditions.Length == 0) return;

        int numCompleted = 0;

        foreach (var v in m_Conditions)
        {
            if (v.IsCompleted) numCompleted++;
        }

        if (numCompleted == m_Conditions.Length)
        {
            m_IsLevelCompleted = true;

            m_EventLevelCompleted?.Invoke();

            _levelSequenceController.FinishCurrentLevel(true);
        }
    }
}
