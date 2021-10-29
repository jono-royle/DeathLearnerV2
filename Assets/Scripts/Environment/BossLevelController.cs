using Assets.Scripts.Static;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossLevelController : MonoBehaviour
{
    public Button RestartButton;
    public Button ExitButton;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActive()
    {
        gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(true);
        ExitButton.interactable = true;
        RestartButton.gameObject.SetActive(true);
        RestartButton.interactable = true;
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        MLTextWriter.DeleteTxtFile();
        MLEngineStarter.DeleteEngineFile();
        SceneManager.LoadScene(0);
    }
}
