﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float speed = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
