using Assets.Scripts.Enemies;
using Assets.Scripts.Static;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGoblin : Enemy
{


    // Update is called once per frame
    protected override void Update()
    {
        moveVelocity = 0;
        if(Player.transform.position.x > transform.position.x && Direction == Vector2.left)
        {
            Direction = CharacterActions.ChangeDirection(Direction != Vector2.left, spriteRenderer, Direction);
        }
        if (Player.transform.position.x < transform.position.x && Direction == Vector2.right)
        {
            Direction = CharacterActions.ChangeDirection(Direction != Vector2.left, spriteRenderer, Direction);
        }

        if(!hitLeft && !hitRight)
        {
            if (!FireEnemyArrow())
            {
                if (Direction == Vector2.left)
                {
                    moveVelocity = -Speed / 2;
                }
                else
                {
                    moveVelocity = Speed / 2;
                }
            }
        }
        base.Update();
    }
}
