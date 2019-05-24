﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : Enemy
{

    public bool isFacingRight = true;
    public Vector3 playerPosition;
    public LayerMask floors;
    public int health = 10;
    public GameObject deathEffect;
    private SpriteRenderer renderer;
    private Rigidbody2D rb2d;
    public GameObject Projectile;
    GameObject go;
    PlayerMovement cs;
    // Start is called before the first frame update
    void Start()
    {
        go = GameObject.Find("Player");
        cs = go.GetComponent<PlayerMovement>();
        playerPosition = cs.transform.position;

        //
        renderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(TimeToPort());
        StartCoroutine(TimeToShootFire());
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = cs.transform.position;
        //keeps mage facing you
        if ((playerPosition.x < transform.position.x) && isFacingRight)
        {
            FlipWizard();
        }
        if ((playerPosition.x > transform.position.x) && !isFacingRight)
        {
            FlipWizard();
        }
        //ends keeping wizard facing you
    }

    private void FixedUpdate()
    {
       rb2d.AddForce(transform.right * Random.Range(-20f, 20f));
       rb2d.AddForce(transform.up * Random.Range(-20f, 20f));
    }

    public override void TakeDamage(int damage)
    {
        health = health - damage;
        renderer.material.color = new Color(255, 0, 0);
        StartCoroutine(TakeDamageFlash());

        if (health <= 0)
        {
            death();
        }
    }

    void death()
    {
        Vector3 explosionPoint = transform.position;
        explosionPoint.y += 2.5f;
        Instantiate(deathEffect, explosionPoint, transform.rotation);
        Destroy(gameObject);
    }

    private IEnumerator TakeDamageFlash()
    {
        yield return new WaitForSeconds(.076f);
        renderer.material.color = Color.white;
    }

    private IEnumerator TimeToPort()
    {
        yield return new WaitForSeconds(1.7f);
        Teleport();
        StartCoroutine(TimeToPort());

    }

    private void Teleport()
    {
        float xPlus = Random.Range(-1f, 1f);
        float yPlus = Random.Range(-1f, 1f);
        Vector3 teleSpot = playerPosition;
        if (xPlus > 0 && yPlus > 0)
        {
            teleSpot.x += 10;
            teleSpot.y += 5;
        }
        else if (xPlus > 0 && yPlus < 0) {
            teleSpot.x += 10;
            teleSpot.y -= 1;
        }
        else if (xPlus < 0 && yPlus > 0)
        {
            teleSpot.x -= 10;
            teleSpot.y += 5;
        }
        else if (xPlus < 0 && yPlus < 0)
        {
            teleSpot.x -= 10;
            teleSpot.y -= 1;
        }
            transform.position = teleSpot;
    }

    void FlipWizard()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private IEnumerator TimeToShootFire()
    {
        yield return new WaitForSeconds(.9f);
        ShootFire();
        StartCoroutine(TimeToShootFire());

    }

    private void ShootFire()
    {
        Vector3 firepoint = transform.position;
        Instantiate(Projectile, firepoint, transform.rotation);
    }

}