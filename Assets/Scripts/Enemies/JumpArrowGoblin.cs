using Assets.Scripts.Enemies;
using Assets.Scripts.Static;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpArrowGoblin : Enemy
{

    // Update is called once per frame
    protected override void Update()
    {
        EnemyJump();

        if (Player.transform.position.x > transform.position.x && Direction == Vector2.left)
        {
            Direction = CharacterActions.ChangeDirection(Direction != Vector2.left, spriteRenderer, Direction);
        }
        if (Player.transform.position.x < transform.position.x && Direction == Vector2.right)
        {
            Direction = CharacterActions.ChangeDirection(Direction != Vector2.left, spriteRenderer, Direction);
        }

        FireEnemyArrow();

        base.Update();
    }
}
