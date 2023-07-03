using System.Collections.Generic;
using UnityEngine;

public class AI_MovementController : FP_MovementController, IDependency<A_Grid>
{
    [SerializeField] private float m_fireRadius;

    [SerializeField] private float m_patrolRadius;

    [SerializeField] private LayerMask m_layerMask;

    [SerializeField] private Color m_color;

    [SerializeField] private bool m_IsWalkable;

    private A_Grid _grid;
    public A_Grid Grid { get => _grid; set => _grid = value; }

    private Pathfinding _pathfinding;

    private AI_CharacterController _characterAI;

    private Vector3 _patrolTarget;

    private bool _isTarget;

    private float _positionTimer;

    public void Construct(A_Grid obj)
    {
        _grid = obj;
    }

    private void Start()
    {
        _pathfinding = GetComponent<Pathfinding>();

        _characterAI = GetComponent<AI_CharacterController>();

        _isTarget = false;

        _positionTimer = 0;
    }

    protected override void CharacterInput()
    {
        if (Player.Instance.CharacterController == null) return;

        FindMovePosition();

        FindAttackTarget();
    }

    private void FindMovePosition()
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
            else
            {
                _isTarget = false;
            }
        }

        if (_isTarget == true)
        {
            PathFinder(_patrolTarget);
        }
        else
        {
            List<Node> walkableNodes = _grid.GetWalkableNodes();

            Node node = walkableNodes[Random.Range(0, walkableNodes.Count - 1)];

            _patrolTarget = node.worldPosition;

            _isTarget = true;
        }
    }

    private void PathFinder(Vector3 target)
    {
        if (_pathfinding.Path.Count <= 0)
        {
            _pathfinding.FindPath(transform.position, target);
        }

        if (_pathfinding.Path.Count > 0)
        {
            // Перемещаем противника к следующему узлу на пути
            Vector3 targetPosition = _pathfinding.Path[0].worldPosition;

            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            Move(moveDirection);

            // Если противник достиг следующей точки, удаляем ее из пути
            if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
            {
                _pathfinding.Path.RemoveAt(0);

                _positionTimer = 0;
            }
            else
            {
                _positionTimer += Time.deltaTime;
            }

            if (_positionTimer > 1)
            {
                _isTarget = false;

                _positionTimer = 0;

                _pathfinding.Path.Clear();
            }
        }
        else
        {
            _isTarget = false;

            _positionTimer = 0;

            _pathfinding.Path.Clear();
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

        if (_pathfinding.Path.Count > 1)
        {
            _playerInputs.MoveAxisForward = 1;

            _playerInputs.Direction = direction;
        }
        else
        {
            _playerInputs.MoveAxisForward = 0;

            _isTarget = false;

            _pathfinding.Path.Clear();
        }

        if (!m_IsWalkable)
        {
            _playerInputs.MoveAxisForward = 0;

            _playerInputs.Direction = Vector3.zero;
        }

        // Применение настроек ввода
        _characterAI.UpdateInputs(ref _playerInputs);
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        //Handles.color = m_color;

        //Handles.DrawSolidDisc(transform.position, transform.forward, m_fireRadius);

        //Отрисовка пути

        if (_pathfinding)
        {
            foreach (Node n in _pathfinding.Path)
            {
                Gizmos.color = Color.black;

                Gizmos.DrawCube(n.worldPosition, Vector3.one * (_grid.nodeRadius * 2 - 0.1f));
            }
        }
    }
#endif

}
