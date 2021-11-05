using Assets.Scripts.Static;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialController : MonoBehaviour
{
    public UnityEvent StartTutorial;

    // Start is called before the first frame update
    void Start()
    {
        MLTextWriter.DeleteTxtFile();
        MLEngineStarter.DeleteEngineFile();
        StartTutorial.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
