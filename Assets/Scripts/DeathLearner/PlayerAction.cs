using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DeathLearner
{
    public class PlayerAction
    {
        public PlayerAction()
        {
            //ButtonsPressed = new List<ButtonPress>();
            ArrowPositions = new List<Position>();
            PlayerPosition = new Position();
            EnemyPosition = new Position();
        }

        //public List<ButtonPress> ButtonsPressed { get; set; }
        public ButtonPress ButtonPressed { get; set; }
        public Position PlayerPosition { get; set; }
        public Position EnemyPosition { get; set; }
        public List<Position> ArrowPositions { get; set; }
    }
}
