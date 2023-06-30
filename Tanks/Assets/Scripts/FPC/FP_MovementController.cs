using UnityEngine;

public enum CharacterState
{
    Attack,
    Idel,
    Move,
}


public struct PlayerInputs
{
    public float MoveAxisForward;

    public float MoveAxisRight;

    public Vector3 MousePosition;

    public bool MouseButtonDown;

    public Vector3 Direction;
}

public class FP_MovementController : MonoBehaviour
{
    protected FP_CharacterController _character;

    protected const string _horizontalInput = "Horizontal";

    protected const string _verticalInput = "Vertical";


    protected PlayerInputs _playerInputs;

    private void Start()
    {
        _playerInputs = new PlayerInputs();
    }
    private void LateUpdate()
    {
        CameraInput();
    }

    private void Update()
    {
        // Премещение персонажа с клавиатуры
        CharacterInput();
    }

    private void FixedUpdate()
    {
        if (_character == null) return;

        _character.FixedUpdateInputs(ref _playerInputs);
    }

    protected void CameraInput()
    {

    }

    protected virtual void CharacterInput()
    {
        if (_character == null) return;

        _playerInputs.MoveAxisForward = Input.GetAxisRaw(_verticalInput);

        _playerInputs.MoveAxisRight = Input.GetAxisRaw(_horizontalInput);

        _playerInputs.MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _playerInputs.MouseButtonDown = Input.GetMouseButtonDown(0);

        // Применение настроек ввода
        _character.UpdateInputs(ref _playerInputs);
    }

    public void SetTargetCharacterController(FP_CharacterController characterController)
    {
        _character = characterController;
    }
}
