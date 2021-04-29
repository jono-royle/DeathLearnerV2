using Assets.Scripts.DeathLearner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLearner : MonoBehaviour
{
    public float WaitTimer = 0.5f;
    public Rigidbody2D player;
    public Rigidbody2D enemy;

    private List<PlayerAction> playerActions = new List<PlayerAction>();

    // Update is called once per frame
    void Update()
    {
        WaitTimer -= Time.deltaTime;
        var playerAction = new PlayerAction();

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            playerAction.ButtonsPressed.Add(ButtonPress.Jump);
            WaitTimer = 0.5f;
        }

        //Left Right Movement
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            playerAction.ButtonsPressed.Add(ButtonPress.Left);
            WaitTimer = 0.5f;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            playerAction.ButtonsPressed.Add(ButtonPress.Right);
            WaitTimer = 0.5f;
        }

        //Fire Arrow
        if (Input.GetKey(KeyCode.F))
        {
            playerAction.ButtonsPressed.Add(ButtonPress.Arrow);
            WaitTimer = 0.5f;
        }

        //Swing Sword
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAction.ButtonsPressed.Add(ButtonPress.Sword);
            WaitTimer = 0.5f;
        }

        if(WaitTimer <= 0)
        {
            playerAction.ButtonsPressed.Add(ButtonPress.None);
            WaitTimer = 0.5f;
        }

        playerActions.Add(playerAction);
    }

    public void RecordDeath()
    {
        var test = playerActions;
    }
}
