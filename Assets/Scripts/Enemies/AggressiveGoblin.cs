using Assets.Scripts.Enemies;
using Assets.Scripts.Static;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveGoblin : Enemy
{
    public float StunTime = 1;

    // Update is called once per frame
    protected override void Update()
    {
        if(hitTimer < 0)
        {
            if (Player.transform.position.x > transform.position.x)
            {
                moveVelocity = Speed;
                if (Direction == Vector2.left)
                {
                    Direction = CharacterActions.ChangeDirection(Direction != Vector2.left, spriteRenderer, Direction);
                }
            }
            if (Player.transform.position.x < transform.position.x)
            {
                moveVelocity = -Speed;
                if (Direction == Vector2.right)
                {
                    Direction = CharacterActions.ChangeDirection(Direction != Vector2.left, spriteRenderer, Direction);
                }
            }

            if (Player.transform.position.y == transform.position.y)
            {
                FireEnemyArrow();
            }
            if (Player.transform.position.y - transform.position.y > 2)
            {
                EnemyJump();
            }
        }
        else
        {
            moveVelocity = 0;
        }

        base.Update();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Scenery")
        {
            hitTimer = StunTime;
        }

        base.OnCollisionEnter2D(collision);
    }
}
