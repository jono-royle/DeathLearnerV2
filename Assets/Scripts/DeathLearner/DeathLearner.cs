using Assets.Scripts.DeathLearner;
using Assets.Scripts.Static;
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
            playerAction.PlayerPosition= player.position;
            playerAction.EnemyPosition = enemy.position;
            playerAction.EnemyVelocity = enemy.velocity;
            //Commenting out arrows as MS machine learning cant handle variable column numbers.
            //TODO: Maybe do closest arrow?
            //foreach(GameObject taggedEnemy in GameObject.FindGameObjectsWithTag("Enemy"))
            //{
            //    if(taggedEnemy.name == "RedArrow(Clone)")
            //    {
            //        playerAction.ArrowPositions.Add(taggedEnemy.GetComponent<Rigidbody2D>().position);
            //    }
            //}
            playerActions.Add(playerAction);
        }

    }

    public async void RecordDeath()
    {
        deathRecorded = true;
        await MLTextWriter.WriteTxtAsync(playerActions);
    }
}
