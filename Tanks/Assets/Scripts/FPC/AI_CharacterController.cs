using UnityEngine;

public class AI_CharacterController : FP_CharacterController
{
    public override void UpdateInputs(ref PlayerInputs playerInputs)
    {
        // Атака.
        if (playerInputs.MouseButtonDown)
        {
            Debug.Log("Огонь");
        }

        // Перемещение танка
        Vector2 movement = transform.up * playerInputs.MoveAxisForward * m_forceSpeed;

        _rigibody.velocity = movement;

        // Поворот танка
        float angle = Mathf.Atan2(playerInputs.Direction.y, playerInputs.Direction.x) * Mathf.Rad2Deg - 90;

        Quaternion target = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, target, m_tankRotationSpeed * Time.deltaTime);

        //transform.rotation = Quaternion.Euler(0, 0, angle);

        //Поворот башни

        Vector3 playerPosition = Player.Instance.CharacterController.transform.position;

        Vector2 direction = playerPosition - m_TowerTransform.position;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        target = Quaternion.AngleAxis(angle, Vector3.forward);

        m_TowerTransform.rotation = Quaternion.Slerp(m_TowerTransform.rotation, target, m_towerRotationSpeed * Time.deltaTime);
    }
}
