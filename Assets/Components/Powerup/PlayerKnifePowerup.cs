using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnifePowerup : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                PlayerThrow playerThrow = collision.gameObject.GetComponent<PlayerThrow>();

                if (playerThrow)
                    playerThrow.CanThrow = true;
                
                Destroy(gameObject);
                return;
            default:
                break;
        }
    }
}
