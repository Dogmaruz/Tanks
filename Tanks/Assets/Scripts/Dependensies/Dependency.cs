using UnityEngine;

public abstract class Dependency : MonoBehaviour
{
    protected virtual void BindFoundMonoBehaviour(MonoBehaviour mono)
    {

    }

    protected void FindAllObjectToBind()
    {
        MonoBehaviour[] MonoInScene = FindObjectsOfType<MonoBehaviour>();

        for (int i = 0; i < MonoInScene.Length; i++)
        {
            BindFoundMonoBehaviour(MonoInScene[i]);
        }
    }

    protected void Bind<T>(MonoBehaviour mono, MonoBehaviour bindObject) where T : class
    {
        if (mono is IDependency<T>) (mono as IDependency<T>).Construct(bindObject as T);
    }
}
