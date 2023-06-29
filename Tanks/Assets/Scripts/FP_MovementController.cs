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


}

public class FP_MovementController : MonoBehaviour
{
    private FP_CharacterController _character;

    private FP_CameraController _characterCamera;

    private const string _horizontalInput = "Horizontal";

    private const string _verticalInput = "Vertical";


    private PlayerInputs _playerInputs;

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

    private void CameraInput()
    {

    }

    private void CharacterInput()
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
