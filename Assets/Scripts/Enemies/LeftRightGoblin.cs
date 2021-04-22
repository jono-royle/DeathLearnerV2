using Assets.Scripts.Enemies;
using Assets.Scripts.Static;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeftRightGoblin : Enemy
{
    private bool firstCollision = true;

    // Update is called once per frame
    void Update()
    {

        //Left Right Movement
        if (direction == Vector2.left)
        {
            moveVelocity = -Speed;
        }
        else
        {
            moveVelocity = Speed;
        }

        base.Update();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (firstCollision)
        {
            firstCollision = false;
            return;
        }
        direction = CharacterActions.ChangeDirection(direction != Vector2.left, spriteRenderer, direction);
        base.OnCollisionEnter2D(collision);
    }
}
