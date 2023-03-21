using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum BlockState
{
    low,    // 0
    mid,    // 1
    high    // 2
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float jumpMagnitude = 18f;
    [SerializeField] public string playerName = "";

    [HideInInspector] public int playerId = 0;
    [HideInInspector] public int hp = 1000;
    [HideInInspector] public int hitstun = 0;
    [HideInInspector] public int blockstun = 0;
    [HideInInspector] public bool canBlock = false;
    [HideInInspector] public bool canMove = true;

    private const string JUMP_KEY = "Jump";
    private const string CROUCH_KEY = "Crouch";
    private const string MOVE_AXIS = "Movement";
    private const string LIGHT_PUNCH_KEY = "Light Punch";
    private const string HEAVY_PUNCH_KEY = "Heavy Punch";
    private const string LIGHT_KICK_KEY = "Light Kick";
    private const string HEAVY_KICK_KEY = "Heavy Kick";

    // VALUES TO OVERWRITE FOR ACTUAL CHARACTERS
    private int SLP_DURATION;
    private int SHP_DURATION;
    private int SLK_DURATION;
    private int SHK_DURATION;
    private int CLP_DURATION;
    private int CHP_DURATION;
    private int CLK_DURATION;
    private int CHK_DURATION;
    private int JLP_DURATION;
    private int JHP_DURATION;
    private int JLK_DURATION;
    private int JHK_DURATION;

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
    private enum AttackState    // sorted in order of which moves lead into others
    {
        none,   // 0
        lp,     // 1
        lk,     // 2
        hp,     // 3
        hk      // 4
    }

    private float dirX = 0f;
    private bool pressedJump = false;
    private bool pressedCrouch = false;
    private bool hasJumped = false;
    private bool isGrounded = false;    // start off the ground
    private bool isCrouching = false;
    private bool inHitstun = false;
    private bool inBlockstun = false;
    private bool triggeredCollider = false;
    private string gotInputThisFrame = "";
    private bool canCancelAttack = true;
    private bool[] blockState = new bool[] {false, false, false};
    private string currentAttack = "";
    private AttackState currentAttackState = AttackState.none;
    private Coroutine currentAttackCoroutine = null;
    private Coroutine currentHitboxCoroutine = null;
    private Queue<AttackInput> attackQueue = new Queue<AttackInput>();
    private bool queueIsOpen = true;

    private Animator anim;
    private CharacterController controller;
    private CapsuleCollider2D collisionBox;
    private CapsuleCollider2D pushBox;
    private float originalCBx;
    private float originalPBx;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private GameObject myHurtboxesObject;
    private GameObject myHitboxesObject;
    private GameObject myPushboxesObject;
    private FightManager fightManager;
    private HitboxManager hbm;
    private HurtboxArtist hurtboxArtist;

    private InputActionAsset inputAsset;
    private InputActionMap player;


    private void Awake()
    {
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");

        myHurtboxesObject = transform.Find("Hurtboxes").gameObject;
        myHitboxesObject = transform.Find("Hitboxes").gameObject;
        myPushboxesObject = transform.Find("Pushboxes").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        // Iterate through each anim clip to set timing
        foreach (AnimationClip ac in anim.runtimeAnimatorController.animationClips)
        {
            string name = ac.name;
            int clilpLength = (int)(ac.length * 60) - 1;
            if (name.Contains("_LightPunch")) {
                SLP_DURATION = clilpLength;
            } else if (name.Contains("_LightKick")) {
                SLK_DURATION = clilpLength;
            } else if (name.Contains("_HeavyPunch")) {
                SHP_DURATION = clilpLength;
            } else if (name.Contains("_HeavyKick")) {
                SHK_DURATION = clilpLength;
            } else if (name.Contains("_CrouchingLightPunch")) {
                CLP_DURATION = clilpLength;
            } else if (name.Contains("_CrouchingLightKick")) {
                CLK_DURATION = clilpLength;
            } else if (name.Contains("_CrouchingHeavyPunch")) {
                CHP_DURATION = clilpLength;
            } else if (name.Contains("_CrouchingHeavyKick")) {
                CHK_DURATION = clilpLength;
            } else if (name.Contains("_JumpingLightPunch")) {
                JLP_DURATION = clilpLength;
            } else if (name.Contains("_JumpingLightKick")) {
                JLK_DURATION = clilpLength;
            } else if (name.Contains("_JumpingHeavyPunch")) {
                JHP_DURATION = clilpLength;
            } else if (name.Contains("_JumpingHeavyKick")) {
                JHK_DURATION = clilpLength;
            }
        }

        CapsuleCollider2D[] capsuleColliders = myPushboxesObject.GetComponents<CapsuleCollider2D>(); // should be 2; collision and pushbox
        if (capsuleColliders[0].isTrigger)
        {
            pushBox = capsuleColliders[0];
            collisionBox = capsuleColliders[1];
        }
        else
        {
            pushBox = capsuleColliders[1];
            collisionBox = capsuleColliders[0];
        }
        originalCBx = collisionBox.offset.x;
        originalPBx = pushBox.offset.x;

        // Use the proper hurtbox artist depending on the character selected
        switch (playerName)
        {
            case "SPREAD":
                hurtboxArtist = new SpreadHurtboxArtist(hbm, myHurtboxesObject, myHitboxesObject);
                break;
            // case "MILA":
            //     // hurtboxArtist = new MilaHurtboxArtist(hbm, myHurtboxesObject, myHitboxesObject);
            //     break;
            default:
                hurtboxArtist = new PlayerHurtboxArtist(hbm, myHurtboxesObject, myHitboxesObject);
                break;
        }
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
        pressedJump = context.action.triggered && context.action.ReadValue<float>() != 0;
        if (gotInputThisFrame == "jump" || !pressedJump) return;  // prevent multi-input
        AttackInput jumpInput = new AttackInput(BufferableInput.jump);
        StartCoroutine(jumpInput.PassTime());
        attackQueue.Enqueue(jumpInput);
        StartCoroutine(ReceivedInputThisFrame("jump"));
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        pressedCrouch = context.action.triggered;
    }

    // ========== ATTACK HELPER METHODS ==========

    private void AttackButtonPressed(
        bool actionTriggered,
        AttackState attack,
        string attackButtonName,
        IEnumerator standingRoutine,
        IEnumerator crouchingRoutine,
        IEnumerator jumpingRoutine
    ){
        // Blockers
        if (gotInputThisFrame == attackButtonName) return;  // don't get multiple inputs of the same button

        // Decide which attack we're doing
        AttackInput newAttack = null;
        if (!pressedCrouch) // standing attack
        {
            newAttack = new AttackInput(
                (BufferableInput)((int)attack - 1),
                attackButtonName,
                standingRoutine,
                jumpingRoutine
            );
        }
        else                // crouching attack
        {
            newAttack = new AttackInput(
                (BufferableInput)((int)attack + 3),
                attackButtonName,
                crouchingRoutine,
                jumpingRoutine
            );
        }
        StartCoroutine(newAttack.PassTime());
        attackQueue.Enqueue(newAttack);
        StartCoroutine(ReceivedInputThisFrame(attackButtonName));
    }

    private void CheckAttackQueue()
    {
        if (attackQueue.Count <= 0) return; // queue empty
        if (!queueIsOpen) return;       // queue is closed
        if (!canCancelAttack) return;   // cannot do an attack

        do
        {
            // Pull from queue
            AttackInput input = attackQueue.Dequeue();
            if (!input.IsAlive()) continue; // input is stale; go to the next one

            // Special case: process jumping input
            if (input.input == BufferableInput.jump)
            {
                if (isGrounded)
                {
                    ResetPlayerToIdle();
                    if (isCrouching)     // jumping from a crouch should maintain horizontal movement
                    {
                        rb.velocity = new Vector2(dirX * horizontalSpeed, rb.velocity.y);
                    }
                    rb.velocity = new Vector3(rb.velocity.x, jumpMagnitude, 0f);
                    isGrounded = false;
                    StartCoroutine(CloseQueueThisFrame());
                }
                return;
            }

            // Cancelling attack into another attack: make sure it's higher priority
            if ((int)currentAttackState >= ((int)input.input % 4) + 1) return;

            // Reset player status and set local parameters
            ResetPlayerToIdle(false);
            canCancelAttack = false;
            currentAttack = input.attackButtonName;
            SetAttackState();

            // Process attack input
            if (isGrounded) // grounded attack (standing or crouching)
            {
                rb.velocity = Vector2.zero;
                isCrouching = (int)BufferableInput.clp <= (int)input.input &&
                    (int)input.input <= (int)BufferableInput.chk;
                StartCoroutine(input.regularRoutine);
            }
            else            // jumping attack
            {
                StartCoroutine(input.jumpingRoutine);
            }
            return;
        } while (attackQueue.Count > 0);
    }

    private IEnumerator CloseQueueThisFrame()
    {
        queueIsOpen = false;
        yield return new WaitForSeconds(1f / 60f);
        queueIsOpen = true;
    }

    private IEnumerator ReturnToIdleAfterFrames(int framesToWait)
    {
        Debug.Log(framesToWait);
        yield return new WaitForSeconds(framesToWait / 60f);    // duration of some move
        Debug.Log("Done");
        currentAttack = "";
        SetAttackState();
        canCancelAttack = true;
    }

    private void ResetPlayerToIdle(bool resetAttack = true)
    {
        StopCurrentCoroutines();
        hurtboxArtist.StopDrawingAll();
        hurtboxArtist.StopCurrentRoutine();
        if (resetAttack)
        {
            currentAttack = "";
            currentAttackState = AttackState.none;
            canCancelAttack = true;
        }
    }

    private IEnumerator ReceivedInputThisFrame(string buttonName)
    {
        gotInputThisFrame = buttonName;
        yield return new WaitForSeconds(1f / 60f);
        gotInputThisFrame = "";
    }

    // ========== ATTACKS ==========

    // Light Punch

    public void OnLightPunch(InputAction.CallbackContext context)
    {
        if (context.action.ReadValue<float>() == 0) return; // button up
        AttackButtonPressed(
            context.action.triggered,
            AttackState.lp,
            LIGHT_PUNCH_KEY,
            StandingLightPunch(),
            CrouchingLightPunch(),
            JumpingLightPunch()
        );
    }

    private IEnumerator StandingLightPunch()
    {
        currentAttackCoroutine = StartCoroutine(ReturnToIdleAfterFrames(SLP_DURATION));
        yield return currentAttackCoroutine;
    }

    private IEnumerator CrouchingLightPunch()
    {
        currentAttackCoroutine = StartCoroutine(ReturnToIdleAfterFrames(CLP_DURATION));
        yield return currentAttackCoroutine;
    }

    private IEnumerator JumpingLightPunch()
    {
        currentAttackCoroutine = StartCoroutine(ReturnToIdleAfterFrames(JLP_DURATION));
        yield return currentAttackCoroutine;
    }

    // Heavy Punch

    public void OnHeavyPunch(InputAction.CallbackContext context)
    {
        if (context.action.ReadValue<float>() == 0) return; // button up
        AttackButtonPressed(
            context.action.triggered,
            AttackState.hp,
            HEAVY_PUNCH_KEY,
            StandingHeavyPunch(),
            CrouchingHeavyPunch(),
            JumpingHeavyPunch()
        );
    }

    private IEnumerator StandingHeavyPunch()
    {
        currentAttackCoroutine = StartCoroutine(ReturnToIdleAfterFrames(SHP_DURATION));
        yield return currentAttackCoroutine;
    }

    private IEnumerator CrouchingHeavyPunch()
    {
        currentAttackCoroutine = StartCoroutine(ReturnToIdleAfterFrames(CHP_DURATION));
        yield return currentAttackCoroutine;
    }

    private IEnumerator JumpingHeavyPunch()
    {
        currentAttackCoroutine = StartCoroutine(ReturnToIdleAfterFrames(JHP_DURATION));
        yield return currentAttackCoroutine;
    }

    // Light Kick

    public void OnLightKick(InputAction.CallbackContext context)
    {
        if (context.action.ReadValue<float>() == 0) return; // button up
        AttackButtonPressed(
            context.action.triggered,
            AttackState.lk,
            LIGHT_KICK_KEY,
            StandingLightKick(),
            CrouchingLightKick(),
            JumpingLightKick()
        );
    }

    private IEnumerator StandingLightKick()
    {
        currentAttackCoroutine = StartCoroutine(ReturnToIdleAfterFrames(SLK_DURATION));
        yield return currentAttackCoroutine;
    }

    private IEnumerator CrouchingLightKick()
    {
        currentAttackCoroutine = StartCoroutine(ReturnToIdleAfterFrames(CLK_DURATION));
        yield return currentAttackCoroutine;
    }

    private IEnumerator JumpingLightKick()
    {
        currentAttackCoroutine = StartCoroutine(ReturnToIdleAfterFrames(JLK_DURATION));
        yield return currentAttackCoroutine;
    }

    // Heavy Kick

    public void OnHeavyKick(InputAction.CallbackContext context)
    {
        if (context.action.ReadValue<float>() == 0) return; // button up
        AttackButtonPressed(
            context.action.triggered,
            AttackState.hk,
            HEAVY_KICK_KEY,
            StandingHeavyKick(),
            CrouchingHeavyKick(),
            JumpingHeavyKick()
        );
    }

    private IEnumerator StandingHeavyKick()
    {
        currentAttackCoroutine = StartCoroutine(ReturnToIdleAfterFrames(SHK_DURATION));
        yield return currentAttackCoroutine;
    }

    private IEnumerator CrouchingHeavyKick()
    {
        currentAttackCoroutine = StartCoroutine(ReturnToIdleAfterFrames(CHK_DURATION));
        yield return currentAttackCoroutine;
    }

    private IEnumerator JumpingHeavyKick()
    {
        currentAttackCoroutine = StartCoroutine(ReturnToIdleAfterFrames(JHK_DURATION));
        yield return currentAttackCoroutine;
    }

    // ========== UPDATING ==========

    // Update is called once per frame
    void Update()
    {
        // Do absolutely nothing if cannot move (game ended)
        if (!canMove) 
        {
            ResetPlayerToIdle();
            UpdateAnimations();
            return;
        }

        // Handle whether we can block
        canBlock = (sprite.flipX ? dirX > 0.01f : dirX < -0.01f) && currentAttack == "" &&
            hitstun <= 0 && blockstun <= 0;

        // Only do movement if not attacking and not in hitstun
        if ((currentAttack == "" || !isGrounded || (pressedJump && canCancelAttack)) && hitstun <= 0 && blockstun <= 0)
        {
            // Handle crouching
            if (pressedCrouch && isGrounded)
            {
                isCrouching = true;
                rb.velocity = Vector2.zero;
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
                hasJumped = false;
                int state = anim.GetInteger("State");
                if (6 <= state && state <= 9)   // landed mid-attack, cancel it
                {
                    ResetPlayerToIdle();
                }
            } // *NOTE* : logic for actually jumping => CheckAttackQueue()

            // If we're blocking, determine how much we're blocking
            if (canBlock)
            {
                if (isCrouching)        // crouch block: block low and mid
                {
                    blockState[(int)BlockState.low] = true;
                    blockState[(int)BlockState.mid] = true;
                    blockState[(int)BlockState.high] = false;
                }
                else if (isGrounded)    // standing block: block mid and high
                {
                    blockState[(int)BlockState.low] = false;
                    blockState[(int)BlockState.mid] = true;
                    blockState[(int)BlockState.high] = true;
                }
                else                    // aerial block: block all
                {
                    blockState[(int)BlockState.low] = true;
                    blockState[(int)BlockState.mid] = true;
                    blockState[(int)BlockState.high] = true;
                }
            }
            else    // not blocking at the moment
            {
                blockState[(int)BlockState.low] = false;
                blockState[(int)BlockState.mid] = false;
                blockState[(int)BlockState.high] = false;
            }
        }

        // Launched into the air, set whether we're grounded
        if (rb.velocity.y > 0.1f || rb.velocity.y < -0.1f && isGrounded)
        {
            isGrounded = false;
        }

        // Decrease hitstun timer
        if (hitstun > 0 && !inHitstun) 
        {
            // stop everything
            ResetPlayerToIdle(false);
            currentAttack = "";
            canCancelAttack = false;
            isCrouching = false;
            // switch to be now in hitstun
            inHitstun = true;
            StartCoroutine(TickAwayHitstun());
            canCancelAttack = true;
        }
        // Decreate blockstun timer
        if (blockstun > 0 && !inBlockstun)
        {
            // stop everything
            ResetPlayerToIdle(false);
            currentAttack = "";
            canCancelAttack = false;
            // switch to be now in blockstun
            inBlockstun = true;
            if (!isGrounded) rb.velocity /= 2f;
            rb.velocity = Vector2.zero;
            StartCoroutine(TickAwayBlockstun());
            canCancelAttack = true;
        }

        if (canCancelAttack) CheckAttackQueue();

        UpdateAnimations();
        // UpdateHurtboxes();   // called in UpdateAnimations(), where it is given a MovementState
    }

    // Handle the updating of animations
    private void UpdateAnimations()
    {
        MovementState newState = MovementState.idle;
        MovementState newPositionalState = MovementState.idle;

        // Determine flipping
        MovementState currState = (MovementState)anim.GetInteger("State");
        bool canFlip = currState == MovementState.idle || currState == MovementState.movingForward ||
            currState == MovementState.movingBackward || currState == MovementState.crouching;
        if (canFlip)     // only flip if idle, moving, or crouching (not in air or attacking)
        {
            sprite.flipX = rb.transform.position.x >= TURNING_POINT_X;
            collisionBox.offset = new Vector2(
                sprite.flipX ? -originalCBx : originalCBx,
                collisionBox.offset.y
            );
            pushBox.offset = new Vector2(
                sprite.flipX ? -originalPBx : originalPBx,
                pushBox.offset.y
            );
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
            newPositionalState = MovementState.crouching;
        }

        // Handle vertical movement
        if (rb.velocity.y > 0.1f)       // rising
        {
            newState = MovementState.jumping;
            newPositionalState = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f)  // falling
        {
            newState = MovementState.falling;
            newPositionalState = MovementState.jumping;
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
            currentAttack = "";
        }

        // TAKING DAMAGE (takes priority over other states)

        if (hitstun > 0)
        {
            newState = MovementState.hit;
            currentAttack = "";
        }


        anim.SetInteger("State", (int)newState);
        anim.SetInteger("Positional State", (int)newPositionalState);
        anim.SetInteger("Attack State", (int)currentAttackState);

        // if (currentHitboxCoroutine != null && currState != newState) 
        // {
        //     StopCurrentCoroutines();
        // }

        // Debug.Log(newState);
        
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
                currentHitboxCoroutine = StartCoroutine(hurtboxArtist.DrawIdle(isFacingRight));
                break;
            case MovementState.movingForward:
                currentHitboxCoroutine = StartCoroutine(hurtboxArtist.DrawMoveForward(isFacingRight));
                break;
            case MovementState.movingBackward:
                currentHitboxCoroutine = StartCoroutine(hurtboxArtist.DrawMoveBackward(isFacingRight));
                break;
            case MovementState.jumping:
                if (hasJumped)
                {
                    currentHitboxCoroutine = StartCoroutine(hurtboxArtist.DrawJumpRise(isFacingRight));
                }
                else
                {
                    currentHitboxCoroutine = StartCoroutine(hurtboxArtist.DrawJump(isFacingRight));
                    StartCoroutine(FlipHasJumped());
                }
                break;
            case MovementState.falling:
                currentHitboxCoroutine = StartCoroutine(hurtboxArtist.DrawJumpFall(isFacingRight));
                hasJumped = false;
                break;
            case MovementState.crouching:
                currentHitboxCoroutine = StartCoroutine(hurtboxArtist.DrawCrouch(isFacingRight));
                break;
            case MovementState.lightPunch:
                if (isCrouching)        // c.LP
                    currentHitboxCoroutine = StartCoroutine(hurtboxArtist.DrawCLP(isFacingRight));
                else if (isGrounded)    // s.LP
                    currentHitboxCoroutine = StartCoroutine(hurtboxArtist.DrawSLP(isFacingRight));
                else                    // j.LP
                    currentHitboxCoroutine = StartCoroutine(hurtboxArtist.DrawJLP(isFacingRight));
                break;
            case MovementState.heavyPunch:
                if (isCrouching)        // c.HP
                    currentHitboxCoroutine = StartCoroutine(hurtboxArtist.DrawCHP(isFacingRight));
                else if (isGrounded)    // s.HP
                    currentHitboxCoroutine = StartCoroutine(hurtboxArtist.DrawSHP(isFacingRight));
                else                    // j.HP
                    currentHitboxCoroutine = StartCoroutine(hurtboxArtist.DrawJHP(isFacingRight));
                break;
            case MovementState.lightKick:
                if (isCrouching)        // c.LK
                    currentHitboxCoroutine = StartCoroutine(hurtboxArtist.DrawCLK(isFacingRight));
                else if (isGrounded)    // s.LK
                    currentHitboxCoroutine = StartCoroutine(hurtboxArtist.DrawSLK(isFacingRight));
                else                    // j.LK
                    currentHitboxCoroutine = StartCoroutine(hurtboxArtist.DrawJLK(isFacingRight));
                break;
            case MovementState.heavyKick:
                if (isCrouching)        // c.HK
                    currentHitboxCoroutine = StartCoroutine(hurtboxArtist.DrawCHK(isFacingRight));
                else if (isGrounded)    // s.HK
                    currentHitboxCoroutine = StartCoroutine(hurtboxArtist.DrawSHK(isFacingRight));
                else                    // j.HK
                    currentHitboxCoroutine = StartCoroutine(hurtboxArtist.DrawJHK(isFacingRight));
                break;
            case MovementState.hit:
                currentHitboxCoroutine = StartCoroutine(hurtboxArtist.DrawHitstun(isFacingRight));
                break;
            case MovementState.block:
                if (isCrouching)        // crouch block
                    currentHitboxCoroutine = StartCoroutine(hurtboxArtist.DrawCrouchingBlock(isFacingRight));
                else if (isGrounded)    // standing block
                    currentHitboxCoroutine = StartCoroutine(hurtboxArtist.DrawStandingBlock(isFacingRight));
                else                    // jumping block
                    currentHitboxCoroutine = StartCoroutine(hurtboxArtist.DrawJumpingBlock(isFacingRight));
                break;
        }
    }

    // Colliding with hitboxes
    void OnTriggerEnter2D(Collider2D col)
    {
        if (this.transform.parent == col.transform.parent) return;  // colliding with ourself; do nothing
        if (col.transform.parent.name == "Grid") return;    // colliding with map; do nothing

        // Colliding with the other player's hitbox
        if (col.transform.parent.name == "Hitboxes")
        {
            if (triggeredCollider) return;
            triggeredCollider = true;
            Hitbox hitbox = col.GetComponent<HitboxComponent>().hitbox;
            fightManager.LandedHit(playerId, hitbox);
            StartCoroutine(FlipColliderTriggered(playerId));
        }
        else    // colliding with pushbox, push out of the way opposite other player
        {
            if (rb.velocity.y < 0.5f)   // only push back if falling
            {
                float pushScaling = 0.2f;
                float maxPushSpeed = 1.0f;
                float newXSpeed = Math.Min(Math.Abs(rb.velocity.x) + pushScaling, maxPushSpeed) * 
                    (TURNING_POINT_X >= rb.transform.position.x ? -0.1f : 0.1f);
                rb.velocity = new Vector2(rb.velocity.x + newXSpeed, rb.velocity.y + 0.001f);
            }
        }
    }

    /// After a hit has landed, change the status of whether or not it
    /// has landed to be false again.
    IEnumerator FlipColliderTriggered(int playerId)
    {
        yield return new WaitForSeconds(1f / 60f);
        triggeredCollider = false;
    }

    private IEnumerator FlipHasJumped()
    {
        yield return new WaitForSeconds(1f / 60f);
        hasJumped = true;
    }

    private IEnumerator TickAwayHitstun()
    {
        while (hitstun > 0)
        {
            hitstun--;
            yield return new WaitForSeconds(1f / 60f);
        }
        inHitstun = false;
        ResetPlayerToIdle();
    }

    private IEnumerator TickAwayBlockstun()
    {
        while (blockstun > 0)
        {
            blockstun--;
            yield return new WaitForSeconds(1f / 60f);
        }
        inBlockstun = false;
        canCancelAttack = true;
        currentAttackState = AttackState.none;
    }

    public void StopCurrentCoroutines()
    {
        if (currentAttackCoroutine != null) StopCoroutine(currentAttackCoroutine);
        if (currentHitboxCoroutine != null) StopCoroutine(currentHitboxCoroutine);
        hurtboxArtist.StopDrawingAll();
    }

    /// Tell whether or not the player is currently blocking against a type of attack
    public bool IsBlockingAgainst(BlockState hit)
    {
        return blockState[(int)hit];
    }

    public void ClearHitboxesThisImage()
    {
        hbm.ClearHitboxes();
        hurtboxArtist.PreventHitboxesThisImage();
    }

    public string GetCurrentAttack()
    {
        return currentAttack;
    }

    public void SetAttackState()
    {
        switch (currentAttack)
        {
            case LIGHT_PUNCH_KEY:
                currentAttackState = AttackState.lp;
                return;
            case HEAVY_PUNCH_KEY:
                currentAttackState = AttackState.hp;
                return;
            case LIGHT_KICK_KEY:
                currentAttackState = AttackState.lk;
                return;
            case HEAVY_KICK_KEY:
                currentAttackState = AttackState.hk;
                return;
            default:
                currentAttackState = AttackState.none;
                return;
        }
    }

    public void SetCanCancelAttack(bool status)
    {
        canCancelAttack = status;
    }

    public void SetVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
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
