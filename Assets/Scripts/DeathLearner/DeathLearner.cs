using Assets.Scripts.DeathLearner;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathLearner : MonoBehaviour
{
    public float WaitTimer = 0.5f;
    public Rigidbody2D player;
    public Rigidbody2D enemy;

    private List<PlayerAction> playerActions = new List<PlayerAction>();
    private bool deathRecorded = false;

    // Update is called once per frame
    void Update()
    {
        if (deathRecorded)
        {
            return;
        }

        WaitTimer -= Time.deltaTime;
        var playerAction = new PlayerAction();
        var actionRecorded = false;

        //TODO: Currently just recording a single button press, can be multiple at once

        //Left Right Movement
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            playerAction.ButtonPressed = ButtonPress.Left;
            WaitTimer = 0.5f;
            actionRecorded = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            playerAction.ButtonPressed = ButtonPress.Right;
            WaitTimer = 0.5f;
            actionRecorded = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            playerAction.ButtonPressed = ButtonPress.Jump;
            WaitTimer = 0.5f;
            actionRecorded = true;
        }

        //Fire Arrow
        if (Input.GetKey(KeyCode.F))
        {
            playerAction.ButtonPressed = ButtonPress.Arrow;
            WaitTimer = 0.5f;
            actionRecorded = true;
        }

        //Swing Sword
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAction.ButtonPressed = ButtonPress.Sword;
            WaitTimer = 0.5f;
            actionRecorded = true;
        }

        if(WaitTimer <= 0)
        {
            playerAction.ButtonPressed = ButtonPress.None;
            WaitTimer = 0.5f;
            actionRecorded = true;
        }

        if (actionRecorded == true)
        {
            playerAction.PlayerPosition.PositionX = player.position.x;
            playerAction.PlayerPosition.PositionY = player.position.y;
            playerAction.EnemyPosition.PositionX = enemy.position.x;
            playerAction.EnemyPosition.PositionY = enemy.position.y;
            playerActions.Add(playerAction);
        }

    }

    public async void RecordDeath()
    {
        deathRecorded = true;
        await WriteTxtAsync();
    }


    public async Task WriteTxtAsync()
    {
        using (StreamWriter writer = new StreamWriter($"C:\\Users\\monoj\\UnityProjects\\MLTest\\DeathLearnTest1.txt", true))
        {
            foreach(var playerAction in playerActions)
            {
                await writer.WriteLineAsync($"{ playerAction.ButtonPressed}\t{playerAction.PlayerPosition.PositionX}\t{playerAction.PlayerPosition.PositionY}\t{playerAction.EnemyPosition.PositionX}\t{playerAction.EnemyPosition.PositionY}");
            }
        }
    }
}
