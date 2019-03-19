using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{

    public float moveSpeed = .25f;
    private Rigidbody2D rb2d;
    public bool isFacingRight = true;
    public Transform turnAroundPoint;
    public LayerMask floors;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        bool shouldSwitch = Physics2D.OverlapArea(new Vector2(turnAroundPoint.position.x - .2f, turnAroundPoint.position.y - 1.3f),
          new Vector2(turnAroundPoint.position.x + .2f, turnAroundPoint.position.y + 1.3f), floors);

        if (!shouldSwitch)
        {
            FlipSnake();
        }


        if (isFacingRight)
        {
            rb2d.velocity = new Vector2 (moveSpeed, rb2d.velocity.y);
        } else {
            rb2d.velocity = new Vector2 (-1 * moveSpeed, rb2d.velocity.y);
        }
        
    }

    void FlipSnake()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
