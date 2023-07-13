using UnityEngine;
using Zenject;

public class AI_CharacterController : FP_CharacterController
{
    Vector3 gunTarget;

    private Player _player;

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }

    public override void UpdateInputs(ref PlayerInputs playerInputs)
    {
        // Атака.
        if (playerInputs.MouseButtonPrimaryDown)
        {
            if (_player.CharacterController == null) return;

            gunTarget = _player.CharacterController.transform.position - m_TowerTransform.position;

            _mode = TurretMode.Primary;

            Fire();
        }
        else
        {
            gunTarget = playerInputs.Direction;
        }

        // Перемещение танка
        Vector2 movement = transform.up * playerInputs.MoveAxisForward * m_moveSpeed;

        if (_rigibody.bodyType != RigidbodyType2D.Static)
        {
            _rigibody.velocity = movement;
        }

        // Поворот танка
        if (playerInputs.Direction != Vector3.zero)
        {

            float angle = Mathf.Atan2(playerInputs.Direction.y, playerInputs.Direction.x) * Mathf.Rad2Deg - 90;

            Quaternion target = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, target, m_tankRotationSpeed * Time.deltaTime);
        }

        // Поворот башни

        Vector2 direction = gunTarget;

        float angleTower = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        Quaternion targetTower = Quaternion.AngleAxis(angleTower, Vector3.forward);

        m_TowerTransform.rotation = Quaternion.Slerp(m_TowerTransform.rotation, targetTower, m_towerRotationSpeed * Time.deltaTime);
    }
}
