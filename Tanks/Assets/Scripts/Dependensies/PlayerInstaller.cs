using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private FP_CharacterController m_characterController;

    [SerializeField] private Transform m_characterSpawnPoint;

    [SerializeField] private Player m_player;

    private FP_CharacterController playerInstance;

    public override void InstallBindings()
    {
        BindPlayer();
    }

    private void BindPlayer()
    {
        Container.
            Bind<Player>().FromInstance(m_player).AsSingle();

        playerInstance = Container.
            InstantiatePrefabForComponent<FP_CharacterController>(m_characterController, m_characterSpawnPoint.position, Quaternion.identity, null);

        playerInstance.EventOnDeath?.AddListener(m_player.OnPlayerDeath);

        Container.
            Bind<FP_CharacterController>().FromInstance(playerInstance).AsSingle();
    }

    private void OnDestroy()
    {
        playerInstance.EventOnDeath?.RemoveListener(m_player.OnPlayerDeath);
    }
}