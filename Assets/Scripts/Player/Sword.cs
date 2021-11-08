using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sword : MonoBehaviour
{
    public float SwordLifeTime = 0.5f;
    public UnityEvent SwordDestroyedEvent;
    public bool Midair = false;

    private float swordLifeTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //When a sword is used on the ground it has a life timer, in mid air it exists until it hits the ground
        if (!Midair)
        {
            swordLifeTimer += Time.deltaTime;
            if (swordLifeTimer >= SwordLifeTime)
            {
                SwordDestroyedEvent.Invoke();
                Destroy(gameObject);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Midair)
        {
            SwordDestroyedEvent.Invoke();
            Destroy(gameObject);
        }
    }
}
