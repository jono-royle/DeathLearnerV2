using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    public Sprite Hearts8;
    public Sprite Hearts7;
    public Sprite Hearts6;
    public Sprite Hearts5;
    public Sprite Hearts4;
    public Sprite Hearts3;
    public Sprite Hearts2;
    public Sprite Hearts1;

    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth(int health)
    {
        switch (health)
        {
            case 1:
                spriteRenderer.sprite = Hearts1;
                break;
            case 2:
                spriteRenderer.sprite = Hearts2;
                break;
            case 3:
                spriteRenderer.sprite = Hearts3;
                break;
            case 4:
                spriteRenderer.sprite = Hearts4;
                break;
            case 5:
                spriteRenderer.sprite = Hearts5;
                break;
            case 6:
                spriteRenderer.sprite = Hearts6;
                break;
            case 7:
                spriteRenderer.sprite = Hearts7;
                break;
            case 8:
                spriteRenderer.sprite = Hearts8;
                break;
            default:
                break;
        }
    }
}
