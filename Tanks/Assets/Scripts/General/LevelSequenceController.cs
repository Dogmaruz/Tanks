using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelSequenceController : MonoBehaviour
{
    public event Action<bool> OnResult;

    public static string MainMenu_SceneNickName = "Main_Menu";
    public Episode CurrentEpisode { get; private set; } //������� ������.

    public int CurrentLevel { get; private set; } //������� �������.

    public bool LastLevelResult { get; private set; }

    public int EpisodeCount { get; private set; } = 1; //����������� ���������� ��������.

    //����� �������.
    public void StartEpisode(Episode episode)
    {
        CurrentEpisode = episode;

        CurrentLevel = 0;

        SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
    }

    //������� ������.
    public void RestartLevel()
    {
        SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
    }

    //���������� ����������� ������.
    public void FinishCurrentLevel(bool success)
    {
        LastLevelResult = success;

        OnResult?.Invoke(success);
    }

    //������� �� ������  ������� ���� �� ����, � ����� ���������� ������� � ����� � ������� ����.
    public void AdvanceLevel()
    {
        CurrentLevel++;


        if (CurrentEpisode.Levels.Length <= CurrentLevel)
        {
            EpisodeCount++;

            SceneManager.LoadScene(MainMenu_SceneNickName);
        }
        else
        {
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }
    }
}
