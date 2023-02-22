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
    private const string LIGHT_PUNCH_KEY = "Light Punch";
    private const string HEAVY_PUNCH_KEY = "Heavy Punch";
    private const string LIGHT_KICK_KEY = "Light Kick";
    private const string HEAVY_KICK_KEY = "Heavy Kick";

    private float TURNING_POINT_X = 0f;

    private enum MovementState { 
        idle,               // 0
        movingForward,      // 1
        movingBackward,     // 2
        jumping,            // 3
        falling,            // 4
        crouching,          // 5
        lightPunch,         // 6
        heavyPunch,         // 7
        lightKick,          // 8
        heavyKick           // 9
    }

    private float dirX = 0f;
    private bool pressedJump = false;
    private bool isGrounded = false;    // start off the ground
    private bool pressedCrouch = false;
    private bool isCrouching = false;
    private string currentAttack = "";

    private Animator anim;
    private CharacterController controller;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private GameObject fightManager;
    private HitboxManager hbm;
    private GameObject myHitboxesObject;

    private InputActionAsset inputAsset;
    private InputActionMap player;


    private void Awake()
    {
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");

        myHitboxesObject = transform.Find("Hurtboxes").gameObject;
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
        player.FindAction("Light Punch").started += OnLightPunch;
        player.FindAction("Heavy Punch").started += OnHeavyPunch;
        player.FindAction("Light Kick").started += OnLightKick;
        player.FindAction("Heavy Kick").started += OnHeavyKick;
        player.Enable();
    }

    private void OnDisable()
    {
        player.FindAction("Jump").started -= OnJump;
        player.FindAction("Crouch").started -= OnCrouch;
        player.FindAction("Light Punch").started -= OnLightPunch;
        player.FindAction("Heavy Punch").started -= OnHeavyPunch;
        player.FindAction("Light Kick").started -= OnLightKick;
        player.FindAction("Heavy Kick").started -= OnHeavyKick;
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

    // ========== ATTACKS ==========

    // Light Punch

    public void OnLightPunch(InputAction.CallbackContext context)
    {
        // s.LP
        if (context.action.triggered && isGrounded && !isCrouching && currentAttack == "")
        {
            rb.velocity = new Vector2(0f, 0f);
            currentAttack = LIGHT_PUNCH_KEY;
            StartCoroutine(StandingLightPunch());
        }
    }

    private IEnumerator StandingLightPunch()
    {
        yield return new WaitForSeconds(15f / 60f); // duration of s.LP
        currentAttack = "";
    }

    // Heavy Punch

    public void OnHeavyPunch(InputAction.CallbackContext context)
    {
        // s.HP
        if (context.action.triggered && isGrounded && !isCrouching && currentAttack == "")
        {
            rb.velocity = new Vector2(0f, 0f);
            currentAttack = HEAVY_PUNCH_KEY;
            StartCoroutine(StandingHeavyPunch());
        }
    }

    private IEnumerator StandingHeavyPunch()
    {
        yield return new WaitForSeconds(19f / 60f);  // duration of s.HP
        currentAttack = "";
    }

    // Light Kick

    public void OnLightKick(InputAction.CallbackContext context)
    {
        // s.LK
        if (context.action.triggered && isGrounded && !isCrouching && currentAttack == "")
        {
            rb.velocity = new Vector2(0f, 0f);
            currentAttack = LIGHT_KICK_KEY;
            StartCoroutine(StandingLightKick());
        }
    }

    private IEnumerator StandingLightKick()
    {
        yield return new WaitForSeconds(14f / 60f); // duration of s.LK
        currentAttack = "";
    }

    // Heavy Kick

    public void OnHeavyKick(InputAction.CallbackContext context)
    {
        // s.HK
        if (context.action.triggered && isGrounded && !isCrouching && currentAttack == "")
        {
            rb.velocity = new Vector2(0f, 0f);
            currentAttack = HEAVY_KICK_KEY;
            StartCoroutine(StandingHeavyKick());
        }
    }

    private IEnumerator StandingHeavyKick()
    {
        yield return new WaitForSeconds(21f / 60f);  // duration of s.HK
        currentAttack = "";
    }

    // ========== UPDATING ==========

    // Update is called once per frame
    void Update()
    {

        hbm.CreateHurtbox(myHitboxesObject, new Vector2(0f, 0f), new Vector2(1f, 1f), 1);

        // Only do movement if not attacking
        if (currentAttack == "")
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

        // ATTACKS

        if (currentAttack == LIGHT_PUNCH_KEY)       // s.LP
        {
            newState = MovementState.lightPunch;
        }
        else if (currentAttack == LIGHT_KICK_KEY)   // s.LK
        {
            newState = MovementState.lightKick;
        }
        else if (currentAttack == HEAVY_PUNCH_KEY)  // s.HP
        {
            newState = MovementState.heavyPunch;
        }
        else if (currentAttack == HEAVY_KICK_KEY)   // s.HK
        {
            newState = MovementState.heavyKick;
        }

        anim.SetInteger("State", (int)newState);
    }

    public void SetTurningPoint(float tp)
    {
        TURNING_POINT_X = tp;
    }

    public void SetFightManager(GameObject fm)
    {
        fightManager = fm;
        hbm = fightManager.GetComponent<FightManager>().GetHitboxManager();
    }
}
