using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirasaHandler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator mirasaAnimator;
    private Rigidbody2D rigidbody;
    //
    public int Health = 5;

    private void Start()
    {
        mirasaAnimator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private IEnumerator DamageEffects()
    {
        Vector2 computed = new Vector2(spriteRenderer.flipX ? 64 : -64, 42);
        rigidbody.AddForce(computed * 4f, ForceMode2D.Impulse);
       
        mirasaAnimator.SetTrigger("Hit");
        spriteRenderer.color = new Color(0.8207547f, 0.4684496f, 0.4684496f);
        yield return new WaitForSeconds(.35f);
        spriteRenderer.color = Color.white;
        if (Health <= 0)
            Destroy(gameObject);
    }
    
    public void Damage(int by)
    {
        Health -= by;

        StartCoroutine(DamageEffects());
    }
}
