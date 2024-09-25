using System;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Objetos Jugador")]
    [SerializeField] private GameObject _player;
    [SerializeField] public Transform _playerClamp;

    [Header("Velocidad del jugador")]  
    [SerializeField] private float _playerSpeed;

    // Opciones Player para moverse
    public Animator _playerAnimator;
    private bool _lookRight = true;
    public Rigidbody2D _playerRB;
    public Vector2 _playerDirection;

    // Inputs del sistema
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _interactionAction;

    // Objectos Que van a interactuar con el Player
    private IInteractuable _interactuableEnRango;

    private void Awake()
    {
        /*_objectInteract = FindObjectOfType<ObjectInteract>();
        _objectInteractable = _objectInteract._object;

        _climbInteractive = FindObjectOfType<ClimbInteractive>();
        _climbInteractable = _climbInteractive._arriba;

        _finishDay = FindObjectOfType<FinishDay>();
        _finish = _finishDay._objectBed;*/

        _playerInput = GetComponent<PlayerInput>();
        _playerRB = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        _moveAction = _playerInput.actions["Move"];
        _interactionAction = _playerInput.actions["Action"];
    }

    private void Start()
    {
        GlobalEventSystem.instance.dialogueStarted.AddListener(ActionDisable);
        GlobalEventSystem.instance.dialogueEnded.AddListener(ActionEnable);
    }

    private void OnEnable()
    {
        //GlobalEventSystem.instance.dialogueStarted.AddListener(ActionDisable);
        //GlobalEventSystem.instance.dialogueEnded.AddListener(ActionEnable);
    }

    private void OnDisable()
    {
        GlobalEventSystem.instance.dialogueStarted.RemoveListener(ActionDisable);
        GlobalEventSystem.instance.dialogueEnded.RemoveListener(ActionEnable);
    }

    private void Update()
    {
        _playerDirection = _moveAction.ReadValue<Vector2>();
        
        InteractuableObject();
        RotatePlayer(_playerDirection.x);
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void ActionEnable()
    {
        _moveAction.Enable();
    }

    public void ActionDisable()
    {
        _moveAction.Disable();
    }

    private void InteractuableObject()
    {
        if (_interactionAction.WasPerformedThisFrame() && _interactuableEnRango != null)
        {
            _interactuableEnRango.Interactuar(this);
        }
        //else if (_moveAction.ReadValue<Vector2>().y > 0 && _interactuableEnRango != null) 
        //{
        //    _interactuableEnRango.Interactuar(this);
        //}
    }

    /*public void InteractionObjectPublic()
    {
        if (!_isDropObject)
        {
            _interactionAction.started += InteractionTakeObject;
            _interactionAction.started -= InteractionDropObject;
            _interactionAction.started -= InteractionClimb;
            _interactionAction.started -= InteractionStopClimb;
        }
        else if (_isDropObject)
        {
            _interactionAction.started += InteractionDropObject;
            _interactionAction.started -= InteractionTakeObject;
            _interactionAction.started -= InteractionClimb;
            _interactionAction.started -= InteractionStopClimb;
        }
    }

    public void InteractionClimbPublic()
    {
        if (_isClimb)
        {
            _interactionAction.started += InteractionClimb;
        }
    }

    public void InteractionClimbStopPublic()
    {
        if (!_isClimb)
        {
            _interactionAction.started -= InteractionClimb;
        }
    }

    public void InteractionFinishDayPublic()
    {
        if (_isFinishDay)
        {
            _interactionAction.started += InteractionFinishDay;
            _interactionAction.started -= InteractionStopClimb;
            _interactionAction.started -= InteractionClimb;
            _interactionAction.started -= InteractionDropObject;
            _interactionAction.started -= InteractionTakeObject;
        }
        if (!_isFinishDay)
        {
            _interactionAction.started -= InteractionFinishDay;
        }
    }

    private void InteractionFinishDay(InputAction.CallbackContext context)
    {
        ScenesManager.instance.LoadNextScene();
    }

    private void InteractionClimb(InputAction.CallbackContext context)
    {
        
        _playerRB.gravityScale = 0;
        _playerAnimator.SetBool("Climb", true);
    }

    private void InteractionStopClimb(InputAction.CallbackContext context)
    {
        _player.transform.position = _climbInteractable.position;
        _playerAnimator.SetBool("Climb", false);
    }

    private void InteractionTakeObject(InputAction.CallbackContext context)
    {
        _objectInteractable.transform.SetParent(_playerClamp);
        _objectInteractable.transform.position = _playerClamp.position;
        _objectInteractable.GetComponent<Rigidbody2D>().isKinematic = true;
        _objectInteractable.gameObject.tag = "PickUpObject";
        _playerAnimator.SetBool("TakeObject", true);
    }

    private void InteractionDropObject(InputAction.CallbackContext context)
    {
        _objectInteractable.transform.SetParent(null);
        _objectInteractable.GetComponent<Rigidbody2D>().isKinematic = false;
        _objectInteractable.gameObject.tag = "InteractiveObject";
        _playerAnimator.SetBool("TakeObject", false);
    }*/

    private void Move()
    {
        _playerRB.velocity = new Vector2(_playerDirection.x * _playerSpeed, _playerDirection.y * _playerSpeed);
        _playerAnimator.SetFloat("Horizontal", MathF.Abs(_playerDirection.x)); 
    }

    private void RotatePlayer(float x)
    {
        if (x > 0 && !_lookRight)
        {
            Rotate();
        } else if (x < 0 && _lookRight)
        {
            Rotate();
        }
    }

    private void Rotate()
    {
        _lookRight = !_lookRight;
        Vector2 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractuable interactuable))
        {
            _interactuableEnRango = interactuable;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractuable interactuable))
        {
            _interactuableEnRango = null;
        }
    }

    private void PlayFootstep()
    {
        AudioManager.instance.PlayFootstep();
    }

    private void PlayCrown()
    {
        AudioManager.instance.PlayCorona();
    }
}