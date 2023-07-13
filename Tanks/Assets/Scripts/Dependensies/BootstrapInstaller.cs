using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private GamePause m_gamePause;

    [SerializeField] private LevelSequenceController m_levelSequenceController;

    public override void InstallBindings()
    {
        Container.
            Bind<GamePause>().FromInstance(m_gamePause).AsSingle();

        Container.
            Bind<LevelSequenceController>().FromInstance(m_levelSequenceController).AsSingle();
    }
}