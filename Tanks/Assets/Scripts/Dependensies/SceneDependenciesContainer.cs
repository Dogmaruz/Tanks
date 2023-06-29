using UnityEngine;

public class SceneDependenciesContainer : Dependency
{
    [SerializeField] private FP_MovementController m_movementController;


    protected override void BindFoundMonoBehaviour(MonoBehaviour mono)
    {
        Bind<FP_MovementController>(mono, m_movementController);
    }

    private void Awake()
    {
        FindAllObjectToBind();
    }
}
