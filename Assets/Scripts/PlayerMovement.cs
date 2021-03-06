﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public bool isFacingRight = true;
    public float jumpForce = 400;
    public float moveSpeed;
    private Rigidbody2D rb2d;
    float moveHorizontal = 0f;
    bool didJump = false;
    CircleCollider2D footCollider;
    public bool isOnGround;
    public LayerMask floors;
    public Animator animator;
    public float fallMultiplier = 5f;
    public float lowJumpMultiplier = 8f;
    public Vector3 velocity;
    private Vector3 respawnPoint;
    public SpriteRenderer rend;
    public GameObject deathEffect;
    //health related things
    public Health playerHealth;
    private int maxHealth;
    private int curHealth = 3;
    public bool isKnockBack = false;
    public bool isWhite = false;
    private int isWhiteFlipper = 0;
    private bool canTakeDamage = true;

    private void Start()
    {
      

        rb2d = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        
    }

    public void setRespawnPoint(Vector3 position)
    {
        respawnPoint = position;
    }

    private void Update()
    {
    
        //checking if player can jump by overlapping two bounding boxes
        isOnGround = Physics2D.OverlapArea(new Vector2(transform.position.x - .2f, transform.position.y - 1.3f), 
            new Vector2(transform.position.x + .2f, transform.position.y + 1.3f), floors);


        moveHorizontal = Input.GetAxisRaw("Horizontal");

        //applying animator variables
        animator.SetBool("isOnGround", isOnGround);
        animator.SetFloat("moveSpeed", Mathf.Abs(moveHorizontal));
        animator.SetFloat("YVelocity", (rb2d.velocity.y));

        

        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            didJump = true;
        }

        velocity = rb2d.velocity;

        //health stuff
        playerHealth.curHealth = this.curHealth;
        
        
    }

    void FlipCharacter()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180, 0f);

    }

    //always do physics code within Fixed Update
    //going for infinite acceleration technique, similar to megaman games
    private void FixedUpdate()
    {
        //Debug.Log() is the console
        if (!isKnockBack)
        {
            Vector2 movement = new Vector2(moveHorizontal * moveSpeed, rb2d.velocity.y);
            rb2d.velocity = movement;

            if (didJump)
            {
                rb2d.AddForce(new Vector2(0f, jumpForce));
                didJump = false;
            }

            if (moveHorizontal > 0 && !isFacingRight)
            {
                FlipCharacter();
            }
            else if (moveHorizontal < 0 && isFacingRight)
            {
                FlipCharacter();
            }

            //to apply fast fall and shortened jump
            if (rb2d.velocity.y < 0)
            {
                rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb2d.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
            velocity = rb2d.velocity;
            //this works dont touch it

            if (transform.position.y < -100)
            {
                transform.position = respawnPoint;
                rb2d.velocity = new Vector3(0f, 0f, 0f);
            }
        } else
        {
            if (isWhiteFlipper < 4)
            {
                rend.material.color = new Color(255, 255, 255);
                //isWhite = !isWhite;
                isWhiteFlipper++;
            } else
            {

                rend.material.color = Color.white;
                //isWhite = !isWhite;
                isWhiteFlipper++;
                if (isWhiteFlipper > 4)
                {
                    isWhiteFlipper = 0;
                }
            }
            knockBack(5);
        }
        if (this.curHealth <= 0)
        {
            death();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       
        //if the thing is an enemy
        if (other.gameObject.CompareTag("Enemy") && canTakeDamage)
        {

            curHealth--;
            isKnockBack = true;
            StartCoroutine(InvulTime());
            StartCoroutine(knockBackTime());
        }
    }

    void knockBack(float i)
    {
        if (isFacingRight)
        {
            rb2d.velocity = new Vector2(-i, 0f) ;
        } else
        {
            rb2d.velocity = new Vector2(i, 0f);
        }
    }

    IEnumerator InvulTime()
    {
        canTakeDamage = false;
        rend.material.color = new Color(1f, 1f, 1f, .25f);
        yield return new WaitForSeconds(1f);
        canTakeDamage = true;
    }

    IEnumerator knockBackTime()
    {
        yield return new WaitForSeconds(.25f);
        rend.material.color = Color.white;
        isKnockBack = false;
        isWhiteFlipper = 0;
    }

    void death()
    {
        Vector3 explosionPoint = transform.position;
        explosionPoint.y += 2.5f;
        Instantiate(deathEffect, explosionPoint, transform.rotation);
        Destroy(gameObject);
    }
}
