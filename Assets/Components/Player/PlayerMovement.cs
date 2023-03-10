using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 MovementAxisSpeed = new Vector2(32, 0);
    public float JumpForce = 20f;
    
    private Animator playerAnimator;
    
    public bool CanMove = true;
    public bool CanJump = false;

    public bool CanDoubleJump = false;
    //
    [SerializeField] private bool OnPlatform = false;
    [SerializeField] private bool JumpCooldown = false;
    [SerializeField] private bool DoubleJumpCooldown = false;
    [SerializeField] private float DefaultJumpAmplifier = 1000f;
    [SerializeField] private float DoubleJumpVelocity = 5f;
    [SerializeField] private Vector2 DefaultMovementAmplifier = new Vector2(100, 0);
    
    //
    private Rigidbody2D rigidbody;

    private protected void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float XInput = Input.GetAxis("Horizontal");
        float YInput = Input.GetAxis("Vertical");

        Vector2 computed = new Vector2();

        CanDoubleJump = (rigidbody.velocity.y >= DoubleJumpVelocity || rigidbody.velocity.y <= -DoubleJumpVelocity);
        
        // Not using y for input yet.
        if (CanMove && (Mathf.Abs(XInput) + Mathf.Abs(YInput)) > 0) computed += Move(new Vector2(XInput, YInput)); 
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 jumpComputed = new Vector2();
            if (CanJump && !JumpCooldown)
            {
                jumpComputed += Jump();
                StartCoroutine(JumpCooldownHandler(.25f));
                playerAnimator.SetTrigger("Jump");
            }
            else if (CanDoubleJump && !DoubleJumpCooldown)
            {
                playerAnimator.SetTrigger("DoubleJump");
                jumpComputed += Jump(.75f, new Vector2(4, 3));
                DoubleJumpCooldown = true;
            }
            
            rigidbody.AddForce(jumpComputed, ForceMode2D.Impulse);
        }
        
        rigidbody.AddForce(computed * (Time.deltaTime), ForceMode2D.Force); 
    }

    private Vector2 Move(Vector2 Input)
    {
        return new Vector2((MovementAxisSpeed.x * Input.x) * (DefaultMovementAmplifier.x + 1f), (MovementAxisSpeed.y * Input.y)) * (DefaultMovementAmplifier.y + 1f);
    }

    private Vector2 Jump(float InitialJumpMultiplier = 1f, Vector2 addedForce = new Vector2())
    {
        return new Vector2(0, JumpForce * (DefaultJumpAmplifier * InitialJumpMultiplier)) + addedForce;
    }

    // Cooldowns
    private IEnumerator JumpCooldownHandler(float CooldownDuration)
    {
        JumpCooldown = true;
        yield return new WaitForSeconds(CooldownDuration);
        JumpCooldown = false;
    }

    private void NonCoroutine()
    {
        
    }
    
    // Collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Platform":
                OnPlatform = true;
                DoubleJumpCooldown = false;
                CanJump = true;
                break;
            default:
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Platform":
                OnPlatform = false;
                CanJump = false;
                break;
            default:
                break;
        }
    }
}
