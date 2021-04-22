using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumpLeftRightGoblin : Enemy
{
    public float SmallJumpLength = 1;

    private bool leftFacing = false;
    private bool isGrounded = true;
    private float jumpCount = 0;
    private bool firstCollision = true;
    private float jumpTimer = 0;

    // Update is called once per frame
    void Update()
    {
        jumpTimer -= Time.deltaTime;

        if (isGrounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, Jump);
            isGrounded = false;

            if (leftFacing)
            {
                moveVelocity = Speed;
            }
            else
            {
                moveVelocity = -Speed;
            }
        }
        //if(jumpCount == 1 && jumpTimer <= 0)
        //{
        //    GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
        //}

        base.Update();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        //if (firstCollision)
        //{
        //    firstCollision = false;
        //    return;
        //}
        if (collision.gameObject.tag == "Scenery")
        {
            isGrounded = true;
            jumpCount++;
            if(jumpCount >= 2)
            {
                leftFacing = !leftFacing;
                spriteRenderer.flipX = !spriteRenderer.flipX;
                jumpCount = 0;
            }
        }
        base.OnCollisionEnter2D(collision);
    }
}
