using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 2f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    Rigidbody2D myRigidBody2D;

    private bool hasStarted = false;

    // cached component refs
    AudioSource MyAudioSource;

    Vector2 paddleToBallVector;

	// Use this for initialization
	void Start () {
        paddleToBallVector = transform.position - paddle1.transform.position;
        MyAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if(hasStarted == false)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
        

        
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
            hasStarted = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2
            (UnityEngine.Random.Range(0, randomFactor*2), 
            UnityEngine.Random.Range(0, randomFactor));

        myRigidBody2D.velocity += velocityTweak;
        if (hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            MyAudioSource.PlayOneShot(clip);
        }
        
    }
}
