using UnityEngine;

public class RecalculationGrid : MonoBehaviour, IDependency<A_Grid>
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

    public void SetGrid(A_Grid grid)
    {
        _grid = grid;
    }
}
