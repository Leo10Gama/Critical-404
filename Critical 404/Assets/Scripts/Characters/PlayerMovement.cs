using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float jumpMagnitude = 18f;

    [HideInInspector] public int playerId = 0;
    [HideInInspector] public int hp = 10000;
    [HideInInspector] public int hitstun = 0;
    [HideInInspector] public int blockstun = 0;
    [HideInInspector] public bool canBlock = false;

    private const string JUMP_KEY = "Jump";
    private const string CROUCH_KEY = "Crouch";
    private const string MOVE_AXIS = "Movement";
    private const string LIGHT_PUNCH_KEY = "Light Punch";
    private const string HEAVY_PUNCH_KEY = "Heavy Punch";
    private const string LIGHT_KICK_KEY = "Light Kick";
    private const string HEAVY_KICK_KEY = "Heavy Kick";

    private const int SLP_DURATION = 15;
    private const int SHP_DURATION = 19;
    private const int SLK_DURATION = 14;
    private const int SHK_DURATION = 21;

    private float TURNING_POINT_X = 0f;

    private enum MovementState 
    { 
        idle,               // 0
        movingForward,      // 1
        movingBackward,     // 2
        jumping,            // 3
        falling,            // 4
        crouching,          // 5
        lightPunch,         // 6
        heavyPunch,         // 7
        lightKick,          // 8
        heavyKick,          // 9
        hit,                // 10
        block               // 11
    }

    private float dirX = 0f;
    private bool pressedJump = false;
    private bool pressedCrouch = false;
    private bool isGrounded = false;    // start off the ground
    private bool isCrouching = false;
    private bool inHitstun = false;
    private bool inBlockstun = false;
    private bool triggeredCollider = false;
    private string currentAttack = "";

    private Animator anim;
    private CharacterController controller;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private GameObject myHurtboxesObject;
    private GameObject myHitboxesObject;
    private FightManager fightManager;
    private HitboxManager hbm;
    private PlayerHurtboxArtist hurtboxArtist;

    private InputActionAsset inputAsset;
    private InputActionMap player;


    private void Awake()
    {
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");

        myHurtboxesObject = transform.Find("Hurtboxes").gameObject;
        myHitboxesObject = transform.Find("Hitboxes").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        hurtboxArtist = new PlayerHurtboxArtist(hbm, myHurtboxesObject, myHitboxesObject);
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
        yield return new WaitForSeconds(SLP_DURATION / 60f); // duration of s.LP
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
        yield return new WaitForSeconds(SHP_DURATION / 60f);  // duration of s.HP
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
        yield return new WaitForSeconds(SLK_DURATION / 60f); // duration of s.LK
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
        yield return new WaitForSeconds(SHK_DURATION / 60f);  // duration of s.HK
        currentAttack = "";
    }

    // ========== UPDATING ==========

    // Update is called once per frame
    void Update()
    {
        // Only do movement if not attacking and not in hitstun
        if (currentAttack == "" && hitstun <= 0 && blockstun <= 0)
        {
            // Handle whether we can block
            // (presumably in this section, we can do actions freely)
            canBlock = sprite.flipX ? dirX > 0.01f : dirX < -0.01f;

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

        // Decrease hitstun timer
        if (hitstun > 0 && !inHitstun) 
        {
            inHitstun = true;
            StartCoroutine(TickAwayHitstun()); 
        }
        // Decreate blockstun timer
        if (blockstun > 0 && !inBlockstun)
        {
            inBlockstun = true;
            if (!isGrounded) rb.velocity /= 2f;
            rb.velocity = new Vector2(0f, 0f);
            StartCoroutine(TickAwayBlockstun());
        }

        UpdateAnimations();
        // UpdateHurtboxes();   // called in UpdateAnimations(), where it is given a MovementState
    }

    // Handle the updating of animations
    private void UpdateAnimations()
    {
        MovementState newState = MovementState.idle;

        // Determine flipping
        MovementState currState = (MovementState)anim.GetInteger("State");
        bool canFlip = currState == MovementState.idle || currState == MovementState.movingForward ||
            currState == MovementState.movingBackward || currState == MovementState.crouching;
        if (canFlip)     // only flip if idle, moving, or crouching (not in air or attacking)
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

        // IS BLOCKING (takes some priority)
        if (blockstun > 0)
        {
            newState = MovementState.block;
        }

        // TAKING DAMAGE (takes priority over other states)

        if (hitstun > 0)
        {
            newState = MovementState.hit;
        }

        anim.SetInteger("State", (int)newState);

        UpdateHurtboxes(newState);
    }

    // Handle updating hurtboxes
    private void UpdateHurtboxes(MovementState state)
    {
        int totalBoxes = myHurtboxesObject.GetComponents<BoxCollider2D>().Length +
            myHitboxesObject.GetComponents<BoxCollider2D>().Length;
        if (totalBoxes > 0) return;    // only draw once per frame

        bool isFacingRight = !sprite.flipX;

        switch (state)
        {
            case MovementState.idle:
                StartCoroutine(hurtboxArtist.DrawIdle(isFacingRight));
                return;
            case MovementState.movingForward:
                StartCoroutine(hurtboxArtist.DrawMoveForward(isFacingRight));
                return;
            case MovementState.movingBackward:
                StartCoroutine(hurtboxArtist.DrawMoveBackward(isFacingRight));
                return;
            case MovementState.jumping:
                StartCoroutine(hurtboxArtist.DrawJumpRise(isFacingRight));
                return;
            case MovementState.falling:
                StartCoroutine(hurtboxArtist.DrawJumpFall(isFacingRight));
                return;
            case MovementState.crouching:
                StartCoroutine(hurtboxArtist.DrawCrouch(isFacingRight));
                return;
            case MovementState.lightPunch:
                if (isCrouching)        // c.LP
                    {/* TODO */}
                else if (isGrounded)    // s.LP
                    StartCoroutine(hurtboxArtist.DrawSLP(isFacingRight));
                else                    // j.LP
                    {/* TODO */}
                return;
            case MovementState.heavyPunch:
                if (isCrouching)        // c.HP
                    {/* TODO */}
                else if (isGrounded)    // s.HP
                    StartCoroutine(hurtboxArtist.DrawSHP(isFacingRight));
                else                    // j.HP
                    {/* TODO */}
                return;
            case MovementState.lightKick:
                if (isCrouching)        // c.LK
                    {/* TODO */}
                else if (isGrounded)    // s.LK
                    StartCoroutine(hurtboxArtist.DrawSLK(isFacingRight));
                else                    // j.LK
                    {/* TODO */}
                return;
            case MovementState.heavyKick:
                if (isCrouching)        // c.HK
                    {/* TODO */}
                else if (isGrounded)    // s.HK
                    StartCoroutine(hurtboxArtist.DrawSHK(isFacingRight));
                else                    // j.HK
                    {/* TODO */}
                return;
            case MovementState.hit:
                StartCoroutine(hurtboxArtist.DrawHitstun(isFacingRight));
                return;
            case MovementState.block:
                StartCoroutine(hurtboxArtist.DrawStandingBlock(isFacingRight));
                return;
        }
    }

    // Colliding with hitboxes
    void OnTriggerEnter2D(Collider2D col)
    {
        if (this.transform.parent != col.transform.parent && col.transform.parent.name == "Hitboxes")
        {
            // colliding with other player's hitbox
            if (triggeredCollider) return;
            triggeredCollider = true;
            Hitbox hitbox = col.GetComponent<HitboxComponent>().hitbox;
            fightManager.LandedHit(playerId, hitbox);
            StartCoroutine(FlipColliderTriggered(playerId));
        }
    }

    /// After a hit has landed, change the status of whether or not it
    /// has landed to be false again.
    IEnumerator FlipColliderTriggered(int playerId)
    {
        yield return new WaitForSeconds(1f / 60f);
        triggeredCollider = false;
    }

    private IEnumerator TickAwayHitstun()
    {
        while (hitstun > 0)
        {
            hitstun--;
            yield return new WaitForSeconds(1f / 60f);
        }
        inHitstun = false;
    }

    private IEnumerator TickAwayBlockstun()
    {
        while (blockstun > 0)
        {
            blockstun--;
            yield return new WaitForSeconds(1f / 60f);
        }
        inBlockstun = false;
    }

    public void ClearHitboxesThisImage()
    {
        hbm.ClearAll(myHitboxesObject);
        hurtboxArtist.PreventHitboxesThisImage();
    }

    public void SetTurningPoint(float tp)
    {
        TURNING_POINT_X = tp;
    }

    public void SetFightManager(GameObject fm)
    {
        fightManager = fm.GetComponent<FightManager>();
        hbm = fightManager.GetHitboxManager();
    }
}
