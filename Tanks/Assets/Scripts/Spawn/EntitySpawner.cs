using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour, IDependency<A_Grid>
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

    public void Construct(A_Grid obj)
    {
        _grid = obj;
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

                var distance = Vector2.Distance(Player.Instance.CharacterController.transform.position, target);

                node.IsActive = true;

                if (distance >= m_maxDistanceSpawnToPlayer)
                {
                    GameObject newEntity = Instantiate(m_entityPrefabs[index].gameObject);

                    var enemy = newEntity.GetComponent<AI_MovementController>();

                    if (enemy != null)
                    {
                        _grid.CreateGrid();

                        enemy.Grid = _grid;

                        enemy.GetComponent<Pathfinding>().SetGrid(_grid);
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
