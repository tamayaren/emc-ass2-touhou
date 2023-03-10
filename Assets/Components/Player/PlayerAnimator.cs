using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator playerAnimator;
    private Rigidbody2D rigidbody;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        playerAnimator.SetInteger("XSpeed", Mathf.Abs((int)(rigidbody.velocity.x)));
        playerAnimator.SetInteger("YSpeed", (int)(rigidbody.velocity.y));

        if (rigidbody.velocity.x != 0f)
            spriteRenderer.flipX = rigidbody.velocity.x < 0f;
    }
}
