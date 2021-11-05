using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Static
{
    public static class MLEngineStarter
    {
        private static string MLConsoleAppFileName = $"{Application.dataPath}/MLConsoleApp/DeathLearnV2ML.ConsoleApp.exe";

        public static Process StartMachineLearningEngine()
        {

            var compiler = new Process();
            compiler.StartInfo.FileName = MLConsoleAppFileName;
            compiler.StartInfo.UseShellExecute = false;
            compiler.StartInfo.RedirectStandardOutput = true;
            compiler.StartInfo.RedirectStandardInput = true;
            compiler.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            compiler.StartInfo.Arguments = $"{Application.persistentDataPath}   Start";

            compiler.Start();
            return compiler;
        }

        public static Process BuildMachineLearningEngine()
        {
            var compiler = new Process();
            compiler.StartInfo.FileName = MLConsoleAppFileName;
            compiler.StartInfo.UseShellExecute = true;
            compiler.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            compiler.StartInfo.Arguments = $"{Application.persistentDataPath}   Build";
            compiler.Start();
            return compiler;
        }

        public static void DeleteEngineFile()
        {
            File.Delete(@$"{Application.persistentDataPath}\MLModel.zip");
        }
    }
}
