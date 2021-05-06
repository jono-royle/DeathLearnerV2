using Assets.Scripts.DeathLearner;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Static
{
    public static class MLTextWriter
    {
        public static async Task WriteTxtAsync(List<PlayerAction> playerActions)
        {
            using (StreamWriter writer = new StreamWriter($"C:\\Users\\monoj\\UnityProjects\\MLTest\\DeathLearnTest1.txt", true))
            {
                foreach (var playerAction in playerActions)
                {
                    var actionLine = $"{ playerAction.ButtonPressed}\t{playerAction.PlayerPosition.x}\t{playerAction.PlayerPosition.y}\t{playerAction.EnemyPosition.x}\t{playerAction.EnemyPosition.y}\t{playerAction.EnemyVelocity.x}\t{playerAction.EnemyVelocity.y}";
                    foreach (var arrow in playerAction.ArrowPositions)
                    {
                        actionLine += $"\t{arrow.x}\t{arrow.y}";
                    }
                    await writer.WriteLineAsync(actionLine);
                }
            }
        }
    }
}
