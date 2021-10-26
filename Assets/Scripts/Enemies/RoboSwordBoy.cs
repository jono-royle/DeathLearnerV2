using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.DeathLearner;
using System;
using Assets.Scripts.Static;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

public class RoboSwordBoy : Enemy
{
    [DllImport("user32.dll")] static extern uint GetActiveWindow();
    [DllImport("user32.dll")] static extern bool SetForegroundWindow(IntPtr hWnd);

    private Process compiler;
    private IntPtr unityPtr;
    //private StreamWriter streamWriter;

    private void Awake()
    {
        unityPtr = (IntPtr)GetActiveWindow();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        Direction = Vector2.right;
        compiler = MLEngineStarter.StartMachineLearningEngine();
        //This is an ugly hack - run the ML console app as a new process and then change focus back to unity. Would be better to run the
        //ML in the unity program but can't get the ML packages to load in unity
        Thread.Sleep(500);
        SetForegroundWindow(unityPtr);
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        ButtonPress buttonPress = GetButtonPressFromMLEngine();

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
            Direction = CharacterActions.ChangeDirection(true, spriteRenderer, Direction);
        }
        if (buttonPress == ButtonPress.Right)
        {
            moveVelocity = Speed;
            Direction = CharacterActions.ChangeDirection(false, spriteRenderer, Direction);
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

    private void OnApplicationQuit()
    {
        MLTextWriter.DeleteTxtFile();
        MLEngineStarter.DeleteEngineFile();
        compiler.CloseMainWindow();
    }

    private ButtonPress GetButtonPressFromMLEngine()
    {
        var inputString = $"{transform.position.x} {transform.position.y} {Player.position.x} {Player.position.y} {Player.velocity.x} {Player.velocity.y}";

        compiler.StandardInput.WriteLine(inputString);
        //streamWriter.WriteLine(inputString);
        var prediction = compiler.StandardOutput.ReadLine();
        ButtonPress buttonPress;
        if (!Enum.TryParse(prediction, out buttonPress))
        {
            buttonPress = ButtonPress.None;
        }

        return buttonPress;
    }
}
