using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBarController : MonoBehaviour {

    private Rigidbody2D rb;
    private float speed = 50f;
    private Vector3 startingPosition;
    public PongBallController ball;
    private bool onWallLeft, onWallRight;
	// Use this for initialization
	public void StartGame () { 
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        startingPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (ball.endGame == true)
        {
            Debug.Log("Is it over? :/");
            transform.position = startingPosition;
            rb.velocity = Vector2.zero;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && onWallLeft == false)
        {
            rb.velocity = new Vector2(-speed, 0);
            onWallRight = false;
        }
        if (Input.GetKey(KeyCode.RightArrow) && onWallRight == false)
        {
            rb.velocity = new Vector2(speed, 0);
            onWallLeft = false;
        }
        if (Input.anyKey == false)
        {
            rb.velocity = Vector2.zero;
        }
        
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("LeftWall"))
        {
            Debug.Log("LeftWall hit");
            onWallLeft = true;
            rb.velocity = Vector2.zero;
        }
        if (collision.gameObject.tag.Equals("RightWall"))
        {
            Debug.Log("RightWall hit");
            onWallRight = true;
            rb.velocity = Vector2.zero;
        }
    }
}
