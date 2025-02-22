using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerInputs _playerInputs;

    private void Awake()
    {
        _playerInputs = new PlayerInputs();
        _playerInputs.Player.Enable();
    }
    
    public bool IsJumpPressed()
    {
        return _playerInputs.Player.Jump.ReadValue<float>() > 0.5f;
    }

    public bool IsCrouchPressed()
    {
        return _playerInputs.Player.Crouch.ReadValue<float>() > 0.5f;
    }

    public bool IsSprintPressed()
    {
        return _playerInputs.Player.Sprint.ReadValue<float>() > 0.5f;
    }
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = _playerInputs.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    public Vector2 GetLookVector()
    {
        Vector2 lookVector = _playerInputs.Player.Look.ReadValue<Vector2>();
        return lookVector;
    }
}