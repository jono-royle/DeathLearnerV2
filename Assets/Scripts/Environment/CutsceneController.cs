using Assets.Scripts.Static;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{
    [DllImport("user32.dll")] static extern uint GetActiveWindow();
    [DllImport("user32.dll")] static extern bool SetForegroundWindow(IntPtr hWnd);

    public GameObject CutsceneCanvas;
    public Ghost Ghost;
    public float GhostSpawnRate = 0.5f;
    public Text MlProgressText;

    private bool engineCompleted;
    private bool dialogueCompleted;
    private float ghostTimer = 0;
    private float mlTimer = 0;

    private IntPtr unityPtr;

    private void Awake()
    {
        unityPtr = (IntPtr)GetActiveWindow();
    }

    // Start is called before the first frame update
    async void Start()
    {
        engineCompleted = false;
        dialogueCompleted = false;

        var task = BuildEngine();
        Thread.Sleep(3000);
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
        mlTimer += Time.deltaTime;
        MlProgressText.text = $"Machine learning in progress {mlTimer.ToString("0.00")} out of 100";

        if (dialogueCompleted)
        {
            ghostTimer += Time.deltaTime;
            if (ghostTimer >= GhostSpawnRate)
            {
                CreateGhost();
            }
        }

        if (engineCompleted && dialogueCompleted)
        {
            var sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneIndex + 1);
        }
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

    public void CutsceneDialogueFinished()
    {
        dialogueCompleted = true;
        Destroy(CutsceneCanvas);
    }

    private async Task BuildEngine()
    {
        var process = MLEngineStarter.BuildMachineLearningEngine();
        await WaitForExitAsync(process);
    }

    private void CreateGhost()
    {
        Ghost ghost = Instantiate(Ghost, new Vector2(UnityEngine.Random.Range(-16.7f, -10f), 15), transform.rotation);
        ghost.RightWayUp = false;
        ghost.GhostSpeed = UnityEngine.Random.Range(0.01f, 0.06f);
        ghost.EndPosition = new Vector2(-14.8f, -4f);
        ghostTimer = 0;
    }
}
