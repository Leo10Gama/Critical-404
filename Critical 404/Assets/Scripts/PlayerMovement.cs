using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float jumpMagnitude = 18f;

    private const string JUMP_KEY = "up";
    private const string CROUCH_KEY = "down";
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
    private bool isGrounded = false;    // start off the ground
    private bool isCrouching = false;

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Handle crouching
        if (Input.GetKey(CROUCH_KEY) && isGrounded)
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
            dirX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(dirX * horizontalSpeed, rb.velocity.y);
        }

        // Handle jumping
        if (rb.velocity.y < 0.01f && rb.velocity.y > -0.01f && !isGrounded) // landing
        {
            isGrounded = true;
        }
        if (Input.GetKeyDown(JUMP_KEY) && isGrounded)                    // jumping off ground
        {
            if (isCrouching)     // jumping from a crouch should maintain horizontal movement
            {
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * horizontalSpeed, rb.velocity.y);
                Debug.Log("Crouching = true");
            }
            rb.velocity = new Vector3(rb.velocity.x, jumpMagnitude, 0f);
            Debug.Log("Velocity set");
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
}
