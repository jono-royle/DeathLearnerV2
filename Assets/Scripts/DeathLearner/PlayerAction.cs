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
            PlayerPosition = new Vector2();
            EnemyPosition = new Vector2();
            EnemyVelocity = new Vector2();
            ClosestArrowPosition = new Vector2();
        }
        public ButtonPress ButtonPressed { get; set; }
        public Vector2 PlayerPosition { get; set; }
        public Vector2 EnemyPosition { get; set; }
        public Vector2 EnemyVelocity { get; set; }
        public Vector2 ClosestArrowPosition { get; set; }
    }
}
