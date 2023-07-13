using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelSequenceController : MonoBehaviour
{
    public event Action<bool> OnResult;

    public static string MainMenu_SceneNickName = "Main_Menu";
    public Episode CurrentEpisode { get; private set; } //Текущий эпизод.

    public int CurrentLevel { get; private set; } //Текущий уровень.

    public bool LastLevelResult { get; private set; }

    public int EpisodeCount { get; private set; } = 1; //Колличество пройденных эпизодов.

    //Старт эпизода.
    public void StartEpisode(Episode episode)
    {
        CurrentEpisode = episode;

        CurrentLevel = 0;

        SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
    }

    //Рестарт уровня.
    public void RestartLevel()
    {
        SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
    }

    //Завершение прохождения уровня.
    public void FinishCurrentLevel(bool success)
    {
        LastLevelResult = success;

        OnResult?.Invoke(success);
    }

    //Переход на другой  уровень если он есть, а иначе завершение эпизода и выход в главное меню.
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
