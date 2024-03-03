using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private bool _isGrounded;


    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        _isGrounded = _controller.isGrounded;
    }
    //будет получать входные данные из нашего скрипта inputmanager и применять их к контроллеру
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        _controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        _playerVelocity.y += gravity * Time.deltaTime;

        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }
        _controller.Move(_playerVelocity * Time.deltaTime);
        Debug.Log(_playerVelocity.y);
    }
    public void Jump()
    {
        if (_isGrounded)
        {
            _playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}
