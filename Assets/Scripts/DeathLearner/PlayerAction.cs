using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DeathLearner
{
    public class PlayerAction
    {
        public PlayerAction()
        {
            //ButtonsPressed = new List<ButtonPress>();
            ArrowPositions = new List<Vector2>();
            PlayerPosition = new Vector2();
            EnemyPosition = new Vector2();
            EnemyVelocity = new Vector2();
        }

        //public List<ButtonPress> ButtonsPressed { get; set; }
        public ButtonPress ButtonPressed { get; set; }
        public Vector2 PlayerPosition { get; set; }
        public Vector2 EnemyPosition { get; set; }
        public Vector2 EnemyVelocity { get; set; }
        public List<Vector2> ArrowPositions { get; set; }
    }
}
