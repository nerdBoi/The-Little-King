using System.Collections;
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

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
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
        } else if (moveHorizontal < 0 && isFacingRight) {
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
    }

    void OnTriggerEnter2D(Collider2D other)
    {

    }
}
