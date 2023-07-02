using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_mainMenu;

    private const string _START_SCENE_NAME = "Tanks";

    public void NewGame()
    {
        SceneManager.LoadScene(_START_SCENE_NAME);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
