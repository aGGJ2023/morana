using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    float horizontal;
    float vertical;

    public float modifierTTL;
    private float modifierGiven;

    //public float runSpeed = 20.0f;
    public float Speed = 1.5f;
    public float ModifiedSpeed = 1.75f;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");


        if (Time.time - modifierGiven > modifierTTL)
        {
            currentSpeed = Speed;
            modifierGiven = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * Speed, vertical * Speed);
    }

    public void GiveSpeedModifier()
    {
        if (modifierGiven == 0)
        {
            currentSpeed = ModifiedSpeed;
            modifierGiven = Time.time;
        }
    }
}
