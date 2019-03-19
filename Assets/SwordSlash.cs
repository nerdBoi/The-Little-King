using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    //this is really a rough prototype of it as a ranged weapon, could be useful in the future
    private Rigidbody2D rb2d;
    private static SwordSlash _instance;
    public static SwordSlash Instance { get { return _instance; } }
    private PlayerMovement playerMovement;
    public int speed = 20;

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
        }
    }

    private IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
        
    }


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        // rb2d.velocity = playerMovement.velocity;
        //Debug.Log(playerMovement.velocity.x); 
        rb2d.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(KillOnAnimationEnd());

    }


    private void FixedUpdate()
    {
        
    }
}
