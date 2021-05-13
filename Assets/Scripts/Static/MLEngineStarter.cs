using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Static
{
    public static class MLEngineStarter
    {
        public static Process StartMachineLearningEngine()
        {
            var compiler = new Process();
            compiler.StartInfo.FileName = "C:\\Users\\monoj\\UnityProjects\\DeathLearnV2\\DeathLearnV2ML.ConsoleApp\\bin\\Release\\netcoreapp3.1\\DeathLearnV2ML.ConsoleApp.exe";
            compiler.StartInfo.UseShellExecute = false;
            compiler.StartInfo.RedirectStandardOutput = true;
            compiler.StartInfo.RedirectStandardInput = true;
            compiler.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            //compiler.StartInfo.Arguments
            compiler.Start();
            return compiler;
        }
    }
}
