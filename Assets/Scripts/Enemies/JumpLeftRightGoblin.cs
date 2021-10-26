using Assets.Scripts.Enemies;
using Assets.Scripts.Static;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumpLeftRightGoblin : Enemy
{
    private float jumpCount = 0;
    private bool firstCollision = true;

    // Update is called once per frame
    protected override void Update()
    {
        if (isGrounded)
        {
            EnemyJump();

            if (Direction == Vector2.left)
            {
                moveVelocity = -Speed;
            }
            else
            {
                moveVelocity = Speed;
            }
        }

        base.Update();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (firstCollision)
        {
            firstCollision = false;
        }
        else if (collision.gameObject.tag == "Scenery")
        {
            jumpCount++;
            if(jumpCount >= 2)
            {
                Direction = CharacterActions.ChangeDirection(Direction != Vector2.left, spriteRenderer, Direction);
                jumpCount = 0;
            }
        }
        base.OnCollisionEnter2D(collision);
    }
}
