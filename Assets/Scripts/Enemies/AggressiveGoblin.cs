using Assets.Scripts.Enemies;
using Assets.Scripts.Static;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveGoblin : Enemy
{
    // Update is called once per frame
    protected override void Update()
    {
        if (Player.transform.position.x > transform.position.x)
        {
            moveVelocity = Speed;
            if(Direction == Vector2.left)
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
        if(Player.transform.position.y - transform.position.y > 2)
        {
            EnemyJump();
        }

        base.Update();
    }
}
