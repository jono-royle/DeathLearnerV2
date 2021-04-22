using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumpGoblin : Enemy
{

    // Update is called once per frame
    protected override void Update()
    {
        EnemyJump();

        base.Update();
    }
}
