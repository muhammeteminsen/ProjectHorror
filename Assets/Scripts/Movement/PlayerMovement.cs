using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InputHandler))]
public class PlayerMovement : MonoBehaviour
{
    [Header("----References")]
    private InputHandler _inputHandler;
    [Header("----Movement")] 
    [SerializeField, Range(2f, 100f)] private float movementSpeed = 10f;
    [SerializeField] private float movementDeceleration = 5f;
    [SerializeField] private float movementAcceleration = 5f;
    public Vector2 InputVector { get; private set; }
    [Header("----Sprint")] 
    [SerializeField, Range(4f, 200f)] private float sprintSpeed = 20f;
    [SerializeField] private float sprintAcceleration = 5f;
    [SerializeField] private float sprintDeceleration = 5f;
    private float _defaultMovementSpeed;
    [Header("----Camera")] 
    [SerializeField, Range(5f, 1000f)] private float mouseSensitivity = 15f;
    [SerializeField, Range(0f, 360f)] private float maxCameraVerticalAngle = 50f;
    private Camera _mainCamera;
    private float _xRotation;
    private float _yRotation;
    private GameObject _cameraPivot;
    [Header("----Jump")]
    [SerializeField] private float jumpFall = 5f;
    private float _groundCheckDistance;
    public bool IsGroundedRay { get; private set; }
    private Transform _groundCheck;
    [Header("----Components")] 
    private Rigidbody _rb;
    private Animator _animator;
    private void Awake()
    {
        _inputHandler = GetComponent<InputHandler>();
        _rb = GetComponent<Rigidbody>();
        _cameraPivot = GameObject.FindGameObjectWithTag("MainCameraPivot");
        _groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;
    }
    private void Start()
    {
        _mainCamera = Camera.main;
        // Cursor.lockState = CursorLockMode.Locked;
        _defaultMovementSpeed = movementSpeed;
    }
    private void Update()
    {
        Movement();
        CameraRotation();
    }

     private void FixedUpdate()
     {
         GravityControl();
    }

    private void Movement()
    {
        InputVector = _inputHandler.GetMovementVectorNormalized();
        Vector3 moveDirection = transform.right * InputVector.x + transform.forward * InputVector.y;
        Vector3 movement = moveDirection.normalized * movementSpeed;
        if (InputVector.x != 0 || InputVector.y  != 0)
        {
            _rb.linearVelocity = Vector3.Lerp(_rb.linearVelocity, movement,
                movementAcceleration * Time.deltaTime);
            Sprint(InputVector.y);
            
        }
        else if (InputVector is { x: 0, y: 0 })
        {
            _rb.linearVelocity = Vector3.Slerp(_rb.linearVelocity, Vector3.zero, Time.deltaTime * movementDeceleration);
            movementSpeed = Mathf.Lerp(movementSpeed, _defaultMovementSpeed, sprintDeceleration * Time.deltaTime);
        }
    }
    private void Sprint(float inputY)
    {
        if (_inputHandler.IsSprintPressed() && inputY > 0)
        {
            movementSpeed = Mathf.Lerp(movementSpeed, sprintSpeed, sprintAcceleration * Time.deltaTime);
        }
        else
        {
            movementSpeed = Mathf.Lerp(movementSpeed, _defaultMovementSpeed, sprintDeceleration * Time.deltaTime);
        }
    }
    private void GravityControl()
    {
        IsGroundedRay = Physics.Raycast(_groundCheck.position, Vector3.down, 3f);
        if (IsGroundedRay) return;
        if (_rb.linearVelocity.y < 0)
        {
            _rb.AddForce(Vector3.up * (Physics.gravity.y * jumpFall), ForceMode.Acceleration);
        }
    }
    private void CameraRotation()
    {
        _mainCamera.transform.position = _cameraPivot.transform.position;
        Vector2 lookVector = _inputHandler.GetLookVector();
        float mouseX = lookVector.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookVector.y * mouseSensitivity * Time.deltaTime;
        _xRotation -= mouseY;
        _yRotation += mouseX;
        _xRotation = Mathf.Clamp(_xRotation, -maxCameraVerticalAngle, maxCameraVerticalAngle);
        _mainCamera.transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y + mouseX, 0f);
    }
} 


