using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AI_MovementController : FP_MovementController, IDependency<A_Grid>
{
    [SerializeField] private float m_fireRadius;

    [SerializeField] private float m_patrolRadius;

    [SerializeField] private LayerMask m_layerMask;

    [SerializeField] private Color m_color;

    private A_Grid m_grid;

    private Pathfinding _pathfinding;

    private AI_CharacterController _characterAI;

    private Vector3 _patrolTarget;

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
        if (Player.Instance.CharacterController == null) return;

        FintMovePosition();

        FindAttackTarget();
    }

    private void FintMovePosition()
    {
        //Поиск цели.
        var patrolEnter = Physics2D.OverlapCircle(transform.position, m_patrolRadius, m_layerMask);

        if (patrolEnter.attachedRigidbody != null)
        {
            var destructible = patrolEnter.attachedRigidbody.GetComponent<FP_CharacterController>();

            if (destructible)
            {
                PathFinder(Player.Instance.CharacterController.transform.position);
            }
        }
        else
        {
            if (_patrolTarget != Vector3.zero)
            {
                PathFinder(_patrolTarget);
            }
            else
            {
                List<Node> walkableNodes = m_grid.GetWalkableNodes();

                Node node = walkableNodes[Random.Range(0, walkableNodes.Count)];

                _patrolTarget = node.worldPosition;
            }
        }
    }

    private void PathFinder(Vector3 target)
    {
        _pathfinding.FindPath(transform.position, target);

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

    
    private void FindAttackTarget()
    {
        //Поиск цели.
        var fireEnter = Physics2D.OverlapCircle(transform.position, m_fireRadius, m_layerMask);

        if (fireEnter.attachedRigidbody != null)
        {
            var destructible = fireEnter.attachedRigidbody.GetComponent<FP_CharacterController>();

            if (destructible)
            {
                _playerInputs.MouseButtonPrimaryDown = true;
            }
            else
            {
                _playerInputs.MouseButtonPrimaryDown = false;
            }
        }
        else
        {
            _playerInputs.MouseButtonPrimaryDown = false;
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

            _patrolTarget = Vector3.zero;
        }

        _playerInputs.Direction = direction;

        // Применение настроек ввода
        _characterAI.UpdateInputs(ref _playerInputs);
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Handles.color = m_color;

        Handles.DrawSolidDisc(transform.position, transform.forward, m_fireRadius);
    }
#endif
}
