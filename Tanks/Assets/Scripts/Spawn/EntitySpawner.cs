using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EntitySpawner : MonoBehaviour
{
    public enum SpawnMode
    {
        Start,
        Loop
    }

    [SerializeField] private GameObject[] m_entityPrefabs;

    [SerializeField] private SpawnMode m_spawnMode;

    [SerializeField] private int m_numSpawns;

    [SerializeField] private float m_respawnTime;

    [SerializeField] private float m_maxDistanceSpawnToPlayer;

    private float _timer;

    private A_Grid _grid;

    private Player _player;

    private DiContainer _diContainer;

    [Inject]
    public void Construct(A_Grid grid, Player player, DiContainer diContainer)
    {
        _player = player;

        _grid = grid;

        _diContainer = diContainer;
    }

    private void Start()
    {
        if (m_spawnMode == SpawnMode.Start)
        {
            SpawnEntities();
        }

        _timer = m_respawnTime;
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }

        if (m_spawnMode == SpawnMode.Loop && _timer < 0)
        {
            SpawnEntities();

            _timer = m_respawnTime;
        }
    }

    //Спавн объектов.
    private void SpawnEntities()
    {
        List<Node> walkableNodes = _grid.GetWalkableNodes();

        int entitiesSpawned = 0;

        while (entitiesSpawned < m_numSpawns && walkableNodes.Count > 0)
        {
            int index = Random.Range(0, m_entityPrefabs.Length);

            Node node = walkableNodes[Random.Range(0, walkableNodes.Count)];

            if (node.IsActive == false)
            {
                Vector3 target = node.worldPosition;

                var distance = Vector2.Distance(_player.CharacterController.transform.position, target);

                node.IsActive = true;

                if (distance >= m_maxDistanceSpawnToPlayer)
                {
                    GameObject newEntity = _diContainer.InstantiatePrefab(m_entityPrefabs[index].gameObject);

                    var enemy = newEntity.GetComponent<AI_MovementController>();

                    if (enemy != null)
                    {
                        _grid.CreateGrid();
                    }

                    newEntity.transform.position = target;

                    entitiesSpawned++;
                }
            }

            walkableNodes.Remove(node);
        }

        _grid.CreateGrid();
    }
}
