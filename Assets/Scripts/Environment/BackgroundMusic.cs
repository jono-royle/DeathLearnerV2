using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    static BackgroundMusic instance;

    public AudioClip[] MusicClips;

    public AudioSource Audio;

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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
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
