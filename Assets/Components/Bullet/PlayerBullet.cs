using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                return;
            case "Obstacle":
                MirasaHandler mirasaHandler = collision.gameObject.GetComponent<MirasaHandler>();
                
                if (!ReferenceEquals(mirasaHandler, null))
                    mirasaHandler.Damage(1);
                break;
            default:
                break;
        }
        
        Destroy(gameObject);
    }
}
