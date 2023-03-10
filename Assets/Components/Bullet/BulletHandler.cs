using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    public float Speed = 5f;
    public float Duration = 10f;
    private bool Active = false;

    //
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rigidbody.bodyType = Active ? RigidbodyType2D.Dynamic : RigidbodyType2D.Static;

        Vector2 computed = new Vector2(spriteRenderer.flipX ? -Speed : Speed, 0);
        rigidbody.AddForce(computed, ForceMode2D.Force);
    }
    
    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(Duration);
        if (this.Active)
            Destroy(gameObject);
    }

    public bool IsActive()
    {
        return this.Active;
    }

    public void SetActive(bool boolean)
    {
        this.Active = boolean;

        if (this.Active)
            StartCoroutine(Despawn());
    }
}
