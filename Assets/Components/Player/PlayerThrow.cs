using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    public bool CanThrow = false;

    public float Offset = .5f;
    public float CooldownDuration = 1f;
    
    private bool ThrowCooldown = false;
    //
    [SerializeField] private GameObject knife;
    private Animator playerAnimator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!CanThrow) return;
        if (Input.GetMouseButtonDown(0) && !ThrowCooldown)
            Throw();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator SpawnBullet()
    {
        yield return new WaitForSeconds(.35f);
        Vector3 offset = new Vector3((spriteRenderer.flipX ? -Offset : Offset), 0, 0);
        GameObject bullet = Instantiate(knife, transform.position + offset, Quaternion.identity);

        BulletHandler bulletHandler = bullet.GetComponent<BulletHandler>();
        SpriteRenderer bulletSpriteRenderer = bullet.GetComponent<SpriteRenderer>();
        bulletSpriteRenderer.flipX = spriteRenderer.flipX;

        bulletHandler.Speed = 5;
        bulletHandler.SetActive(true);
    }

    private IEnumerator Cooldown()
    {
        ThrowCooldown = true;
        yield return new WaitForSeconds(CooldownDuration);
        ThrowCooldown = false;
    }
    
    private void Throw()
    {
        playerAnimator.SetTrigger("Throw");
        StartCoroutine(SpawnBullet());
        StartCoroutine(Cooldown());
    }
}
