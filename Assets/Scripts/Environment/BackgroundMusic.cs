using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    static BackgroundMusic instance;

    // Drag in the .mp3 files here, in the editor
    public AudioClip[] MusicClips;

    public AudioSource Audio;

    // Singelton to keep instance alive through all scenes
    void Awake()
    {
        if (instance == null) 
        { 
            instance = this; 
        }
        else 
        { 
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        // Hooks up the 'OnSceneLoaded' method to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Called whenever a scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        // Replacement variable (doesn't change the original audio source)
        AudioClip sceneClip;

        // Plays different music in different scenes
        switch (scene.name)
        {
            case "Boss_Cutscene":
            case "Level_Boss":
                sceneClip = MusicClips[1];
                break;
            case "Tutorial":
                Audio.enabled = false;
                return;
            default:
                sceneClip = MusicClips[0];
                break;
        }

        // Only switch the music if it changed
        if (sceneClip != Audio.clip)
        {
            Audio.enabled = false;
            Audio.clip = sceneClip;
            Audio.enabled = true;
            Audio.loop = true;
        }
    }
}
