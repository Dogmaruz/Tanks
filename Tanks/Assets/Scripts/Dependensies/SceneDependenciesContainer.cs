using UnityEngine;

public class SceneDependenciesContainer : Dependency
{
    [SerializeField] private FP_MovementController m_movementController;

    [SerializeField] private A_Grid m_grid;

    [SerializeField] private LevelResultController m_levelResultController;

    protected override void BindFoundMonoBehaviour(MonoBehaviour mono)
    {
        Bind<FP_MovementController>(mono, m_movementController);

        Bind<A_Grid>(mono, m_grid);

        Bind<LevelResultController>(mono, m_levelResultController);
    }

    private void Awake()
    {
        FindAllObjectToBind();
    }
}
