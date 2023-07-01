using UnityEngine;

public class Box : MonoBehaviour, IDependency<A_Grid>
{
    private A_Grid _grid;

    public void Construct(A_Grid obj)
    {
        _grid = obj;
    }

    public void UpdateGrid()
    {
        _grid.CreateGrid();
    }
}
