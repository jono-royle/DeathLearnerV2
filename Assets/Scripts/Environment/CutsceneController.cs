using Assets.Scripts.Static;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    [DllImport("user32.dll")] static extern uint GetActiveWindow();
    [DllImport("user32.dll")] static extern bool SetForegroundWindow(IntPtr hWnd);

    private bool engineCompleted;

    private IntPtr unityPtr;

    private void Awake()
    {
        unityPtr = (IntPtr)GetActiveWindow();
    }

    // Start is called before the first frame update
    async void Start()
    {
        engineCompleted = false;

        var task = BuildEngine();
        Thread.Sleep(200);
        SetForegroundWindow(unityPtr);
        await task;
        await task.ContinueWith(t =>
         {
             engineCompleted = true;
         });
    }

    // Update is called once per frame
    void Update()
    {
        if (engineCompleted)
        {
            var sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneIndex + 1);
        }
    }

    private async Task BuildEngine()
    {
        var process = MLEngineStarter.BuildMachineLearningEngine();
        await WaitForExitAsync(process);
    }

    public static Task WaitForExitAsync(System.Diagnostics.Process process, CancellationToken cancellationToken = default(CancellationToken))
    {
        if (process.HasExited)
        {
            return Task.CompletedTask;
        }

        var tcs = new TaskCompletionSource<object>();
        process.EnableRaisingEvents = true;
        process.Exited += (sender, args) => tcs.TrySetResult(null);
        if (cancellationToken != default(CancellationToken))
        {
            cancellationToken.Register(() => tcs.SetCanceled());
        }

        return process.HasExited ? Task.CompletedTask : tcs.Task;
    }
}
