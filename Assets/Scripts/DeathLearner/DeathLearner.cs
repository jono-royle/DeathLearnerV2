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

    //Every update we record what buttons the player has pressed (or if they have pressed no button for 0.5 seconds)
    //We have this info along with the current game state (player position, enemy position and movement, enemy arrows)
    //to a tab delimited txt file which can be used for ML training
    void Update()
    {
        if (deathRecorded)
        {
            return;
        }

        WaitTimer -= Time.deltaTime;
        var buttonsPressed = new List<ButtonPress>();
        var actionRecorded = false;

        //Left Right Movement
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            buttonsPressed.Add(ButtonPress.Left);
            WaitTimer = 0.5f;
            actionRecorded = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            buttonsPressed.Add(ButtonPress.Right);
            WaitTimer = 0.5f;
            actionRecorded = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            buttonsPressed.Add(ButtonPress.Jump);
            WaitTimer = 0.5f;
            actionRecorded = true;
        }

        //Fire Arrow
        if (Input.GetKey(KeyCode.F))
        {
            buttonsPressed.Add(ButtonPress.Arrow);
            WaitTimer = 0.5f;
            actionRecorded = true;
        }

        //Swing Sword
        if (Input.GetKeyDown(KeyCode.Space))
        {
            buttonsPressed.Add(ButtonPress.Sword);
            WaitTimer = 0.5f;
            actionRecorded = true;
        }

        if(WaitTimer <= 0)
        {
            buttonsPressed.Add(ButtonPress.None);
            WaitTimer = 0.5f;
            actionRecorded = true;
        }

        if (actionRecorded == true)
        {
            foreach(var buttonPress in buttonsPressed)
            {
                var playerAction = new PlayerAction();
                playerAction.ButtonPressed = buttonPress;
                playerAction.PlayerPosition = player.position;
                playerAction.EnemyPosition = enemy.position;
                playerAction.EnemyVelocity = enemy.velocity;
                playerAction.ClosestArrowPosition = GetClosestArrow();
                playerActions.Add(playerAction);
                if (playerAction.ButtonPressed == ButtonPress.Sword || playerAction.ButtonPressed == ButtonPress.Arrow)
                {
                    //Record sword/arrow 5 times to add extra weight in ML
                    for (int i = 0; i < 4; i++)
                    {
                        playerActions.Add(playerAction);
                    }
                }
            }
        }

    }

    public async void RecordDeath()
    {
        deathRecorded = true;
        await MLTextWriter.WriteTxtAsync(playerActions);
    }

    private Vector2 GetClosestArrow()
    {
        var arrowPosition = new Vector2(100, 100);
        float closestDistanceSqr = Mathf.Infinity;
        foreach (GameObject taggedEnemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (taggedEnemy.name == "RedArrow(Clone)")
            {
                Vector3 directionToTarget = taggedEnemy.transform.position - player.transform.position;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    arrowPosition = new Vector2(taggedEnemy.transform.position.x, taggedEnemy.transform.position.y);
                }

            }
        }
        return arrowPosition;
    }
}
