using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    PlayerBaseState _currState;
    PlayerStateFactory _states;

    Rigidbody _playerRb;
    bool _jumpPressed = false;
    bool _grounded = false;

    Animator _playerAnimator;
    PlayerAttackController _playerAttackController;

    Vector2 _movement;
    bool _isRunning = false;
    [SerializeField] float _walkSpeed;
    [SerializeField] float _runSpeed;
    [SerializeField] float _jumpForce;

    bool _isAttacking = false;
    [SerializedDictionary("Condition", "Value")]
    [SerializeField] SerializedDictionary<string, bool> _stateChangeConditions = new SerializedDictionary<string, bool>();

    public PlayerBaseState CurrState { get { return _currState; } set { _currState = value; } }
    public Rigidbody PlayerRb { get { return _playerRb; } }
    public Animator PlayerAnimator { get { return _playerAnimator; } }
    public PlayerAttackController PlayerAttackController { get { return _playerAttackController; } }
    public bool JumpPressed { get { return _jumpPressed; } set { _jumpPressed = value; } }
    public bool Grounded { get { return _grounded; } set { _grounded = value; } }
    public Vector2 Movement { get { return _movement; } set { _movement = value; } }
    public float WalkSpeed { get { return _walkSpeed; } }
    public bool IsRunning { get { return _isRunning; } set { _isRunning= value; } }
    public float RunSpeed { get { return _runSpeed; } }
    public float JumpForce { get { return _jumpForce; } }
    public bool IsAttacking { get { return _isAttacking; } set { _isAttacking= value; } }
    public SerializedDictionary<string, bool> StateChangeConditions { get { return _stateChangeConditions; } set { _stateChangeConditions = value; } }

    void Awake()
    {
        //variables definition
        _playerRb = GetComponent<Rigidbody>();
        _playerAnimator = GetComponent<Animator>();
        _playerAttackController = GetComponent<PlayerAttackController>();

        //states initialization
        _states = new PlayerStateFactory(this);
        _currState = _states.Grounded();
        _currState.EnterState();
    }

    void Update()
    {
        _currState.UpdateStates();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _currState.OnCollisionEnter(collision);   
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && _grounded)
            _jumpPressed = true;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
            _movement = context.ReadValue<Vector2>().normalized;
        if (context.canceled)
            _movement = Vector2.zero;
    }

    public void SwitchRunMode(InputAction.CallbackContext context)
    {
        if(context.performed)
            _isRunning = true;
        if(context.canceled)
            _isRunning = false;
    }
}
