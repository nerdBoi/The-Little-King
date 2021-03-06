﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFireball : MonoBehaviour
{

    private Rigidbody2D rb2d;
    public int speed;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            GameObject go = GameObject.Find("Player");
            PlayerMovement cs = go.GetComponent<PlayerMovement>();
            Vector3 spot = cs.transform.position;
            //
            rb2d = GetComponent<Rigidbody2D>();
            StartCoroutine(FireballLiFe());
            direction = spot - transform.position;
            rb2d.AddForce(direction * speed);
            //
        } catch
        {
            //player is dead
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator FireballLiFe()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //if the thing is an enemy
        if (hitInfo.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        } 
    }

 

}
