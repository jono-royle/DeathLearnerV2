using Assets.Scripts.DeathLearner;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Static
{
    public static class MLTextWriter
    {
        private static string FilePath = @$"{Application.persistentDataPath}\DeathLearnInput.txt";

        public static async Task WriteTxtAsync(List<PlayerAction> playerActions)
        {
            using (StreamWriter writer = new StreamWriter(FilePath, true))
            {
                foreach (var playerAction in playerActions)
                {
                    var actionLine = $"{playerAction.ButtonPressed}\t{playerAction.PlayerPosition.x}\t{playerAction.PlayerPosition.y}" +
                        $"\t{playerAction.EnemyPosition.x}\t{playerAction.EnemyPosition.y}\t{playerAction.EnemyVelocity.x}" +
                        $"\t{playerAction.EnemyVelocity.y}\t{playerAction.ClosestArrowPosition.x}\t{playerAction.ClosestArrowPosition.y}";
                    await writer.WriteLineAsync(actionLine);
                }
            }
        }

        public static void DeleteTxtFile()
        {
            File.Delete(FilePath);
        }
    }
}
