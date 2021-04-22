using Assets.Scripts.Enemies;
using Assets.Scripts.Static;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpArrowGoblin : Enemy
{

    // Update is called once per frame
    void Update()
    {
        EnemyJump();

        if (Player.transform.position.x > transform.position.x && direction == Vector2.left)
        {
            direction = CharacterActions.ChangeDirection(direction != Vector2.left, spriteRenderer, direction);
        }
        if (Player.transform.position.x < transform.position.x && direction == Vector2.right)
        {
            direction = CharacterActions.ChangeDirection(direction != Vector2.left, spriteRenderer, direction);
        }

        FireEnemyArrow();

        base.Update();
    }
}
