using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public bool RightWayUp = true;
    public Vector2 EndPosition;
    public float GhostSpeed = 0.01f;
    public bool BossGhost = false;

    // Start is called before the first frame update
    void Start()
    {
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (!RightWayUp)
        {
            spriteRenderer.flipY = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, EndPosition, 0.01f);
        if(transform.position.x == EndPosition.x && transform.position.y == EndPosition.y)
        {
            Destroy(gameObject);
        }
    }
}
