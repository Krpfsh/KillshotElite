using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerInput.OnFootActions _onFoot;
    private PlayerMotor _motor;
    private PlayerLook _look;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _onFoot = _playerInput.OnFoot;
        _motor = GetComponent<PlayerMotor>();
        _look = GetComponent<PlayerLook>();
        _onFoot.Jump.performed += ctx => _motor.Jump();
    }
    private void FixedUpdate()
    {
        //скажем Playermotor двигаться используя значения из нашего movement action
        _motor.ProcessMove(_onFoot.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {
        _look.ProcessLook(_onFoot.Look.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        _onFoot.Enable();
    }
    private void OnDisable()
    {
        _onFoot.Disable();
    }
}
