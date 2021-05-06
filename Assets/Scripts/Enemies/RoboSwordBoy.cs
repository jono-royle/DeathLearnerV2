using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.DeathLearner;
using System;
using Assets.Scripts.Static;
using System.Diagnostics;
using System.IO;

public class RoboSwordBoy : Enemy
{
    private Process compiler;
    //private StreamWriter streamWriter;

    // Start is called before the first frame update
    protected override void Start()
    {
        direction = Vector2.right;
        compiler = new Process();
        compiler.StartInfo.FileName = "C:\\Users\\monoj\\UnityProjects\\DeathLearnV2\\DeathLearnV2ML.ConsoleApp\\bin\\Release\\netcoreapp3.1\\DeathLearnV2ML.ConsoleApp.exe";
        compiler.StartInfo.UseShellExecute = false;
        compiler.StartInfo.RedirectStandardOutput = true;
        compiler.StartInfo.RedirectStandardInput = true;
        compiler.Start();
        //streamWriter = compiler.StandardInput;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        var inputString = $"{transform.position.x} {transform.position.y} {Player.position.x} {Player.position.y} {Player.velocity.x} {Player.velocity.y}";

        compiler.StandardInput.WriteLine(inputString);
        //streamWriter.WriteLine(inputString);
        var prediction = compiler.StandardOutput.ReadLine();

        //ModelInput inputData = new ModelInput()
        //{
        //    PlayerX = transform.position.x,
        //    PlayerY = transform.position.y,
        //    EnemyX = Player.position.x,
        //    EnemyY = Player.position.y,
        //    EnemyVelocityX = Player.velocity.x,
        //    EnemyVelocityY = Player.velocity.y,
        //};
        //var predictionResult = ConsumeModel.Predict(inputData);
        ButtonPress buttonPress;
        if(!Enum.TryParse(prediction, out buttonPress)){
            buttonPress = ButtonPress.None;
        }

        if (plunging)
        {
            Plunge();
            return;
        }

        //Jumping
        if (buttonPress == ButtonPress.Jump)
        {
            EnemyJump();
        }

        moveVelocity = 0;

        //Left Right Movement
        if (buttonPress == ButtonPress.Left)
        {
            moveVelocity = -Speed;
            direction = CharacterActions.ChangeDirection(true, spriteRenderer, direction);
        }
        if (buttonPress == ButtonPress.Right)
        {
            moveVelocity = Speed;
            direction = CharacterActions.ChangeDirection(false, spriteRenderer, direction);
        }
        if (hitLeft)
        {
            moveVelocity = -2 * Speed;
        }
        if (hitRight)
        {
            moveVelocity = 2 * Speed;
        }
        if (hitTimer <= 0)
        {
            hitRight = false;
            hitLeft = false;
        }

        //Fire Arrow
        if (buttonPress == ButtonPress.Arrow && arrowTimer <= 0)
        {
            FireEnemyArrow();
        }

        //Swing Sword
        if (buttonPress == ButtonPress.Sword && !swordExists)
        {
            if (isGrounded)
            {
                SwingEnemySword();
            }
            else if (!plunging)
            {
                PlungingAttack();
                Plunge();
                return;

            }
        }


        base.Update();
    }
}
