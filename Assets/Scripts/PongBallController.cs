using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBallController : MonoBehaviour {
    private Rigidbody2D rb;
    public Money gameManager;
    private Vector3 startPosition;
    private float Speed = -50f;
    public bool endGame;
	// Use this for initialization
	public void StartGame () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-50f, Speed);
        startPosition = transform.position;
        endGame = false;
	}

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag.Equals("Death")){
            Debug.Log("death");
            endGame = true;
            Debug.Log("BALL: is it over? :/ ");
            rb.velocity = Vector2.zero;
            gameManager.ClosePongGame();
            transform.position = startPosition;
        } 
    }
}
