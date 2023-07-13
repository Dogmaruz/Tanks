using UnityEngine;
using Zenject;

public class RecalculationGrid : MonoBehaviour
{
    private A_Grid _grid;

    [Inject]
    public void Construct(A_Grid grid)
    {
        _grid = grid;
    }

    public void UpdateGrid()
    {
        _grid.CreateGrid();
    }
}
