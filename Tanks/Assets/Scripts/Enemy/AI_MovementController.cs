using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AI_MovementController : FP_MovementController, IDependency<A_Grid>
{
    private A_Grid m_grid;

    private Pathfinding _pathfinding;

    private AI_CharacterController _characterAI;

    public void Construct(A_Grid obj)
    {
        m_grid = obj;
    }

    private void Awake()
    {
        _pathfinding = GetComponent<Pathfinding>();

        _characterAI = GetComponent<AI_CharacterController>();
    }

    protected override void CharacterInput()
    {

        _pathfinding.FindPath(transform.position, Player.Instance.CharacterController.transform.position);


        if (m_grid.path != null && m_grid.path.Count > 0)
        {
            // Перемещаем противника к следующему узлу на пути
            Vector3 targetPosition = m_grid.path[0].worldPosition;

            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            Move(moveDirection);

            // Если противник достиг следующей точки, удаляем ее из пути
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                m_grid.path.RemoveAt(0);
            }
        }
    }

    public void Move(Vector3 direction)
    {
        if (_characterAI == null) return;

        if (m_grid.path.Count > 4)
        {
            _playerInputs.MoveAxisForward = 1;
        }
        else
        {
            _playerInputs.MoveAxisForward = 0;
        }

        _playerInputs.Direction = direction;

        // Применение настроек ввода
        _characterAI.UpdateInputs(ref _playerInputs);
    }
}
