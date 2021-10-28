using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinHead : MonoBehaviour
{
    private float headLifeTime = 1;
    private float headLifeTimer = 0;
    private Vector2 endPosition;


    // Start is called before the first frame update
    void Start()
    {
        endPosition = new Vector2(Random.Range(-20f, 20f), transform.position.y + 15);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, endPosition, 0.02f);
        headLifeTimer += Time.deltaTime;
        if (headLifeTimer >= headLifeTime)
        {
            Destroy(gameObject);
        }
    }
}
