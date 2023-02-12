using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float jumpMagnitude = 18f;

    private const string JUMP_KEY = "Jump";
    private const string CROUCH_KEY = "Crouch";
    private const string MOVE_AXIS = "Movement";
    private const string PUNCH_KEY = "z";
    private const string KICK_KEY = "x";

    private float TURNING_POINT_X = 0f;

    private enum MovementState { 
        idle,               // 0
        movingForward,      // 1
        movingBackward,     // 2
        jumping,            // 3
        falling,            // 4
        crouching           // 5
    }

    private float dirX = 0f;
    private bool pressedJump = false;
    private bool isGrounded = false;    // start off the ground
    private bool pressedCrouch = false;
    private bool isCrouching = false;

    private Animator anim;
    private CharacterController controller;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private InputActionAsset inputAsset;
    private InputActionMap player;


    private void Awake()
    {
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        player.FindAction("Jump").started += OnJump;
        player.FindAction("Crouch").started += OnCrouch;
        player.Enable();
    }

    private void OnDisable()
    {
        player.FindAction("Jump").started -= OnJump;
        player.FindAction("Crouch").started -= OnCrouch;
        player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        dirX = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        pressedJump = context.action.triggered;
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        pressedCrouch = context.action.triggered;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle crouching
        if (pressedCrouch && isGrounded)
        {
            isCrouching = true;
            rb.velocity = new Vector2(0f, 0f);
        }
        else
        {
            isCrouching = false;
        }

        // Handle horizontal movement
        if (isGrounded && !isCrouching) // if in the air, horizontal momentum is locked
        {
            // dirX = Input.GetAxisRaw(MOVE_AXIS);
            rb.velocity = new Vector2(dirX * horizontalSpeed, rb.velocity.y);
        }

        // Handle jumping
        if (rb.velocity.y < 0.01f && rb.velocity.y > -0.01f && !isGrounded) // landing
        {
            isGrounded = true;
        }
        if (pressedJump && isGrounded)                    // jumping off ground
        {
            if (isCrouching)     // jumping from a crouch should maintain horizontal movement
            {
                rb.velocity = new Vector2(dirX * horizontalSpeed, rb.velocity.y);
            }
            rb.velocity = new Vector3(rb.velocity.x, jumpMagnitude, 0f);
            isGrounded = false;
        }

        UpdateAnimations();
    }

    // Handle the updating of animations
    private void UpdateAnimations()
    {
        MovementState newState = MovementState.idle;

        // Determine flipping
        if (isGrounded)     // only flip if on ground
        {
            sprite.flipX = rb.transform.position.x >= TURNING_POINT_X;
        }
        MovementState forward = sprite.flipX ? MovementState.movingBackward : MovementState.movingForward;
        MovementState backward = sprite.flipX ? MovementState.movingForward : MovementState.movingBackward;

        // Handle horizontal movement
        if (dirX > 0f)
        {
            newState = forward;
        }
        else if (dirX < 0f)
        {
            newState = backward;
        }

        // Handle crouching
        if (isCrouching)
        {
            newState = MovementState.crouching;
        }

        // Handle vertical movement
        if (rb.velocity.y > 0.1f)       // rising
        {
            newState = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f)  // falling
        {
            newState = MovementState.falling;
        }

        anim.SetInteger("State", (int)newState);
    }

    public void SetTurningPoint(float tp)
    {
        TURNING_POINT_X = tp;
    }
}
