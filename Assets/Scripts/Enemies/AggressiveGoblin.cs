using Assets.Scripts.Enemies;
using Assets.Scripts.Static;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveGoblin : Enemy
{
    private bool isGrounded = true;
    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.x > transform.position.x)
        {
            moveVelocity = Speed;
            if(direction == Vector2.left)
            {
                direction = CharacterActions.ChangeDirection(direction != Vector2.left, spriteRenderer, direction);
            }
        }
        if (Player.transform.position.x < transform.position.x)
        {
            moveVelocity = -Speed;
            if (direction == Vector2.right)
            {
                direction = CharacterActions.ChangeDirection(direction != Vector2.left, spriteRenderer, direction);
            }
        }

        if (Player.transform.position.y == transform.position.y)
        {
            FireEnemyArrow();
        }
        if(Player.transform.position.y - transform.position.y > 2)
        {
            EnemyJump();
        }

        base.Update();
    }
}
