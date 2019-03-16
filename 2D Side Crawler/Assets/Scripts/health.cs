﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    [SerializeField] private float healthPoints = 100f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //its function for taking damage and healing with potions etc.
    public void ChangeHealth(float value)
    {
        healthPoints -= value;
        bool moveAway = true; //Dont forget to change this
        if(moveAway)
        {
            rb.velocity = new Vector3(0, 10, 0);
        }
    }
}
