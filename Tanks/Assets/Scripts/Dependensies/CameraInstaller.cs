using UnityEngine;
using Zenject;

public class CameraInstaller : MonoInstaller
{
    [SerializeField] private ShakeCamera m_shakeCamera;

    public override void InstallBindings()
    {
        Container.Bind<ShakeCamera>().FromInstance(m_shakeCamera).AsSingle();
    }
}