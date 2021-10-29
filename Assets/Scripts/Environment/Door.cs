using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public Sprite UnlockedSprite;

    private bool locked = true;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlock()
    {
        locked = false;
        spriteRenderer.sprite = UnlockedSprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!locked && collision.gameObject.name == "SwordBoy")
        {
            var sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneIndex + 1);
        }
    }
}
