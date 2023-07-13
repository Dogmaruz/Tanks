using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private A_Grid m_grid;

    [SerializeField] private LevelResultController m_levelResultController;

    public override void InstallBindings()
    {
        Container.Bind<A_Grid>().FromInstance(m_grid).AsSingle().NonLazy();

        Container.Bind<LevelResultController>().FromInstance(m_levelResultController).AsSingle().NonLazy();
    }
}